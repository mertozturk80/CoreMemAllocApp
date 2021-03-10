using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMemAllocApp.Models
{
    public class ScenarioModel
    {
        public int scenarioDelayinMs { get; set; }
        public int _numberOfParallelRootTasks { get; set; }
        public int _numberOfParallelDontRootTasks { get; set; }
        public int minAlloc { get; set; }
        public int MaxAlloc { get; set; }
        public int loopCount { get; set; }
        public bool CreatePinnedObjects { get; set; }
        public int PinnedOnEveryNAllocation { get; set; }
    }
}
