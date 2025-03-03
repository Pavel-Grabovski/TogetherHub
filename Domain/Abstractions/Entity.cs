namespace Domain.Abstractions;

public abstract class Entity<T> : IEntity
{
    public required T Id { get; set; }
}
