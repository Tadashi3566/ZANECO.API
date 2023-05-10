namespace ZANECO.API.Domain.Common.Contracts;

public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    DefaultIdType? DeletedBy { get; set; }
}