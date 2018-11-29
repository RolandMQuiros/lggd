using LostGen;
using Newtonsoft.Json;
using DeBroglie.Topo;
using Godot;
using System;

public class DeBroglieBuilder : GridMap {
    [Export] public string SampleSrc = "";
    
    public override void _Ready()
    {
    }

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
                }/* else {
                    
                }*/
            }
        );

        return this;
    }
}
