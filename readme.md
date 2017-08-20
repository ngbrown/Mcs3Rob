Mcs3Rob
=======

[![Build status](https://ci.appveyor.com/api/projects/status/k6e4n059rv7h10vg/branch/master?svg=true)](https://ci.appveyor.com/project/ngbrown/mcs3rob/branch/master) [![NuGet](https://img.shields.io/nuget/v/Mcs3Rob.svg)](https://www.nuget.org/packages/Mcs3Rob/)

A .ROB file parser written in C# for the .NET family of languages.  A .ROB file is written in a structured ASCII format to describe the internal electronic control unit (ECU) variables used in measurement and calibration of an engine, such as that in an automotive vehicle.


# Downloads

The latest release of Mcs3Rob is [available on NuGet](https://www.nuget.org/packages/Mcs3Rob/).


# Usage

```cs
using System;
using Mcs3Rob;

void Main()
{
    var parser = new Mcs3Rob.Parser();
    parser.Error += (sender, args) => Console.WriteLine(args.ToString());
    var robFile = parser.Read("cal-file-4345.rob");
}
```


# Contributors

Mcs3Rob is &copy; 2017 [Nathan Brown](https://github.com/ngbrown) under the [MIT license](https://github.com/ngbrown/Mcs3Rob/blob/master/LICENSE.txt)

