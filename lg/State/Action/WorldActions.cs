using System.Collections.Generic;
using Redux;

namespace LostGen.Action {
    public class AddPawns : IAction {
        public List<Pawn> ToAdd;
    }

    public class RemovePawns : IAction {
        public HashSet<uint> IDsToRemove;
    }

    public class SetBlocks : IAction {
        public Dictionary<Point, BlockType> ToSet = new Dictionary<Point, BlockType>();
    }

    public class Step : IAction { }
}
