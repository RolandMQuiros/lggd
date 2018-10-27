using System.Collections.Generic;
using System.Linq;
using Redux;

namespace LostGen {
    partial class Reducers {
        private static uint _pawnInstanceIDSequence = 0;

        private static Dictionary<uint, Pawn> PawnsReducer(Dictionary<uint, Pawn> previous, IAction action) {
            var add = action as Action.AddPawns;
            if (add != null) { return AddPawns(previous, add); }
            var remove = action as Action.RemovePawns;
            if (remove != null) { return RemovePawns(previous, remove); }
            return previous;
        }

        private static Dictionary<uint, Pawn> AddPawns(Dictionary<uint, Pawn> previous, Action.AddPawns add) {
            var next = previous;
                
            if (add.ToAdd.Any()) {
                next = new Dictionary<uint, Pawn>(previous);
                foreach (var pawn in add.ToAdd) {
                    pawn.InstanceID = _pawnInstanceIDSequence++;
                    next[pawn.InstanceID] = pawn;
                }
            }
            return next;
        }

        private static Dictionary<uint, Pawn> RemovePawns(Dictionary<uint, Pawn> previous, Action.RemovePawns remove) {
            var next = previous;
            if (remove.IDsToRemove.Any()) {
                next = previous
                    .Where(pair => !remove.IDsToRemove.Contains(pair.Key))
                    .ToDictionary(p => p.Key, p => p.Value);
            }
            return next;
        }
    }
}
