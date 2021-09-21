using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ReportingService> _logger;

        public ReportingService(ILogger<ReportingService> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        public ReportingStructure GetDirectReportsById(string id)
        {
            Employee employee = _employeeService.GetWithDirectReportsById(id);
            ReportingStructure reportingStructure = new ReportingStructure();
            reportingStructure.Employee = employee;
            reportingStructure.NumberOfReports = GetTotalNumberOfReports(employee);
            return reportingStructure;
        }
        public int GetTotalNumberOfReports(Employee employee)
        {
            int reportCount = 0;
            if (employee != null && employee.DirectReports == null)
            {
                return reportCount;
            }
            else
            {
                reportCount += employee.DirectReports.Count; // set the count for direct reports, then we can iterate through sub list
                foreach (var reportingEmployee in employee.DirectReports)
                {
                    var currentemployee = _employeeService.GetWithDirectReportsById(reportingEmployee.EmployeeId);
                    reportCount += GetTotalNumberOfReports(currentemployee); // A recursive call to travers through employee --> reports to
                }
                return reportCount;
            }

        }
    }
}
