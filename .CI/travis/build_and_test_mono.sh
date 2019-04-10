#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

nuget restore KhivaCsharp/KhivaCsharp.sln
msbuild /p:Configuration=Debug KhivaCsharp/KhivaCsharp.sln
ls ./KhivaCsharp/packages/NUnit.ConsoleRunner.3.9.0
./KhivaCsharp/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe -register:user -target:"./KhivaCsharp/packages/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe" -targetargs:"KhivaCsharp/KhivaCSharpNUnitTest/bin/x64/Debug/KhivaCSharpNUnitTest.dll" -output:"coverage.xml" -filter:"+[*]khiva.*"