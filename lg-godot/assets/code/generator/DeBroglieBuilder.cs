using Newtonsoft.Json;
using DeBroglie.Topo;
using Godot;
using System;

public class DeBroglieBuilder : GridMap {
    [Export] public string SampleSrc = "";
    
    public override void _Ready()
    {
    }

    public DeBroglieBuilder Build(string src) {
        var file = new File();
        file.Open(SampleSrc, (int)File.ModeFlags.Read);
        var sampleData = JsonConvert.DeserializeObject<DeBroglieSampleData>(file.GetAsText());

        int width = sampleData.Dimensions.X;
        int height = sampleData.Dimensions.Y;
        int depth = sampleData.Dimensions.Z;

        var sample = TopoArray.Create<int>(
            p => sampleData.Sample[
                p.Z * width * height +
                p.Y * width +
                p.X
            ],
            new Topology(width, height, depth, sampleData.Periodic)
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

        return this;
    }
}
