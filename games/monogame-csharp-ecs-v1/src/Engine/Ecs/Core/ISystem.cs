namespace Engine.Ecs.Core;

public interface ISystem
{
    public void Process(IEnumerable<IEntity> entities, float delta);
}
