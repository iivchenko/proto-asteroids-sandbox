namespace Engine.EFS.Systems;

public interface ISystem
{
    void Process(IEnumerable<IEntity> faces, float delta);
}
