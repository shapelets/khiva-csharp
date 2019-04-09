#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

PATH=/usr/local/bin:/usr/lib:$PATH
LD_LIBRARY_PATH=/usr/local/lib:/usr/local/include:$LD_LIBRARY_PATH
DYLD_FALLBACK_LIBRARY_PATH=/usr/local/include:$DYLD_FALLBACK_LIBRARY_PATH
wget --no-check-certificate https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
nuget restore KhivaCsharp/KhivaCsharp.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
mono ./nuget.exe install NUnit.Runners -Version 3.0.1