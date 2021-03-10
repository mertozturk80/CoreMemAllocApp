using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMemAllocApp.Models
{
    public enum AfterAllocation
    {
        DontRootObjects = 0,
        RootObjects = 1
    };

    public enum AllocationType
    {
        String = 0,
        Char = 1,
        Integer = 2
    };

    public class AllocationModel 
    {
        public int AllocationCount{ get; set; }
        public int AllocationSize { get; set; }
        public int DelayBetweenAllocations { get; set; }
        public AllocationType AllocType { get; set; }
        public AfterAllocation AfterAlloc { get; set; }
    }
}
