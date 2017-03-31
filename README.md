# os-smog-api

![img](https://avatars3.githubusercontent.com/u/25404263?v=3&s=200)

The **.NET** implementation of the data ingress API for OpenSmog.

**Build Status**

|Branch   |Status|
|---------|------|
feature/* |![badge](https://opensmog-net.visualstudio.com/_apis/public/build/definitions/dbf362cf-6d45-4160-8ea6-622363ba1a82/7/badge)
dev/master|![badge](https://opensmog-net.visualstudio.com/_apis/public/build/definitions/dbf362cf-6d45-4160-8ea6-622363ba1a82/8/badge)

## Building the project

In order to build the project on your local machine, apply the following commands:

* `bootstrap.cmd` to download the required build framework scaffolding (CAKE)
* `tools/Cake/Cake.exe ./build.cake --experimental --target=Build`

**Supported Build Targets**
- Clean
- Restore
- Build *Performs Clean & Restore*
- UnitTests *Requires Build*
- IntegrationTests *Requires Build*
- Publish *Publishes the OS.Smog.Api artifacts*

**Additional arguments**
- target *The CAKE target, defaults to Build*
- configuration *The build configuration: [Debug|Release]*
- platform *The build platform: [x86|x64|Any CPU]*
- buildNumber *The CI build number, used for emitting NuGet package artifacts*
- branch *The GIT branch name, used for emitting NuGet package artifacts*