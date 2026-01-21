namespace Engine.EFS.Systems;

public interface ISystem
{
    IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta);
}
