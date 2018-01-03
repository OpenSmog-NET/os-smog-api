using MediatR;
using Microsoft.AspNetCore.Http;
using OS.Core;
using OS.Core.Queries;
using OS.Domain;
using OS.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OS.Smog.Api.Devices
{
    public class DevicesQueryHandler : IRequestHandler<DevicesQuery, ApiResult<QueryResult<Device>>>
    {
        private readonly IDeviceRepository repository;
        private readonly IHttpContextAccessor contextAccessor;

        public DevicesQueryHandler(IDeviceRepository repository, IHttpContextAccessor contextAccessor)
        {
            this.repository = repository;
        }

        public Task<ApiResult<QueryResult<Device>>> Handle(DevicesQuery request, CancellationToken cancellationToken)
        {
            return new Task<ApiResult<QueryResult<Device>>>(() =>
            {
                var result = new ApiResult<QueryResult<Device>>(contextAccessor.HttpContext)
                {
                };

                try
                {
                    repository.Get(request.GetQuery());
                }
                catch (ArgumentException aex)
                {
                    result.Errors.Add(new ApiError()
                    {
                        Message = aex.Message,
                        Type = ApiErrorType.Validation
                    });
                }

                return result;
            }, cancellationToken);
        }
    }
}