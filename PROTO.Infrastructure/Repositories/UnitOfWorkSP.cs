using PROTO.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.Infrastructure.Repositories
{
    public class UnitOfWorkSP : IUnitOfWorkSP
    {
        public UnitOfWorkSP(IHostDeviceRepositorySP hostRepository)
        {
            HostDevice = hostRepository;
        }

        public IHostDeviceRepositorySP HostDevice { get; }

    }
}
