# UnityRefAnalizer

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

## Installation

### Manual

#### Visual Studio

- Open RefAnalyzer/RefAnalyzer.sln, build it
- Open you Unity project
- References/AddAnalyzer - select RefAnalyzer/RefAnalyzer/Newtonsoft.Json.dll
- References/AddAnalyzer - select RefAnalyzer/RefAnalyzer/bin/Debug/netstandard1.3/RefAnalyzer.dll

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

### Visual Studio

- Open RefAnalyzer/RefAnalyzer.sln, run it
- Another Visual Studio instance will appear, when you open any other project, analyzer will be added automatically
