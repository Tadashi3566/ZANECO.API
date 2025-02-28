﻿namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierByNameSpec : Specification<Supplier, SupplierDto>, ISingleResultSpecification<Supplier>
{
    public SupplierByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}