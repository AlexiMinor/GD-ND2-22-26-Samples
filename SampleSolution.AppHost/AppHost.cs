var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ItAcademy_Samples_Mvc>("Mvc-app");

builder.AddProject<Projects.SampleSolution_MVC_Identity>("samplesolution-mvc-identity");

builder.Build().Run();
