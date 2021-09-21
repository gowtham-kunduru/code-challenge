using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/reporting")]
    public class ReportingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingService _reportingService;

        public ReportingController(ILogger<ReportingController> logger, IReportingService reportingService)
        {
            _logger = logger;
            _reportingService = reportingService;
        }       

        [HttpGet("{id}", Name = "getReportByEmployeeId")]
        public IActionResult getReportByEmployeeId(String id)
        {
            _logger.LogDebug($"Received Reporting  get request for '{id}'");

            var employeeDirectReport = _reportingService.GetDirectReportsById(id);

            if (employeeDirectReport == null)
                return NotFound();

            return Ok(employeeDirectReport);
        }
    }
}
