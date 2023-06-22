using AutoMapper;
using EsuhaiSchedule.API.Parameters;
using EsuhaiSchedule.Application.DTOs;
using EsuhaiSchedule.Application.Enums;
using EsuhaiSchedule.Application.Services.RecurringJob;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EsuhaiSchedule.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecurringController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecurringJobService _recurringJobService;

        public RecurringController(IMapper mapper, IRecurringJobService recurringJobService)
        {
            _mapper = mapper;
            _recurringJobService = recurringJobService;
        }

        [HttpPut("AddOrUpdate")]
        public IActionResult Put([FromQuery] AddOrUpdateParameters param)
        {
            var request = _mapper.Map<AddOrUpdateDtos>(param);
            var result = _recurringJobService.S2_AddOrUpdateRecurringJob(request);
            if (result.Succeeded)
                return Ok(result.Message);
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobname">Chọn tên job cần xóa!</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([Required] JobEnums jobname)
        {
            RecurringJob.RemoveIfExists(jobname.ToString());
            return Ok("Remove successed!");
        }
    }
}


