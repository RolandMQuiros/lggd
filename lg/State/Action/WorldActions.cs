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
        public Dictionary<Point, Block> ToSet = new Dictionary<Point, Block>();
    }

    public class Step : IAction { }
}
