using System.Collections.Generic;
using System.Linq;
using Redux;

namespace LostGen {
    static partial class Reducers {
        public static Board BoardReducer(Board previous, IAction action) {
            if (action is Action.Step) {
                return ProcessStep(previous);
            }
            var pawnAction = action as IPawnAction;
            if (pawnAction != null) {
                return ProcessPawnAction(previous, pawnAction);
            }
            return new Board {
                Pawns = PawnsReducer(previous.Pawns, action),
                Blocks = BlocksReducer(previous.Blocks, action)
            };
        }

        private static Board ProcessPawnAction(Board previous, IPawnAction action) {
            var next = previous;
            // Check if the action has a target pawn
            Pawn targetPawn;
            if (previous.Pawns.TryGetValue(action.ID, out targetPawn)) {
                // Create a new BoardState
                next = new Board(previous) {
                    Pawns = new Dictionary<uint, Pawn>(previous.Pawns) // Copy the Pawns dictionary
                };
                // Run the reducers that only refer to the currently identified Pawn
                next.Pawns[action.ID] = ProcessPawn(targetPawn, previous, action);
            }
            return next;
        }

        private static Board ProcessStep(Board previous) {
            var next = previous;

            var actions = previous.Pawns.Values // Get the first actions in the queues
                .OrderBy(p => p.StepPriority) // Sort Pawns by Priority (Speed)
                .Select(p => p.Actions.Last());

            if (actions.Any()) {
                // Create new Pawn dictionary with shorter action queues
                next = new Board(previous) {
                    Pawns = previous.Pawns.Values.Select(
                        p => new Pawn(p) {
                            Actions = p.Actions.Take(p.Actions.Count() - 1)
                        }
                    ).ToDictionary(p => p.InstanceID, p => p)
                };
                
                // Run the actions
                foreach (var dequeuedAction in actions) {
                    next = BoardReducer(next, dequeuedAction);
                }
            }

            return next;
        }
    }
}