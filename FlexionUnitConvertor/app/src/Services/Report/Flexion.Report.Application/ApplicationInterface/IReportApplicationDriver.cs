using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Report.Application.ApplicationInterface
{
    public interface IReportApplicationDriver
    {
        Task<List<Report.Domain.DomainModel.Report>> GetReportByID(int examID);
        Task<List<Report.Domain.DomainModel.Report>> GetReportByUserID(int UserID, int examID);
        Task<bool> AddReport(Report.Domain.DomainModel.Report report);
    }
}
