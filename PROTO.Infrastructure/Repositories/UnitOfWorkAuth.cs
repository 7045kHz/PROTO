using PROTO.Core.Models;
using PROTO.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.Infrastructure.Repositories
{
    public class UnitOfWorkAuth : IUnitOfWorkAuth
    {
        public UnitOfWorkAuth(IBasicAuthRepository basicRepository)
        {
            BasicAuthorization = basicRepository;
        }

        public IBasicAuthRepository BasicAuthorization { get; }

    }
}
