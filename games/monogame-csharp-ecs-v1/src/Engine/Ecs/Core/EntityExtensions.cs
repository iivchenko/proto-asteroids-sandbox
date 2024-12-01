namespace Engine.Ecs.Core;

public static class EntityExtensions
{
    public static bool WithComponents<TComponent>(this IEntity entity)
    {
        return entity.Components.Any(component => component is TComponent);
    }

    public static bool WithComponents<TComponent1, TComponent2>(this IEntity entity)
    {
        return 
            entity.Components.Any(component => component is TComponent1) &&
            entity.Components.Any(component => component is TComponent2);
    }

    public static (TComponent1, TComponent2) FetchComponents<TComponent1, TComponent2>(this IEntity entity)
        where TComponent1 : class, IComponent
        where TComponent2 : class, IComponent
    {
        var component1 = entity.Components.First(component => component is TComponent1) as TComponent1;
        var component2 = entity.Components.First(component => component is TComponent2) as TComponent2;

        if (component1 == null)
        {
            throw new NullReferenceException($"{nameof(TComponent1)} was not found!");
        }

        if (component2 == null)
        {
            throw new NullReferenceException($"{nameof(TComponent2)} was not found!");
        }

        return (component1, component2);
    }
}
