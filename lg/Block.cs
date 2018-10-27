using System;
using System.Linq;
using System.Collections.Generic;

namespace LostGen {
    public enum BlockType {
        None = 0,
        DebugSolid = CollisionFlags.Terrain
    }

    public class Blocks : Dictionary<Point, BlockType> {
        private static readonly BlockType[] _typeEnumArray = (BlockType[])Enum.GetValues(typeof(BlockType));

        public Blocks() { }

        /// <summary>
        /// Initialize a Blocks dictionary using an array of ints. Useful for testing.
        /// </summary>
        /// <param name="blockArray"></param>
        public Blocks(int[,,] blockArray) {
            Point.ForEachXYZ(
                Point.Zero,
                new Point(blockArray.GetLength(0), blockArray.GetLength(1), blockArray.GetLength(2)),
                index => {
                    int typeNum = blockArray[index.X, index.Y, index.Z];
                    this[index] = _typeEnumArray[typeNum];
                }
            );
        }

        public Blocks(Dictionary<Point, BlockType> other)
            : base (other) { }

        public Blocks(IEnumerable<KeyValuePair<Point, BlockType>> other)
            : base (other.ToDictionary(p => p.Key, p => p.Value)) { }
    }
}
