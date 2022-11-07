// #tool "nuget:?package=xunit.runner.console"
#addin "Cake.WebDeploy"
#addin "Cake.Powershell"
#addin "Cake.Npm"


// Setup(context => {
//     context.Tools.RegisterFile("D:/Program Files (x86)/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin");
// });


var target = Argument ("target", "Default");


// var target = Argument ("target", "Dev1");
// var target = Argument ("target", "TestBuild");

// var target = Argument ("target", "unit_test_event");

// var target = Argument ("target", "AspCoreDeploy");

// target = Argument("target", "Test");


string msBuildPath1 = "";
var isCustom1 = Argument<bool>("custom_msbuild", false);
if (isCustom1){
  msBuildPath1 = @"E:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\MSBuild.exe";
  Information ("Use Custom MS Build-1");
}

var isCustomDev1 = Argument<bool>("custom_msbuild_dev", false);
if (isCustomDev1){
  msBuildPath1 = @"G:\Program Files (x86)/Microsoft Visual Studio/Preview/Community/MSBuild/15.0/Bin\MSBuild.exe";
  Information ("Use Custom Dev MS Build-1");
}

Task ("TestBuild")
  .Does (() => {

      
      var msbuildSettings = new MSBuildSettings ()
      // .WithProperty ("TreatWarningsAsErrors", "true")
      .SetMaxCpuCount (0)
      .WithTarget ("Build")
      .SetConfiguration ("release");
      // msbuildSettings.ToolVersion = MSBuildToolVersion.VS2017;

      string msBuildPath = @"D:/Program Files (x86)/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin/MSBuild.exe";
      // string msBuildPath = @"C:/msbuild_mklink/MSBuild.exe";

      msbuildSettings.ToolPath = msBuildPath;

      var url = "./ExchangeLemon/LogicLibrary/LogicLibrary.csproj";
      url = "./ExchangeLemon/ExchangeLemonCore/ExchangeLemonCore.csproj";
      
      MSBuild (url, msbuildSettings);
  });

Task ("Dev1")
  .Does (() => {
      Information ("Hello World!");

      var msbuildSettings = new MSBuildSettings ()
        // .WithProperty ("TreatWarningsAsErrors", "true")
        .SetMaxCpuCount (0)
        .WithTarget ("Build")
        .SetConfiguration ("release");
        
      var isCustom = Argument<bool>("custom_msbuild", false);
      if (isCustom){
        string msBuildPath = @"D:/Program Files (x86)/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin/MSBuild.exe";
        msbuildSettings.ToolPath = msBuildPath;
        Information ("Use Custom MS Build");
        Information(msBuildPath);
      }

      var configuration = Argument ("Configuration", "Release");
     
  
      var url  ="";


      // url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
      // MSBuild (url, msbuildSettings);
      // DotNetCoreTool (
      //   projectPath: url,
      //   command: "test",
      //   arguments: ""
      // );

       url = "./ExchangeLemon/UnitTestSyncBot/UnitTestSyncBot.csproj";
      // MSBuild (url, msbuildSettings);
      DotNetCoreTool (
        projectPath: url,
        command: "test",
        arguments: ""
        // command: "xunit",
        // arguments: $"-configuration {configuration} -diagnostics -stoponfail"
      );
  });
  

Task ("Deploy")
  .Does (() => {
    Information ("Hello World! - Deploy");
    NpmInstall();
    var configuration = Argument ("Configuration", "Release");
       var msbuildSettings = new MSBuildSettings ()
      // .WithProperty ("TreatWarningsAsErrors", "true")
      .SetMaxCpuCount (0)
      .WithTarget ("Build")
      .SetConfiguration ("release");

      // string msBuildPath = @"D:/Program Files (x86)/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin/MSBuild.exe";
      // msbuildSettings.ToolPath = msBuildPath;

    var url = "";

    url = "./ExchangeLemon/CodingLiteCore.sln";
    NuGetRestore (url);
    
    url = "./ExchangeLemon/ExchangeLemonCore/ExchangeLemonCore.csproj";
    MSBuild (url, msbuildSettings);
});
Task ("Basic")
  .Does (() => {
    Information ("Hello World!");
    NpmInstall();
    // StartProcess ("cmd","npm install");
    var configuration = Argument ("Configuration", "Release");
    var url = "";

    var msbuildSettings = new MSBuildSettings ()
      // .WithProperty ("TreatWarningsAsErrors", "true")
      .SetMaxCpuCount (0)
      .WithTarget ("Build")
      .SetConfiguration ("release");


    var isCustom = Argument<bool>("custom_msbuild", false);
    if (isCustom){
      string msBuildPath = @"D:/Program Files (x86)/Microsoft Visual Studio/2017/BuildTools/MSBuild/15.0/Bin/MSBuild.exe";
      
      
      msbuildSettings.ToolPath = msBuildPath;
      Information ("Use Custom MS Build");
    }

    url = "./ExchangeLemon/CodingLiteCore.sln";
    NuGetRestore (url);

    url = "./ExchangeLemon/CodingLiteCoreWatcher.sln";
    NuGetRestore (url);
    

    // var url3 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // NuGetRestore (url3);

    // var url2 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // MSBuild (url2, msbuildSettings);

    //     var url1 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // DotNetCoreTool (
    //   projectPath: url1,
    //   command: "test",
    //   arguments: ""
    // );

    // url = "./ExchangeLemon/DeploymentFunctionAppBtc.sln";
    // NuGetRestore (url);

    // url = "./ExchangeLemon/DeploymentProjectSyncRate.sln";
    // NuGetRestore (url);

    // url = "./ExchangeLemon/DeploymentProjectGekko.sln";
    // NuGetRestore (url);

    // var msbuildSettings = new MSBuildSettings ()
    //   // .WithProperty ("TreatWarningsAsErrors", "true")
    //   .SetMaxCpuCount (0)
    //   .WithTarget ("Build")
    //   .SetConfiguration ("release");

    url = "./ExchangeLemon/LogicLibrary/LogicLibrary.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/ExchangeLemonCore/ExchangeLemonCore.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/FunctionAppBitcoin/FunctionAppBitcoin.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/BotWalletWatcher/BotWalletWatcher.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/FunctionAppWatcher/FunctionAppWatcher.csproj";
    MSBuild (url, msbuildSettings);

    // url = "./ExchangeLemon/ExchangeLemonSyncBotCore/ExchangeLemonSyncBotCore.csproj";
    // MSBuild (url, msbuildSettings);

    // url = "./ExchangeLemon/ExchangeServerGekkoCore/ExchangeServerGekkoCore.csproj";
    // MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/UnitTest/UnitTest.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: $""
    );

    url = "./ExchangeLemon/UnitTestSyncBot/UnitTestSyncBot.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
      // command: "xunit",
      // arguments: $"-configuration {configuration} -diagnostics -stoponfail"
    );

    url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
    );

    url = "./ExchangeLemon/UnitTestWatcher/UnitTestWatcher.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
    );


    var platform = Environment.OSVersion.Platform;
    if (platform.ToString () != "Unix") {
      url = "./ExchangeLemon/UnitTestWebVisit/UnitTestWebVisit.csproj";
      MSBuild (url, msbuildSettings);
      // DotNetCoreTool (
      //   projectPath: url,
      //   command: "test",
      //   arguments: ""
      // );
    }

    // url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // DotNetCoreTool (
    //   projectPath: url,
    //   command: "xunit",
    //   arguments: $"-configuration {configuration} -diagnostics -stoponfail"
    // );

  });


  Task ("Main")
  .Does (() => {
    Information ("Hello World!");
    NpmInstall();
    // StartProcess ("cmd","npm install");
    var configuration = Argument ("Configuration", "Release");
    var url = "";

    var msbuildSettings = new MSBuildSettings ()
      // .WithProperty ("TreatWarningsAsErrors", "true")
      .SetMaxCpuCount (0)
      .WithTarget ("Build")
      .SetConfiguration ("release");

    //TODO:1
    if (msBuildPath1 != ""){
      msbuildSettings.ToolPath = msBuildPath1;
      Information ("Use Custom MS Build-2");
      Information (msBuildPath1);
    }

    url = "./ExchangeLemon/CodingLiteCore.sln";
    NuGetRestore (url);

    // var url3 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // NuGetRestore (url3);

    // var url2 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // MSBuild (url2, msbuildSettings);

    //     var url1 = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // DotNetCoreTool (
    //   projectPath: url1,
    //   command: "test",
    //   arguments: ""
    // );

    // url = "./ExchangeLemon/DeploymentFunctionAppBtc.sln";
    // NuGetRestore (url);

    // url = "./ExchangeLemon/DeploymentProjectSyncRate.sln";
    // NuGetRestore (url);

    // url = "./ExchangeLemon/DeploymentProjectGekko.sln";
    // NuGetRestore (url);

    // var msbuildSettings = new MSBuildSettings ()
    //   // .WithProperty ("TreatWarningsAsErrors", "true")
    //   .SetMaxCpuCount (0)
    //   .WithTarget ("Build")
    //   .SetConfiguration ("release");

    url = "./ExchangeLemon/LogicLibrary/LogicLibrary.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/ExchangeLemonCore/ExchangeLemonCore.csproj";
    MSBuild (url, msbuildSettings);

    url = "./ExchangeLemon/FunctionAppBitcoin/FunctionAppBitcoin.csproj";
    MSBuild (url, msbuildSettings);

    // url = "./ExchangeLemon/ExchangeLemonSyncBotCore/ExchangeLemonSyncBotCore.csproj";
    // MSBuild (url, msbuildSettings);

    // url = "./ExchangeLemon/ExchangeServerGekkoCore/ExchangeServerGekkoCore.csproj";
    // MSBuild (url, msbuildSettings);

  });



  Task("Testing").Does ( () => {
  

  var msbuildSettings = new MSBuildSettings ()
      // .WithProperty ("TreatWarningsAsErrors", "true")
      .SetMaxCpuCount (0)
      .WithTarget ("Build")
      .SetConfiguration ("release");

    //TODO:1
    if (msBuildPath1 != ""){
      msbuildSettings.ToolPath = msBuildPath1;
      Information ("Use Custom MS Build-2");
    }

    var url = "";
    url = "./ExchangeLemon/UnitTest/UnitTest.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: $""
    );

    url = "./ExchangeLemon/UnitTestSyncBot/UnitTestSyncBot.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
      // command: "xunit",
      // arguments: $"-configuration {configuration} -diagnostics -stoponfail"
    );

    url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    MSBuild (url, msbuildSettings);
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
    );


    var platform = Environment.OSVersion.Platform;
    if (platform.ToString () != "Unix") {
      url = "./ExchangeLemon/UnitTestWebVisit/UnitTestWebVisit.csproj";
      MSBuild (url, msbuildSettings);
      // DotNetCoreTool (
      //   projectPath: url,
      //   command: "test",
      //   arguments: ""
      // );
    }

    // url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    // DotNetCoreTool (
    //   projectPath: url,
    //   command: "xunit",
    //   arguments: $"-configuration {configuration} -diagnostics -stoponfail"
    // );
  })
  .IsDependentOn ("Main");

  Task("unit_test_event").Does ( () => {
   var url = "./ExchangeLemon/UnitTestEventLogic/UnitTestEventLogic.csproj";
    DotNetCoreTool (
      projectPath: url,
      command: "test",
      arguments: ""
    );
  });

Task ("Default")
  .Does (() => {
    var platform = Environment.OSVersion.Platform;
    if (platform.ToString () != "Unix") {
      StartProcess ("send_message.bat");
    } else {
      StartProcess ("sh","./send_message_mac.sh");
    }
  })
  // .IsDependentOn ("Basic");;
  .IsDependentOn ("Testing");
  
Task ("Test")
  .Does (() => {
    Information ("Hello World!");

    var url = "";

    url = "./ExchangeLemon/DeploymentSignal.sln";
    NuGetRestore (url);

    url = "./ExchangeLemon/ExchangeSignal/ExchangeSignal.csproj";
    MSBuild (url, new MSBuildSettings ()
      .WithTarget ("Build")
    );

  });

var configuration = Argument ("configuration", "Release");

var siteName = "";
var password = "";
var project = "";
var sourceDirectoryPath = "";

void Build () {
  DotNetCoreBuild (
    project,
    new DotNetCoreBuildSettings () {
      Configuration = configuration,
        // ArgumentCustomization = args => args.Append("--no-restore"),
    });

  DotNetCorePublish (
    project,
    new DotNetCorePublishSettings () {
      Configuration = configuration,
        OutputDirectory = sourceDirectoryPath,
        // ArgumentCustomization = args => args.Append("--no-restore"),
    });
}

void Publish () {
  var scriptStop = "az webapp stop --name " + siteName + " --resource-group exchangeblue ";
  var scriptStart = "az webapp start --name " + siteName + " --resource-group exchangeblue ";

  StartPowershellScript (scriptStop);

  DeployWebsite (new DeploySettings () {
    SourcePath = sourceDirectoryPath,
      ComputerName = "https://" + siteName + ".scm.azurewebsites.net:443/msdeploy.axd?site=" + siteName,
      SiteName = siteName,
      Username = "$" + siteName,
      Password = password,
      // Delete = true,
      // WhatIf = true
  });
  StartPowershellScript (scriptStart);
}

Task ("AspCoreDeploy")
  .Does (() => {
    siteName = "exchangefunctionappbitcoincore";
    password = "c5NWssydPZ7eaobfWLSFDfdQBvMqc19sY0rxqWnZgwpJzjPaYkbmQeu7YjD4";
    project = "./ExchangeLemon/FunctionApp1/FunctionAppBitcoin.csproj";
    sourceDirectoryPath = "./targetPublishAspCore/FunctionAppBitcoin/";
    Build ();
    Publish ();

    siteName = "exchangekirincore";
    password = "cCi2t5KWqr3ebjwEhKXuJRku8pHhFM2xdqdZq3750Hxul78wHas61csiyxtZ";
    project = "./ExchangeLemon/ExchangeServerGekkoCore/ExchangeServerGekkoCore.csproj";
    sourceDirectoryPath = "./targetPublishAspCore/ExchangeServerGekkoCore/";
    Build ();
    Publish ();

    siteName = "exchangesyncbluecore";
    password = "f2lAm1LlP0AimpWgl3y3hxRhklH9xC1dcqQ6oM6cXvxnCYXhwM5ljeLl2Zst";
    project = "./ExchangeLemon/ExchangeLemonSyncBotCore/ExchangeLemonSyncBotCore.csproj";
    sourceDirectoryPath = "./targetPublishAspCore/ExchangeLemonSyncBotCore/";
    Build ();
    Publish ();

    siteName = "lemoncore";
    password = "KFTqwnQPr9lxABgwfdg4sp2NxYsRRzj9mKrljS0hncMGMu3ZuvMP6FLvtlji";
    project = "./ExchangeLemon/ExchangeLemonCore/ExchangeLemonCore.csproj";
    sourceDirectoryPath = "./targetPublishAspCore/ExchangeLemonCore/";
    Build ();
    Publish ();

  })
  .IsDependentOn ("Basic");

Task ("Dev").Does (() => {
  //StartProcess ("sh");
  StartProcess ("sh","./send_message_mac.sh");
  return;
  var platform = Environment.OSVersion.Platform;
  Console.WriteLine ("platform={0}", platform);
  Console.WriteLine ("int={0}", (int) platform);
  if (platform.ToString () == "Unix") {
    Console.WriteLine ("running on unix");
  }
});

RunTarget (target);

// Task ("AspCore")
//   .Does (() => {

//     Information ("Hello World! AspCore");

//     DotNetCoreBuild (
//       project,
//       new DotNetCoreBuildSettings () {
//         Configuration = configuration,
//           // ArgumentCustomization = args => args.Append("--no-restore"),
//       });

//     DotNetCorePublish (
//       project,
//       new DotNetCorePublishSettings () {
//         Configuration = configuration,
//           OutputDirectory = sourceDirectoryPath,
//           // ArgumentCustomization = args => args.Append("--no-restore"),
//       });
//   });

// Task ("AspCoreDeploy")
//   .Does (() => {

//     var scriptStop = "az webapp stop --name " + siteName + " --resource-group exchangeblue ";
//     var scriptStart = "az webapp start --name " + siteName + " --resource-group exchangeblue ";

//     StartPowershellScript (scriptStop);

//     DeployWebsite (new DeploySettings () {
//       SourcePath = sourceDirectoryPath,
//         ComputerName = "https://" + siteName + ".scm.azurewebsites.net:443/msdeploy.axd?site=" + siteName,
//         SiteName = siteName,
//         Username = "$" + siteName,
//         Password = password,
//         // Delete = true,
//         // WhatIf = true
//     });
//     StartPowershellScript (scriptStart);

//   })
//   .IsDependentOn ("AspCore");

// NuGetRestore("./ExchangeLemon/ExchangeLemonFull.sln");
// NuGetRestore("./ExchangeLemon/CodingFullVerTwo-StartHere.sln");
// var url = "./ExchangeLemon/BackEndClassLibrary/BackEndClassLibrary.csproj";
// var url = "./ExchangeLemon/LogicLibrary/LogicLibrary.csproj";
//    var url =  "./ExchangeLemon/DeploymentLemon.sln";
// var url =  "./ExchangeLemon/ExchangeSignal/ExchangeSignal.csproj";

// var url =  "./ExchangeLemon/ExchangeLemonNet/ExchangeLemonNet.csproj";

// MSBuild(url, new MSBuildSettings()
// // MSBuild("./ExchangeLemon/DeploymentLemon.sln", new MSBuildSettings()
// .WithTarget("Build"));
// //   .WithProperty("OutDir", "./publish/")
// //   .WithProperty("DeployOnBuild", "true")
// //   .WithProperty("WebPublishMethod", "Package")
// //   .WithProperty("PackageAsSingleFile", "true")
// //   .WithProperty("SkipInvalidConfigurations", "true"))

// MSBuild("./ExchangeLemon/DeploymentLemon.sln", new MSBuildSettings()
// .WithTarget("Build"));