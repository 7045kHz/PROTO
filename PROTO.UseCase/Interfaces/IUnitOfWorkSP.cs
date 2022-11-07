using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROTO.Core.Models;

namespace PROTO.UseCase.Interfaces
{
    public interface IUnitOfWorkSP
    {
        IHostDeviceRepositorySP HostDevice { get; }
    }
    
}
