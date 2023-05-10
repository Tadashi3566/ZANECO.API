﻿namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerByIdSpec : Specification<Customer>, ISingleResultSpecification
{
    public CustomerByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}