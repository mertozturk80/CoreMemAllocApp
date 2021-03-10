using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMemAllocApp.Models
{
    public class CounterModel
    {
        // System.GC based counters
        public int Gen0CollectionCount { get; set; }
        public int Gen1CollectionCount { get; set; }
        public int Gen2CollectionCount { get; set; }
        public int Gen3CollectionCount { get; set; }

        public double PauseTimePercentageGenALL { get; set; }
        public double PauseTimePercentageGen0Gen1 { get; set; }
        public double PauseTimePercentageGen2 { get; set; }
        public double PauseTimePercentageBackGroundGEN2 { get; set; }
        public long TotalFragmentedBytes { get; set; }

        //Scenario based counters. 
        public int ScenarioRootedObjectCount { get; set; }
        public int ScenarioUNRootedObjectCount { get; set; }
        public int ScenarioThreadsStarted { get; set; }
        public int ScenarioThreadsStopped { get; set; }
        public int ScenarioExceptionCount { get; set; }
        


        public long TotalAllocatedBytes { get; set; }
        public long TotalMemory { get; set; }
        public long MemoryLoadBytes { get; set; }
        public long TotalAvailableMemoryBytes { get; set; }
        public long HighMemoryLoadThresholdBytes { get; set; }




    }
}
