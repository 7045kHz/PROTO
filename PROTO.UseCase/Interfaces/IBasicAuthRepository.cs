using PROTO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.UseCase.Interfaces
{
    public interface IBasicAuthRepository
    {
        Task<BasicAuthorization> GetBasicAuthAsync(string UserName, string PassWord);
    }
}
