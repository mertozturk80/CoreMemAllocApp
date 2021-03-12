# CoreMemAllocApp

This application is to test CoreCLR Garbage Collector features. 

We have 3 controllers: 

![image](https://user-images.githubusercontent.com/6884474/110933910-64f63480-833e-11eb-8557-44fb87dedb99.png)


ScenarioController: Launch N number of parallel threads, to allocate randomly between size of “MinAlloc” & “MaxAlloc” , “loopCount” number of times. Some of these are rooted.
Additionally you can intermittently craete Pinned objects in the heap. See sample usage here: 
![image](https://user-images.githubusercontent.com/6884474/110701319-1479bc80-8202-11eb-8b42-cf3de937a216.png)

AllocateController: Performs "allocationCount" number of allocations with "allocationSize" size of allocations of type "allocType". "afterAlloc" options gives you the option to root the objects or not.

![image](https://user-images.githubusercontent.com/6884474/110933959-750e1400-833e-11eb-8e17-7b1ed505a8f4.png)

CounterController: Return useful information during Scenario execution from the scenario & from GetGCMemoryInfo API. 

![image](https://user-images.githubusercontent.com/6884474/110934154-b69ebf00-833e-11eb-88d9-79bd15dcb3ff.png)

Ie. the "TotalAvailableMemoryBytes" is the total amount of memory visible to the system. 

![image](https://user-images.githubusercontent.com/6884474/110934267-d7671480-833e-11eb-91bc-9efb92728209.png)

Any feedback is appreciated.

