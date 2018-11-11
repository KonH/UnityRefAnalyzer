# UnityRefAnalizer

[![Build status](https://ci.appveyor.com/api/projects/status/ktnlrhdxpdsay8p3?svg=true)](https://ci.appveyor.com/project/KonH/unityrefanalyzer)
[![NuGet](https://img.shields.io/nuget/v/UnityRefAnalyzer.svg)](https://www.nuget.org/packages/UnityRefAnalyzer/)

Roslyn code analyzer to find method usages on scenes. Early stage of production, issues represents progress.

You can see which methods is referenced from scenes:
![screen_1](https://raw.githubusercontent.com/KonH/UnityRefAnalyzer/master/Content/screen_1.png)

And even find objects where it referenced:
![screen_2](https://raw.githubusercontent.com/KonH/UnityRefAnalyzer/master/Content/screen_2.png)

## Summary

This project get you ability to track scene references to your methods inside your IDE. It's useful, because IDE don't now anything about such links and you can't rely only on "Find References" feature.
Example: you think that method in your class isn't used and decided to remove it. But you identify issues with that only when open one of your scene, when it actually used and inspect references.
Such problems is hard to predict in common cases.

UnityRefAnalyzer contains two parts:
- Unity-side exporter - made .json report about suck links from your assets
- Roslyn analyzer - analyze report & your sources

## Features

- Shows warnings on methods, which referenced via Inspector (in IDE)
- Show warnings on missing references in Unity log when exporting data

## Requirements

- Unity 2017.3+
- Scripting backend: experimental (.NET 4.6+)

## Installation

### NuGet

### Visual Studio

- Project/Manage NuGet Packages, find UnityRefAnalyzer, install it
- or Tools/NuGet Package Manager/Package Manager Console, 'Install-Package UnityRefAnalyzer'

### Rider

- Switch to Solution mode in explorer panel
- Project/Manage NuGet Packages, find UnityRefAnalyzer, install it

### Manual

- Open RefAnalyzer/RefAnalyzer.sln, build it
- Open you Unity project

#### Visual Studio

- References/AddAnalyzer - select RefAnalyzer/RefAnalyzer/Newtonsoft.Json.dll
- References/AddAnalyzer - select RefAnalyzer/RefAnalyzer/bin/Debug/netstandard1.3/RefAnalyzer.dll

#### Rider

- Add references to csproj file manually: `<ItemGroup><Analyzer Include=".../Newtonsoft.Json.dll"/><Analyzer Include=".../RefAnalyzer.dll"/></ItemGroup>`

## Export

To get analyzer to work, you need to collect data from assets in your Unity project.

- Import package.unitypackage
- Open Exporter window - Window/RefAnalyzer
- Click RefreshData
- When it's done, you get refs.json file in root of your project
- It represents all inspector-based links in you assets
- When you build project in IDE (and if analyzer is installed properly), you get warnings in methods definitions, which is used on scenes 
- If links on your scenes changed, you need to refresh it manually

## Debugging

### Common

- If you are changed RefAnalyzer.Shared code, you need to run RefAnalyzer/RefAnalyzer.Shared/copy_dlls_to_unity_project.sh to get this changes on Unity side
- Or you can copy DLL's manually 
- Use RefAnalizer/Export Package to update package.unitypackage after changes in exporter

### Visual Studio

- Open RefAnalyzer/RefAnalyzer.sln, run it
- Another Visual Studio instance will appear, when you open any other project, analyzer will be added automatically

### Rider

- Add references to other project manually
- Changes will be applied when you rebuid other project
