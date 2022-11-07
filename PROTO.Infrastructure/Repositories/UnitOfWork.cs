using PROTO.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTO.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IHostDeviceRepository hostRepository)
        {
            HostDevice = hostRepository;
        }

        public IHostDeviceRepository HostDevice { get; }

    }
}
