using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flexion.Report.Domain.DomainModel;

namespace Flexion.Report.Application.ApplicationInterface
{
    public interface IReportApplicationDriver
    {
        Task<List<Report.Domain.DomainModel.Report>> GetReportByID(int examID);
        Task<List<Report.Domain.DomainModel.Report>> GetReportByUserID(int UserID, int examID);
        Task<bool> AddReport(Report.Domain.DomainModel.Report report);
        Task<bool> AddReports(List<Report.Domain.DomainModel.Report> reports);
    }
}
