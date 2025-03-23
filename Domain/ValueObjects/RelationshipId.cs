namespace Domain.ValueObjects;

public record RelationshipId
{
    public Guid Value { get; }

    private RelationshipId(Guid value)
    {
        this.Value = value;
    }

    public static RelationshipId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("RelationshipId cannot be empty.");

        return new RelationshipId(value);
    }
}
