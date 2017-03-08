#load ./build/settings.cake

Task(Build).Does(() => {
    DotNetBuild("./OS.Smog.Api.sln");
});

RunTarget(target);