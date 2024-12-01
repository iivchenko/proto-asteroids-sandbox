using Engine;
using Engine.MonoGame;
using Engine.Services.Content;
using Engine.Services.Draw;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MonoGameServiceCollectionExtensions
    {
        public static IServiceCollection WithMonoGameBackend(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<MonoGameGame>()
                    .AddSingleton<IGame, MonoGameGame>(x => 
                    {
                        return x.GetService<MonoGameGame>();
                    })
                    .AddSingleton<IContentService, MonoGameGame>(x =>
                    {
                        return x.GetService<MonoGameGame>();
                    })
                    .AddSingleton<IDrawService, MonoGameGame>(x =>
                    {
                        return x.GetService<MonoGameGame>();
                    });                    
        }
    }
}
