var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ItAcademy_Samples_Mvc>("Mvc-app");

builder.AddProject<Projects.SampleSolution_MVC_Identity>("samplesolution-mvc-identity");

builder.AddProject<Projects.ItAcademy_Samples_WebAPI>("itacademy-samples-webapi");

builder.AddProject<Projects.ItAcademy_Sample_MinimalApi>("itacademy-sample-minimalapi");

builder.Build().Run();
