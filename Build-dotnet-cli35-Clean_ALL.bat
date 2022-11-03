@echo off
call common-path
msbuild /m ".sln\\sf2wav.sln" "/t:sf2wav:Clean" "/p:Platform=Any CPU;Configuration=Debug"
msbuild /m ".sln\\sf2wav.sln" "/t:sf2wav:Clean" "/p:Platform=Any CPU;Configuration=Release"
