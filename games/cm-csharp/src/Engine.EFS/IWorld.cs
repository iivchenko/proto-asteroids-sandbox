namespace Engine.EFS;

public interface IWorld
{
    void AddEntity(IEntity entity);

    void Process(float delta);
}
