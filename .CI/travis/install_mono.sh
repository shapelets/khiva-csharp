#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.
wget --no-check-certificate https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
PATH=/usr/local/bin:$PATH
echo $LD_LIBRARY_PATH
nuget.exe restore KhivaCsharp/KhivaCsharp.sln
mono ./nuget.exe install NUnit.Runners -Version 3.0.1