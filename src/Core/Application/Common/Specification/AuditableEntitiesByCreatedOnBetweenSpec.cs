namespace ZANECO.API.Application.Common.Specification;

public class AuditableEntitiesByCreatedOnBetweenSpec<T> : Specification<T>
    where T : AuditableEntity
{
    public AuditableEntitiesByCreatedOnBetweenSpec(DateTime from, DateTime until) =>
        Query.Where(e => e.CreatedOn >= from && e.CreatedOn <= until);
}

public class AuditableEntitiesByCreatedOnBetweenSpecInt<T> : Specification<T>
    where T : AuditableEntity<int>
{
    public AuditableEntitiesByCreatedOnBetweenSpecInt(DateTime from, DateTime until) =>
        Query.Where(e => e.CreatedOn >= from && e.CreatedOn <= until);
}