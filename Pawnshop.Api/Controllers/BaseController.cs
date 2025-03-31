using MediatR;
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
        private IMediator _sender;
        protected IMediator Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<IMediator>();


        [HttpPost("add")]
        public virtual async Task<IActionResult> AddAsync([FromBody] TAddData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await Sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpPut("update")]
        public virtual async Task<IActionResult> PutAsync([FromBody] TPutData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await Sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpDelete("delete")]
        public virtual async Task<IActionResult> DeteleAsync([FromBody] TDeleteData data, CancellationToken cancellation)
        {
            if (data == null)
                throw new BadRequestException("Cannot provide NULL data as param.");

            var response = await Sender.Send(data, cancellation);

            return Ok(response);
        }

        [HttpGet("get")]
        public virtual async Task<IActionResult> GetAsync([FromQuery] TGetData data, CancellationToken cancellation)
        {
            var response = await Sender.Send(data, cancellation);

            return Ok(response);
        }
    }
}
