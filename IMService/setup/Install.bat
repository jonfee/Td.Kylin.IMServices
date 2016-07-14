@echo.服务启动......  
@echo off  
@sc create IMService binPath= "D:\Work\01-Projects\IM\Td.Kylin.IMServices\IMService\bin\Debug\IMService.exe"  
@net start IMService  
@sc description IMService "Kylin IM服务中心"
@sc config IMService start= AUTO  
@echo off  
@echo.启动完毕！  
@pause  