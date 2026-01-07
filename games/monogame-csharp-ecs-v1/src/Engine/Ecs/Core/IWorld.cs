namespace Engine.Ecs.Core;

public interface IWorld
{
    public void Process(float delta);

    public IEntity CreateEntity(IEnumerable<IComponent> components);
}
