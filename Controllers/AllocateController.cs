using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMemAllocApp.Models;

namespace CoreMemAllocApp.Controllers
{

    [Route("api/Allocate")]
    [ApiController]
    public class AllocateController : ControllerBase
    {
        private static Dictionary<object, object> allocationRoot;
        private static int rootKeyIndex = 0;

        public AllocateController()
        {
            allocationRoot = new Dictionary<object, object>();
        }
        [HttpPost]
        [ActionName("Alloc")]
        //AllocationModel.AllocationCount : Number of Allocations
        //AllocationModel.AllocationSize : Size of individual allocation
        //AllocationModel.DelayBetweenAllocations : Put n miliseconds delay between each allocation.
        //AllocationModel.AllocType : Type of the allocation. (String = 0,Char = 1,Integer = 2)
        //AllocationModel.AfterAlloc :  Shall we root the objects? (DontRootObjects = 0,RootObjects = 1)
        public string Alloc(AllocationModel _model)
        {
            string return_results = "";
            char newAllocChar = '@';

            int i, j;
            if (_model.AfterAlloc == AfterAllocation.RootObjects)
            {
                for (j = 0; j < _model.AllocationCount; j++)
                {
                    switch (_model.AllocType)
                    {
                        case AllocationType.String:
                            string new_str = new string(newAllocChar, _model.AllocationSize);
                            allocationRoot.Add(rootKeyIndex++, new_str);
                            break;
                        case AllocationType.Char:
                            char[] newAllocArr = new char[_model.AllocationSize];
                            for (i = 0; i < _model.AllocationSize; i++) newAllocArr[i] = (char)Byte.Parse((i % 256).ToString());
                            allocationRoot.Add(rootKeyIndex++, newAllocArr);
                            break;
                        case AllocationType.Integer:
                            allocationRoot.Add(rootKeyIndex++, new int[_model.AllocationSize]);
                            break;
                        default:
                            break;
                    };
                }

            }
            else if (_model.AfterAlloc == AfterAllocation.DontRootObjects)
            {
                for (j = 0; j < _model.AllocationCount; j++)
                {
                    switch (_model.AllocType)
                    {
                        case AllocationType.String:
                            string new_str = new string(newAllocChar, _model.AllocationSize);
                            break;

                        case AllocationType.Char:
                            char[] newAllocArr = new char[_model.AllocationSize];
                            newAllocArr = new char[_model.AllocationSize];
                            for (i = 0; i < _model.AllocationSize; i++) newAllocArr[i] = (char)Byte.Parse((i % 256).ToString());
                            break;

                        case AllocationType.Integer:
                            int[] new_IntArr = new int[_model.AllocationSize];
                            break;

                        default:
                            break;
                    };
                }
            }
            else
            {
                return_results = "Incorrect Parameters";

            }
            return_results = "\nAllocated " + _model.AllocType.ToString() + " number of " + _model.AllocType.ToString() + "objects. Root Key Index is now: " + rootKeyIndex.ToString();

            return return_results;
        }

    }
}
