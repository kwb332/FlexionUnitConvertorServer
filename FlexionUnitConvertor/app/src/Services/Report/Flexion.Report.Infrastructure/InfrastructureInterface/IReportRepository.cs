
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Report.Infrastructure.InfrastructureInterface
{
    public interface IReportRepository
    {
        Task<List<DataModel.Report>> GetReportByID(int ReportID);
        Task<List<DataModel.Report>> GetReportByUserID(int UserID, int examID);
        Task<bool> AddReport(DataModel.Report report);

    }
}
