del Assets\Plugins\Regulus\*.* /s /q 

copy ..\..\Regulus\Projects\FishHunter\Common\bin\Debug\*.dll Assets\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Debug\*.dll Assets\Plugins\Regulus


del Assets\RemotingCode\*.* /s /q
copy ..\..\Regulus\Projects\FishHunter\Game\*.cs Assets\RemotingCode

pause