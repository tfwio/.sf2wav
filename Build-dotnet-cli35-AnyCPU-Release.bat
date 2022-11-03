@echo off
call common-path
msbuild /m ".sln\\sf2wav.sln" "/t:sf2wav" "/p:Platform=Any CPU;Configuration=Release"
