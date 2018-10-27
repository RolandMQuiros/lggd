using System.Linq;
using Redux;

namespace LostGen {
    public static class Selectors {
        public static Pawn PawnByCharacterID(this Board world, string characterID) {
            return world.Pawns.Values.Where(p => p.CharacterID == characterID).FirstOrDefault();
        }
    }
}
