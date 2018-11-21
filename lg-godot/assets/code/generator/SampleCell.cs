using System;
using System.Linq;
using Godot;
public class SampleCell : Spatial {
    [Export]
    public int ID;

    [Export]
    public LostGen.BlockType Type;

    [Export(PropertyHint.Flags,"Forward,Right,Backward,Left,Up,Down")]
    public int Obstructions;

    public LostGen.Block AsBlock {
        get {
            return new LostGen.Block {
                Type = Type,
                Obstructions = (LostGen.Directions)Obstructions
            };
        }
    }
}