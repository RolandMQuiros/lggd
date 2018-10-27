using System.Linq;
using Redux;

namespace LostGen {
    partial class Reducers {
        private static Blocks BlocksReducer(Blocks previous, IAction next) {
            var set = next as Action.SetBlocks;
            if (set != null) {
                return SetBlocks(previous, set);
            }
            return previous;
        }

        private static Blocks SetBlocks(Blocks previous, Action.SetBlocks blocks) {
            if (blocks.ToSet.Any()) {
                return new Blocks(previous.Concat(blocks.ToSet));
            }
            return previous;
        }
    }
}
