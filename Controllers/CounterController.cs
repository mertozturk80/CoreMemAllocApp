using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using System.Diagnostics;
using System.Diagnostics.Tracing;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CoreMemAllocApp.Controllers
{
    [Route("api/Counter")]
    [ApiController]
    public class CounterController
    {
    
        [HttpGet]
        public Models.CounterModel GetCounters()
        {
         

            Models.CounterModel return_model = new Models.CounterModel();

            // Number Of Collections.
            return_model.Gen0CollectionCount = System.GC.CollectionCount(0);
            return_model.Gen1CollectionCount = System.GC.CollectionCount(1);
            return_model.Gen2CollectionCount = System.GC.CollectionCount(2);
            return_model.Gen3CollectionCount = System.GC.CollectionCount(3);

            //Pause Time Percentages per Generation. 
            //Any --> Any kind of collection.
            //Background (3) --> A background collection.This is always a generation 2 collection.
            //Ephemeral (1) --> A gen0 or gen1 collection.
            //FullBlocking(2) --> A blocking gen2 collection.
            // For more information: https://devblogs.microsoft.com/dotnet/the-updated-getgcmemoryinfo-api-in-net-5-0-and-how-it-can-help-you/

#if NET5_0_OR_GREATER

            return_model.PauseTimePercentageGenALL = System.GC.GetGCMemoryInfo(GCKind.Any).PauseTimePercentage;
            return_model.PauseTimePercentageGen0Gen1 = System.GC.GetGCMemoryInfo(GCKind.Ephemeral).PauseTimePercentage;
            return_model.PauseTimePercentageGen2 = System.GC.GetGCMemoryInfo(GCKind.FullBlocking).PauseTimePercentage;
            return_model.PauseTimePercentageBackGroundGEN2 = System.GC.GetGCMemoryInfo(GCKind.Background).PauseTimePercentage;

            //Total Fragmentation for ANY Collections.
            return_model.TotalFragmentedBytes = System.GC.GetGCMemoryInfo(GCKind.Any).FragmentedBytes;


#else 
            //Total Fragmentation for ANY Collections.
            return_model.TotalFragmentedBytes = System.GC.GetGCMemoryInfo().FragmentedBytes;
#endif

            //How many allocations are made so far..
            return_model.ScenarioRootedObjectCount = ScenarioCounters.RootedByteArrayCount;
            return_model.ScenarioUNRootedObjectCount = ScenarioCounters.UnRootedByteArrayCount;
            
            //How many scenario threads started & finished.. How many exceptions have been encountered.
            return_model.ScenarioThreadsStarted = ScenarioCounters.ScenarioThreadsStarted;
            return_model.ScenarioThreadsStopped = ScenarioCounters.ScenarioThreadsStopped;
            return_model.ScenarioExceptionCount = ScenarioCounters.ScenarioExceptionCount;

        

            return_model.TotalAllocatedBytes = System.GC.GetTotalAllocatedBytes(false);
            return_model.TotalMemory = System.GC.GetTotalMemory(false);
            return_model.MemoryLoadBytes = System.GC.GetGCMemoryInfo().MemoryLoadBytes;
            return_model.HighMemoryLoadThresholdBytes = System.GC.GetGCMemoryInfo().HighMemoryLoadThresholdBytes;
            return_model.TotalAvailableMemoryBytes = System.GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;


            return return_model;

        }
    }

}
