# khiva-csharp

[![License: MPL 2.0](https://img.shields.io/badge/License-MPL%202.0-brightgreen.svg)](https://github.com/shapelets/khiva-python/blob/master/LICENSE.txt)
[![Gitter chat](https://badges.gitter.im/shapelets-io/Lobby.svg)](https://gitter.im/shapelets-io/khiva-python?utm_source=share-link&utm_medium=link&utm_campaign=share-link)

| Build Documentation                                                                                                                                           | Build Linux and Mac OS                                                                                                                   |  Build Windows                                                                                                                                                                | Code Coverage                                                                                                                                                |
|:-------------------------------------------------------------------------------------------------------------------------------------------------------------:|:----------------------------------------------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| [![Documentation Status](https://readthedocs.org/projects/khiva-c/badge/?version=latest)](https://khiva-c.readthedocs.io/en/latest/?badge=latest)             | [![Build Status](https://travis-ci.org/shapelets/khiva-csharp.svg?branch=master)](https://travis-ci.org/shapelets/khiva-csharp)          | [![Build status](https://ci.appveyor.com/api/projects/status/4dhbghhrk3nblxyw/branch/master?svg=true)](https://ci.appveyor.com/project/shapelets/khiva-csharp/branch/master)  |[![Coverage Status](https://codecov.io/gh/shapelets/khiva-csharp/branch/master/graph/badge.svg)](https://codecov.io/gh/shapelets/khiva-csharp/branch/master)  |

# README #
This is the Khiva C# binding, it allows the usage of Khiva library from C#.

## License
This project is licensed under [MPL-v2](https://www.mozilla.org/en-US/MPL/2.0/). 

## Quick Summary
This C# binding called 'khiva' provides all the functionalities of the KHIVA library for time series analytics.

## Set up
In order to use this binding, you need to install Khiva library.

### Prerequisites
- C# 7.3 or later
- [Mono 5.20.1 or later](https://www.mono-project.com/download/stable/)

### Install latest version
Install latest stable version of Khiva library. Follow the steps in the "Installation" section of the [Khiva repository](https://github.com/shapelets/khiva)

To install the Khiva C# binding, we just need to include the dll in our project.

### Install any release
Install the prerequisites listed in the "Installation" section of the [Khiva library repository](https://github.com/shapelets/khiva). Download and install your selected Khiva release from [Khiva repository](https://github.com/shapelets/khiva/releases).

Install the Khiva C# binding compatible with the Khiva library installed previously. 

## Executing the tests
All tests can be executed separately, please find them in <project-root-dir>/KhivaCsharp/KhivaCsharpNUnitTest.

### Running tests on MacOS
Run the following command from the project root folder:
```bash
nunit-console KhivaCsharp/KhivaCSharpNUnitTest/bin/x64/Release/KhivaCSharpNUnitTest.dll
```
 
## Documentation
This Khiva C# binding follows the standard way of writing documentation of C# by using Sphinx.

In order to generate the documentation (in html format), run the following command under the <project-root-dir>/doc folder:
```bash
make html
```

## Contributing
The rules to contribute to this project are described [here](CONTRIBUTING.md)

[![Powered by Shapelets](https://img.shields.io/badge/powered%20by-Shapelets-orange.svg?style=flat&colorA=E1523D&colorB=007D8A)](https://shapelets.io)

