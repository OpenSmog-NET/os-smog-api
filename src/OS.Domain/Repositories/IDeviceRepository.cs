using System;

namespace OS.Domain.Repositories
{
    public interface IDeviceRepository
    {
        Guid Insert(Domain.Device device);

        Domain.Device Get(Guid id);
    }
}