using Dapper.Contrib.Extensions;
using System.Runtime.CompilerServices;
using TarefasApi.Data;
using static TarefasApi.Data.TarefaContext;

namespace TarefasApi.Endpoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {

            #region GET
            app.MapGet("/", () => $"Bem-Vindo a API Tasks {DateTime.Now}");

            app.MapGet("/tarefas", async (GetConnection connectionGetter) =>
            {
                using var conn = await connectionGetter();
                var tarefas = conn.GetAll<Tarefa>().ToList();
                if(tarefas is null) { return Results.NotFound(); }
                return Results.Ok(tarefas);
            });

            app.MapGet("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var conn = await connectionGetter();
                var tarefa = conn.Get<Tarefa>(id);
                if (tarefa is null) { return Results.NotFound(); }
                return Results.Ok(tarefa);
            });

            #endregion

            #region POST
            app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
            {
                using var conn = await connectionGetter();
                var id = conn.Insert(tarefa);
                return Results.Created($"/tarefas/{id}", tarefa);

            });

            #endregion

            #region PUT
            app.MapPut("/tarefas", async (GetConnection connectionGetter, Tarefa tarefa) =>
            {
                using var conn = await connectionGetter();
                var id = conn.Update(tarefa);
                return Results.Ok(tarefa);

            });

            #endregion

            #region DELETE

            app.MapDelete("/tarefas/{id}", async(GetConnection connectionGetter, int id) =>
            {
                using var conn = await connectionGetter();

                var deleted = conn.Get<Tarefa>(id);

                if(deleted is null)
                {
                    return Results.NotFound();
                }

                conn.Delete(deleted);
                return Results.Ok(deleted);
            });
            #endregion

        }
    }
}
