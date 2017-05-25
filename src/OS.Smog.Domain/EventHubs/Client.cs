using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using OS.Smog.Dto.Events;

namespace OS.Smog.Domain.EventHubs
{
    public class Client : IClient
    {
        private readonly EventHubClient client;
        public Task SendAsync<TEvent>(TEvent integrationEvent) where TEvent : IntegrationEvent
        {
            throw new NotImplementedException();
        }
    }
}
