# Cloo
Cloo `OpenCL`&trade; library with nuget package deployment ported to netstandard2.0 + sample apps.

## Dependencies
1. OpenCL drivers. Depending on your system, you can obtain them from your graphics device manufacturer website or operating system vendor website. Newest drivers for popular GPUs should be fine.
1. The Microsoft .NET Framework 4.7.2 SDK.
1. Visual Studio 2017 (15.9.4) with .NET core support enabled (or VSCode for dotnet core example only)
1. .NET Core 2.2 SDK installed

## Projects
- **Cloo** - `netstandard2.0` library with configured build producing nuget package
- **ClootilsNetFull** - `net472` winforms application sample
- **ClootilsNetCore** - `netcoreapp2.2` console application using linq-style extension

## Usage

### Visual Studio 2017
- Check if .NET Core support installed in VS2017 Installer
- Install .NET Core 2.2 SDK
- Install .NET Framework 4.7.2 SDK
- Open solution file `*.sln`, select startup project (full or core) and press `F5`

### VS Code
- Install `C#` Extension
- Install .NET Core 2.2 SDK
- Open solution's folder
- Press `F5` to run the core app

## Remarks
- Fork from https://github.com/clSharp/Cloo, which is VS2010 .NET Framework 2.0 / Mono project
- Tested on `Windows 10`&trade; with `AMD`&trade; and `Intel`&trade; GPUS.
- Not tested with Mono yet, but [should be doable](https://stackoverflow.com/questions/48061333/using-net-standard-2-0-with-mono)
