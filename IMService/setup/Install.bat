@echo.��������......  
@echo off  
@sc create IMService binPath= "D:\Work\01-Projects\IM\Td.Kylin.IMServices\IMService\bin\Debug\IMService.exe"  
@net start IMService  
@sc description IMService "Kylin IM��������"
@sc config IMService start= AUTO  
@echo off  
@echo.������ϣ�  
@pause  