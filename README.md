# CoreMemAllocApp

This application is to test CoreCLR Garbage Collector features. 
Three controllers: 
ScenarioController: Launch N number of parallel threads, to allocate randomly between size of “MinAlloc” & “MaxAlloc” , “loopCount” number of times. Some of these are rooted.
AllocateController: Simpler than above.
CounterController: Return useful information during Scenario execution from the scenario & from GetGCMemoryInfo API. 

A simple input to Scenario Controller: 

![image](https://user-images.githubusercontent.com/6884474/110701319-1479bc80-8202-11eb-8b42-cf3de937a216.png)
