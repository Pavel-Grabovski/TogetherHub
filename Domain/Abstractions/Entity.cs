namespace Domain.Abstractions;

public abstract class Entity<T> : IEntity
{
    public required T Id { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime CreationTime { get; set; }
}
