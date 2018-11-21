using System.Linq;
using System.Collections.Generic;
using Redux;

namespace LostGen {
    partial class Reducers {
        private static Dictionary<Point, Block> BlocksReducer(Dictionary<Point, Block> previous, IAction action) {
            var next = previous;

            var set = action as Action.SetBlocks;
            if (set != null) {
                next = previous
                    .Concat(set.ToSet)
                    .ToDictionary(p => p.Key, p => p.Value);
            }

            return next;
        }
    }
}
