using Flexion.Report.Application.ApplicationInterface;
using Flexion.Report.Domain.DomainInterface;
using Flexion.Report.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Report.Application
{
    public class ReportApplicationDriver : IReportApplicationDriver
    {
        private readonly IReportService _reportService;

        public ReportApplicationDriver(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<bool> AddReport(Domain.DomainModel.Report report)
        {
            return await _reportService.AddReport(report);
        }

        public async Task<bool> AddReports(List<Domain.DomainModel.Report> reportAdd)
        {
            return await _reportService.AddReports(reportAdd);
        }

        public async Task<List<Domain.DomainModel.Report>> GetReportByID(int examID)
        {
            return await _reportService.GetReportByID(examID);
        }

        public async Task<List<Domain.DomainModel.Report>> GetReportByUserID(int UserID, int examID)
        {
            return await _reportService.GetReportByUserID(UserID, examID);
        }
    }
}
