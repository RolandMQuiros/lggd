using System.Linq;
using System.Collections.Generic;
using Redux;

namespace LostGen {
    public class Pawn {
        public uint InstanceID;
        public string CharacterID;
        public Point Position;
        public CollisionFlags CollisionFlags;
        public bool DidCollide = false;
        public HashSet<Point> Footprint = new HashSet<Point>();
        public IEnumerable<IAction> Actions = Enumerable.Empty<IAction>();
        public int StepPriority = 0;

        public Pawn() { }
        public Pawn(Pawn other) {
            InstanceID = other.InstanceID;
            CharacterID = other.CharacterID;
            CollisionFlags = other.CollisionFlags;
            DidCollide = false;
            Position = other.Position;
            Footprint = other.Footprint;
            Actions = other.Actions;
            StepPriority = other.StepPriority;
        }
    }
}
