# Cloo
Cloo `OpenCL`&trade; library with nuget package deployment ported to netstandard2.0 + sample apps.

[![Build Status](https://travis-ci.org/clSharp/Cloo.svg?branch=master)](https://travis-ci.org/clSharp/Cloo)
## Dependencies
1. OpenCL drivers. Depending on your system, you can obtain them from your graphics device manufacturer website or operating system vendor website. Newest drivers for popular GPUs should be fine.
1. The Microsoft .NET Framework 4.8 SDK.
1. Visual Studio 2019 with .NET core support enabled (or VSCode for dotnet core example only)
1. .NET Core 2.0 SDK installed

## Projects
- **Cloo** - `netstandard2.0` library with configured build producing nuget package
- **ClootilsNetFull** - `netcoreapp3.0` winforms application sample
- **ClootilsNetCore** - `netcoreapp3.0` console application using linq-style extension
- **ClootilsNetCoreUI** - `netcoreapp3.0` ui application based on `Avalonia` (WPF-like)

## Usage

### Visual Studio 2017
- Check if .NET Core support installed in VS2019 Installer
- Install .NET Core 3.0 SDK
- Install .NET Framework 4.8 SDK
- Open solution file `*.sln`, select startup project (full or core) and press `F5`

### VS Code
- Install `C#` Extension
- Install .NET Core 3.0 SDK
- Open solution's folder
- Press `F5` to run the core app

## Remarks
- Tested on `Windows 10`&trade; and `Linux` with `AMD`&trade;, `Nvidia`&trade; and `Intel`&trade; platforms.
