﻿using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountByAccountNumberSpec : Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByAccountNumberSpec(string account) => Query.Where(p => p.AccountNumber == account);
}