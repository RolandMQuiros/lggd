using System.Linq;
using System.Collections.Generic;
using Redux;

namespace LostGen {
    static partial class Reducers {
        private static Pawn ProcessPawn(Pawn previous, Board world, IPawnAction action) {
            Pawn next = null;

            var push = action as Action.PushAction;
            next = push == null ? next : ProcessPushAction(next, push);

            var move = action as Action.MovePawn;
            next = move == null ? next : ProcessMove(previous, world, move);

            return next ?? previous;
        }

        private static Pawn ProcessPushAction(Pawn previous, Action.PushAction push) {
            return new Pawn(previous) {
                Actions = previous.Actions.Concat(new[] { push.ToPush })
            };
        }
        
        private static Pawn ProcessMove(Pawn previous, Board world, Action.MovePawn move) {
            var next = previous;

            // Check each potential position for impassible collisions
            Point newPosition = move.From;
            bool collisionFound = false;
            var pointsToCheck = move.IsContinuous ? Point.Line(move.From, move.To) : new[] { move.To };
            foreach (var point in pointsToCheck) {
                // Collect the footprint positions for the new point
                var footprint = previous.Footprint.Select(f => point + f);

                // Collect the collision flags of any intersecting Block
                var blockCollisions = footprint
                    .Intersect(world.Blocks.Keys)
                    .Select(p => (CollisionFlags)world.Blocks[p]);

                // Collect the collision flags of any intersecting Pawn
                var pawnCollisions = footprint
                    .Join(
                        world.Pawns.Values.Where(p => p.InstanceID != move.ID), // Make sure we don't collide with ourselves
                        f => f,
                        p => p.Position,
                        (_, collided) => collided.CollisionFlags
                    );
                // Check if the move is possible with the collected collisions
                collisionFound = blockCollisions.Intersect(pawnCollisions)
                    //.Where(b => (move.CollisionFlags & b) != 0)
                    .Any();
                if (collisionFound) {
                    newPosition = point;
                }

                return new Pawn(previous) {
                    Position = newPosition,
                    DidCollide = collisionFound
                };
            }

            return previous;
        }
    }
}
