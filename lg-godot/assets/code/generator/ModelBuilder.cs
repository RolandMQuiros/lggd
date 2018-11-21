using System.Linq;
using System;
using DeBroglie.Models;
using DeBroglie.Topo;
using Godot;

public class ModelBuilder : Node {
    public enum ModelType {
        Adjacent,
        Overlapping
    };

    public ModelType Type = ModelType.Adjacent;

    public TileModel BuildModel() {
        TileModel model = null;
        switch (Type) {
            case ModelType.Adjacent:
                model = new AdjacentModel();
                break;
            case ModelType.Overlapping:
                model = new OverlappingModel(3);
                break;
        }
        return model;
    }

    /// <summary>
    /// Builds a WFC sample from a Godot scene.
    /// </summary>
    /// <returns></returns>
    private ITopoArray<LostGen.Block> BuildSample() {
        var cells = GetChildren().Cast<SampleCell>();
        
        // Get the max size of th esample
        var size = new LostGen.Point(
            Mathf.RoundToInt(cells.Max(c => c.Translation.x)),
            Mathf.RoundToInt(cells.Max(c => c.Translation.y)),
            Mathf.RoundToInt(cells.Max(c => c.Translation.z))
        );

        var sample = TopoArray.Create(
            cells.Aggregate(
                new LostGen.Block[size.Z, size.Y, size.X],
                (array, cell) => {
                    var index = new LostGen.Point(
                        Mathf.RoundToInt(cell.Translation.x),
                        Mathf.RoundToInt(cell.Translation.y),
                        Mathf.RoundToInt(cell.Translation.z)
                    );
                    array[index.Z, index.Y, index.Z] = cell.AsBlock;
                    return array;
                }
            ),
            periodic: true
        );

        // Generate a board using ID numbers, which we key to the child SampleCells

        return null;
    }
}