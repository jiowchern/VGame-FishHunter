rd Assets\FishHunter\Plugins\Regulus\ /q /s
mkdir Assets\FishHunter\Plugins\Regulus

copy ..\..\Regulus\Projects\FishHunter\Common\bin\Debug\*.dll Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Debug\*.dll Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\Common\bin\Debug\*.pdb Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Debug\*.pdb Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\Common\bin\Debug\*.xml Assets\FishHunter\Plugins\Regulus
copy ..\..\Regulus\Projects\FishHunter\User\bin\Debug\*.xml Assets\FishHunter\Plugins\Regulus



rd Assets\FishHunter\RemotingCode\ /q /s
mkdir Assets\FishHunter\RemotingCode

xcopy ..\..\Regulus\Projects\FishHunter\Game\*.cs Assets\FishHunter\RemotingCode /s 

pause