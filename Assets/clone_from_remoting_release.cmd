rd Assets\FishHunter\Plugins\Regulus\ /q /s
mkdir Assets\FishHunter\Plugins\Regulus

copy ..\..\Regulus\Projects\FishHunter\Common\bin\Release\*.dll Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Release\*.dll Assets\FishHunter\Plugins\Regulus


rd Assets\FishHunter\RemotingCode\ /q /s
mkdir Assets\FishHunter\RemotingCode

xcopy ..\..\Regulus\Projects\FishHunter\Game\*.cs Assets\FishHunter\RemotingCode /s 

pause