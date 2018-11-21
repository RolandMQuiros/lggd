using System;
using System.Linq;
using System.Collections.Generic;

namespace LostGen {
    public enum BlockType {
        None = 0,
        DebugSolid = CollisionFlags.Terrain
    }

    public struct Block {
        public BlockType Type;
        public Directions Obstructions;
    }
}
