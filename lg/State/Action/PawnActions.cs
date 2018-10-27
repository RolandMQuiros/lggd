using Newtonsoft.Json;
using Redux;

namespace LostGen {
    public interface IPawnAction : IAction {
        uint ID { get; set; }
    }

    namespace Action {
        [JsonObject]
        public class MovePawn : IPawnAction {
            [JsonProperty]public uint ID { get; set; }
            [JsonProperty]public Point From;
            [JsonProperty]public Point To;
            [JsonProperty]public CollisionFlags CollisionFlags;
            [JsonProperty]public bool IsContinuous;
        }

        [JsonObject]
        public class PushAction : IPawnAction {
            [JsonProperty]public uint ID { get; set; }
            [JsonProperty]public IAction ToPush;
        }
    }
}
