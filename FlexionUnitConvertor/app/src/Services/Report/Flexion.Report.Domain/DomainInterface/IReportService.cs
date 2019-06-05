using Flexion.Report.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Report.Domain.DomainInterface
{
    public interface IReportService
    {
        Task<List<Report.Domain.DomainModel.Report>> GetReportByID(int examID);
        Task<List<Report.Domain.DomainModel.Report>> GetReportByUserID(int UserID, int examID);
        Task<bool> AddReport(Report.Domain.DomainModel.Report report);
    }
}
