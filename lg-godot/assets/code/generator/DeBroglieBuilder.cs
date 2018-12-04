using LostGen;
using Newtonsoft.Json;
using DeBroglie.Topo;
using Godot;
using System;

/*
    Notes on cell orientation and obstructions:
    They're undocumented and unintuitive to use, so don't try to derive anything from them.
    Instead, when we're defining the sample, we'll use two GridMaps. One will have the mesh
    with its orientation applied, the other will have 64 explicit obstruction tiles.

    Layer the second on top of the first, then when building the sample, use the latter directly
    for obstructions.
*/

public class DeBroglieBuilder : GridMap {
    [Export] public string SampleSrc = "";

    public DeBroglieBuilder Build(string src, int width, int height, int depth) {
        var file = new File();
        file.Open(SampleSrc, (int)File.ModeFlags.Read);
        var sampleData = JsonConvert.DeserializeObject<DeBroglieSampleData>(file.GetAsText());

        int sampleWidth = sampleData.Dimensions.X;
        int sampleHeight = sampleData.Dimensions.Y;
        int sampleDepth = sampleData.Dimensions.Z;

        var sample = TopoArray.Create<int>(
            p => sampleData.Sample[
                p.Z * sampleWidth * sampleHeight +
                p.Y * sampleWidth +
                p.X
            ],
            new Topology(sampleWidth, sampleHeight, sampleDepth, sampleData.Periodic)
        );

        DeBroglie.Models.TileModel model = null;
        switch (sampleData.Model) {
            case DeBroglieSampleData.ModelType.Adjacent:
                model = new DeBroglie.Models.AdjacentModel(sample.ToTiles());
                break;
            case DeBroglieSampleData.ModelType.Overlapping:
                model = new DeBroglie.Models.OverlappingModel(sampleData.N);
                break;
        }

        var propogator = new DeBroglie.TilePropagator(model, sample.Topology, false);
        var status = propogator.Run();
        if (status != DeBroglie.Resolution.Decided) {
            throw new Exception("Undecided");
        }

        LostGen.Point.ForEachXYZ(Point.Zero, new Point(width, height, depth),
            p => {
                int tileIndex = sample.Topology.GetIndex(p.X, p.Y, p.Z);
                var tile = sampleData.Tiles[tileIndex];
                
                int meshIndex;
                if (int.TryParse(tile.Src, out meshIndex)) {
                    // If the tile Src is an int, it's referring to an index in the MeshLibrary
                    SetCellItem(p.X, p.Y, p.Z, meshIndex, tile.Orientation);
                } else {
                    // Otherwise, assume Src is a resource path pointing to another sample set
                    var subBuilder = new DeBroglieBuilder();
                    AddChild(subBuilder);
                    // Shrink the new builder and offset so that it fits in the larger GridMap
                    subBuilder.SetScale(Vector3.One / tile.Size);
                    subBuilder.Translate(new Vector3(
                        p.X * CellSize.x,
                        p.Y * CellSize.y,
                        p.Z * CellSize.z
                    ));
                    subBuilder.Build(tile.Src, tile.Size, tile.Size, tile.Size);
                }
            }
        );

        return this;
    }
}
