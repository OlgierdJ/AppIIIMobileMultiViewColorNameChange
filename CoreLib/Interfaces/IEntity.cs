namespace CoreLib.Interfaces
{
    public interface IEntity<TId> : IEntity
    {
        new TId Id { get; set; }
    }

    public interface IEntity 
    {
        object Id { get; set; }
    }
}
