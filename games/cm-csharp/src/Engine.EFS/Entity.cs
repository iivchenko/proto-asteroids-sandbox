namespace Engine.EFS
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public bool IsAlive { get; set; } = true;
    }
}
