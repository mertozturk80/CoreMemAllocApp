using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CoreMemAllocApp.Controllers
{
    public static class ScenarioCounters
    {
        public static int RootedByteArrayCount = 0;
        public static int UnRootedByteArrayCount = 0;

        public static int ScenarioThreadsStarted = 0;
        public static int ScenarioThreadsStopped = 0;

        public static int ScenarioExceptionCount = 0;

    }

    [Route("api/scenario")]
    [ApiController]
    public class ScenarioController : Controller
    {

        private static Dictionary<object, object> scenarioRoot = new Dictionary<object, object>();
        private readonly object ScenarioArrayLock = new object();
        private static bool CreatePinnedObj = false;
        private static int CratePinnedObjEveryNAlloc = 1000;

        // Definition: This function create bytearrays in memory. ByteArrays are created paralelly on several threads
        // with a random size between [minAlloc] & [maxAlloc] sizes. 
        //----------- Parameters Passed ----------------------------------------------------------------------
        //ScenarioModel.scenarioDelayinMs : Put n miliseconds of delay between each allocation inside scenario.
        //ScenarioModel._numberOfParallelRootTasks: Number of threads to be spinned up, for the scenario allocations which will be rooted.
        //ScenarioModel._numberOfParallelDontRootTasks :  Number of threads to be spinned up, for the scenario allocations which will not be rooted.
        //ScenarioModel.minAlloc : Minimum size of allocation to be done, during scenario.
        //ScenarioModel.MaxAlloc : Maximum size of allocation to be dont, during scenario.
        //ScenarioModel.loopCount : Each allocation thread will do as many allocations as this number. 
        //ScenarioModel.CreatePinnedObjects : Shall we create pinned objects in between allocations? 
        //ScenarioModel.PinnedOnEveryNAllocation : If we create pinned objects, we will create it on each of N'th allocation.

        [HttpPost]
        [ActionName("Alloc")]
        public void AllocateScenario(Models.ScenarioModel _scneario)
        {
            System.Random rnd = new System.Random(1923);
            CreatePinnedObj = _scneario.CreatePinnedObjects;
            CratePinnedObjEveryNAlloc = _scneario.PinnedOnEveryNAllocation;

            var tasks = new List<Task>();

            for (int t = 0; t < _scneario._numberOfParallelRootTasks; t++)
             tasks.Add( new Task(() => ScenarioRootByteArrays(rnd.Next(), _scneario.loopCount, _scneario.minAlloc, _scneario.MaxAlloc, _scneario.scenarioDelayinMs)));

            for (int t = 0; t < _scneario._numberOfParallelDontRootTasks; t++)
                tasks.Add(new Task(() => ScenarioDontRootByteArrays(rnd.Next(), _scneario.loopCount, _scneario.minAlloc, _scneario.MaxAlloc, _scneario.scenarioDelayinMs)));
            // Fire & Forget..
            Parallel.ForEach(tasks, task =>
             {
                    task.Start();
             });
        }

        //Intentionally using Byte Arrays since sizes approximates to the actual array size. Char arrays are like twice the size.
        internal void ScenarioRootByteArrays(int _randomFeed, int _iterationCount, int minAlloc, int maxAlloc , int delay)
        {

            System.Random rnd = new System.Random(_randomFeed);
            int AllocSize = 0;
            ScenarioCounters.ScenarioThreadsStarted++;

            for (int i = 0; i < _iterationCount; i++)
            {
                try
                {
                    Guid obj = Guid.NewGuid();

                    AllocSize = rnd.Next(minAlloc, maxAlloc);

                    Byte[] newAllocArr;
                    newAllocArr = new byte[AllocSize];

                    //Checked if pinning is enabled. 
                    if (CreatePinnedObj)
                        if (i % CratePinnedObjEveryNAlloc == 0)
                        { GCHandle handle = GCHandle.Alloc(newAllocArr, GCHandleType.Pinned); }

                    //fillin the array
                    for (int j = 0; j < AllocSize; j++) newAllocArr[j] = Convert.ToByte(j%256);
                   
                   if (delay > 0)
                        System.Threading.Thread.Sleep(delay);

                    lock (ScenarioArrayLock)
                    {
                        scenarioRoot.Add(obj, newAllocArr);
                    }
                    ScenarioCounters.RootedByteArrayCount++;
                }
                catch (Exception ex)
                {
                    ScenarioCounters.ScenarioExceptionCount++;
                }
            }

            ScenarioCounters.ScenarioThreadsStopped++;
        }

        internal void ScenarioDontRootByteArrays(int _randomFeed, int _iterationCount, int minAlloc, int maxAlloc, int delay)
        {
            System.Random rnd = new System.Random(_randomFeed);
            int AllocSize = 0;
            ScenarioCounters.ScenarioThreadsStarted++;

            for (int i = 0; i < _iterationCount; i++)
            {
                try
                {
                    AllocSize = rnd.Next(minAlloc, maxAlloc);
                    //allocate
                    Byte[] newAllocArr = new byte[AllocSize];
                    //Checked if pinning is enabled. 
                    if (CreatePinnedObj)
                        if (i % CratePinnedObjEveryNAlloc == 0)
                        { GCHandle handle = GCHandle.Alloc(newAllocArr, GCHandleType.Pinned); }

                    //fillin the array
                    for (int j = 0; j < AllocSize; j++) newAllocArr[j] = newAllocArr[j] = Convert.ToByte(j % 256);
                    if (delay > 0)
                        System.Threading.Thread.Sleep(delay);
                    //scenarioRoot.Add(scenarioRootKeyIndex++, newAllocArr);
                    ScenarioCounters.UnRootedByteArrayCount++;
                }
                catch (Exception ex)
                {
                    ScenarioCounters.ScenarioExceptionCount++;
                }
            }

            ScenarioCounters.ScenarioThreadsStopped++;
        }
    }

}
