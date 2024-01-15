using TarefasApi.Endpoints;
using TarefasApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersintence();

var app = builder.Build();

app.MapTarefasEndpoints();


app.Run();
