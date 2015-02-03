del Assets\FishHunter\Plugins\Regulus\*.* /s /q 

copy ..\..\Regulus\Projects\FishHunter\Common\bin\Debug\*.dll Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Debug\*.dll Assets\FishHunter\Plugins\Regulus


del Assets\FishHunter\RemotingCode\*.* /s /q
copy ..\..\Regulus\Projects\FishHunter\Game\*.cs Assets\FishHunter\RemotingCode

pause