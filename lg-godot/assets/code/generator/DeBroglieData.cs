using Godot;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DeBroglieSampleData {
    public enum ModelType {
        Adjacent,
        Overlapping
    }
    public ModelType Model { get; set; }
    public int[] Sample { get; set; }
    public DeBroglie.Point Dimensions { get; set; }

    public class TileData {
        public string Src { get; set; }
        public int Orientation { get; set; }
        public byte Obstruction { get; set; }
    }
    public IEnumerable<TileData> Tiles { get; set; }
    public int N { get; set; }
    public bool Periodic { get; set; }
    public int Symmetries { get; set; }
}