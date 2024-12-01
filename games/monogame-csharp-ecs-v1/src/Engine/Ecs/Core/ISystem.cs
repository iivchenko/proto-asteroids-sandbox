namespace Engine.Ecs.Core;

public interface ISystem
{
    public void Run(IEnumerable<IEntity> entities);
}
