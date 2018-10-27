using System.Collections.Generic;
using System.Linq;
using Redux;
using Newtonsoft.Json;

namespace LostGen {
    [JsonObject]
    public class Board {
        [JsonProperty]
        public Blocks Blocks = new Blocks();
        [JsonProperty]
        public Dictionary<uint, Pawn> Pawns = new Dictionary<uint, Pawn>();
        public Board() { }
        public Board(Board other) {
            Blocks = other.Blocks;
            Pawns = other.Pawns;
        }
        public static Redux.Store<Board> CreateStore(params Redux.Middleware<Board>[] middleware) {
            return new Store<Board>(Reducers.BoardReducer, new Board(), middleware);
        }
    }
}
