namespace Dotnetstore.Intranet.SharedKernel.Models;

public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id)
    where TId : notnull
{
    public Guid? CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public Guid? LastUpdatedBy { get; init; }

    public DateTime? LastUpdatedDate { get; init; }

    public bool IsDeleted { get; init; }

    public Guid? DeletedBy { get; init; }

    public DateTime? DeletedDate { get; init; }

    public bool IsSystem { get; init; }

    public bool IsGdpr { get; init; }

    public byte[] ConcurrencyToken { get; init; } = null!;
}