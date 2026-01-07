using Engine;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Desktop;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        GameBuilder
            .CreateBuilder()
            .WithServices(services =>
                {
                    services
                        .WithMonoGameBackend()
                        .AddEcs()
                        .AddSingleton<IScene, GamePlayScene>()
                        .AddSingleton<IEntryPoint, EntryPoint>();
                })
            .Build()
            .Run();
    }
}
