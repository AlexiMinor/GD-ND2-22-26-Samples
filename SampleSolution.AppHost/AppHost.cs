var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ItAcademy_Samples_Mvc>("Mvc-app");

builder.Build().Run();
