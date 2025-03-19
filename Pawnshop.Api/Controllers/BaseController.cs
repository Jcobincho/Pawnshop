using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Api.Controllers
{
    [ApiController]
    public abstract class BaseController<TAddData, 
                                         TPutData, 
                                         TDeleteData, 
                                         TGetData> : ControllerBase where TAddData : BaseCommand
                                                                    where TPutData : BaseCommand
                                                                    where TDeleteData : BaseCommand
                                                                    where TGetData : BaseQuery
    {
        private readonly IMediator _sender;

        public BaseController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpPost()]
        public virtual async Task<IActionResult> AddAsync([FromBody] TAddData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await _sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpPut()]
        public virtual async Task<IActionResult> PutAsync([FromBody] TPutData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await _sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpDelete()]
        public virtual async Task<IActionResult> DeteleAsync([FromBody] TDeleteData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await _sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpGet()]
        public virtual async Task<IActionResult> GetAsync([FromQuery] TGetData data, CancellationToken cancellation)
        {
            var response = await _sender.Send(data, cancellation);

            return Ok(response);
        }
    }
}
