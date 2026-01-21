namespace Engine.EFS
{
    public interface IEntity
    {
        Guid Id { get; }
        bool IsAlive { get; set; }
    }
}
