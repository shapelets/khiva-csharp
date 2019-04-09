#!/bin/bash
# Copyright (c) 2019 Shapelets.io
#
# This Source Code Form is subject to the terms of the Mozilla Public
# License, v. 2.0. If a copy of the MPL was not distributed with this
# file, You can obtain one at http://mozilla.org/MPL/2.0/.

PATH=/usr/local/bin:$PATH
nuget restore KhivaCsharp/KhivaCsharp.sln
curl -sSLO https://github.com/codecov/enterprise/releases/download/v4.1.3.zip
unzip v4.1.3.zip && rm v4.1.3.zip && mv ./linux/codecov ./