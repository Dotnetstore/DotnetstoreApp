namespace Dotnetstore.Intranet.SharedKernel.Services;

public abstract class BaseInformationBuilder<TBuilder>
    where TBuilder : BaseInformationBuilder<TBuilder>
{
    protected DateTime _createdDate = DateTime.UtcNow;
    protected Guid? _createdBy;
    protected Guid? _lastUpdatedBy;
    protected DateTime? _lastUpdatedDate;
    protected bool _isDeleted;
    protected Guid? _deletedBy;
    protected DateTime? _deletedDate;
    protected bool _isSystem;
    protected bool _isGdpr;
    
    public TBuilder WithCreatedDate(DateTime createdDate)
    {
        _createdDate = createdDate;
        return (TBuilder)this;
    }

    public TBuilder WithCreatedBy(Guid? createdBy = null)
    {
        _createdBy = createdBy;
        return (TBuilder)this;
    }

    public TBuilder WithLastUpdatedBy(Guid? lastUpdatedBy = null)
    {
        _lastUpdatedBy = lastUpdatedBy;
        return (TBuilder)this;
    }

    public TBuilder WithLastUpdatedDate(DateTime? lastUpdatedDate = null)
    {
        _lastUpdatedDate = lastUpdatedDate;
        return (TBuilder)this;
    }

    public TBuilder WithIsDeleted(bool isDeleted = false)
    {
        _isDeleted = isDeleted;
        return (TBuilder)this;
    }

    public TBuilder WithDeletedBy(Guid? deletedBy = null)
    {
        _deletedBy = deletedBy;
        return (TBuilder)this;
    }

    public TBuilder WithDeletedDate(DateTime? deletedDate = null)
    {
        _deletedDate = deletedDate;
        return (TBuilder)this;
    }

    public TBuilder WithIsSystem(bool isSystem = false)
    {
        _isSystem = isSystem;
        return (TBuilder)this;
    }

    public TBuilder WithIsGdpr(bool isGdpr = false)
    {
        _isGdpr = isGdpr;
        return (TBuilder)this;
    }
}