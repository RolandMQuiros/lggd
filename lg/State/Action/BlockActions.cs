using System.Collections.Generic;
using Redux;

namespace LostGen {
    public interface IBlockAction : IAction {
        IEnumerable<Point> Points { get; set; }
    }

    namespace Action {
        
    }
}
