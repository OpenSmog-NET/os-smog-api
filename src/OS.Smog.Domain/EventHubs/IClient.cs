using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Smog.Dto.Events;

namespace OS.Smog.Domain.EventHubs
{
    public interface IClient
    {
        Task SendAsync<TEvent>(TEvent integrationEvent)
            where TEvent : IntegrationEvent;
    }
}
