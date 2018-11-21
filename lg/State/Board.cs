using System.Collections.Generic;
using System.Linq;
using Redux;
using Newtonsoft.Json;

namespace LostGen {
    [JsonObject]
    public class Board {
        [JsonProperty]
        public readonly Dictionary<Point, Block> Blocks = new Dictionary<Point, Block>();
        [JsonProperty]
        public readonly Dictionary<uint, Pawn> Pawns = new Dictionary<uint, Pawn>();

        public Board() { }

        public Board(Dictionary<Point, Block> blocks, Dictionary<uint, Pawn> pawns) {
            Blocks = blocks;
            Pawns = pawns;
        }

        public Board(Board other) {
            Blocks = other.Blocks;
            Pawns = other.Pawns;
        }
        public static Redux.Store<Board> CreateStore(params Redux.Middleware<Board>[] middleware) {
            return new Store<Board>(Reducers.BoardReducer, new Board(), middleware);
        }
    }
}
