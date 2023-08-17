﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(PlacementDbContext context) : base(context) { }

    }
}
