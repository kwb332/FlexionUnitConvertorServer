using Flexion.Report.API;
using Flexion.Report.Infrastructure.DataModel;
using Flexion.Report.Infrastructure.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flexion.Report.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private ReportDBContext _reportDBContext;
        public ReportRepository(ReportDBContext reportDBContext)
        {
            _reportDBContext = reportDBContext;
        }
        public async Task<List<DataModel.Report>> GetReportByID(int examID)
        {
            return await _reportDBContext.Report.Where(x => x.ExamId == examID).ToListAsync();
        }
        public async Task<bool> AddReport(DataModel.Report report)
        {
            try
            {

                report.InputUnitOfMeasure = report.ExamQuestion.InputUnitOfMeasure;
                report.InputValue = report.ExamQuestion.InputValue;
                report.IsCorrect = report.ExamQuestion.IsCorrect;
                report.StudentID = report.ExamQuestion.StudentID;
                report.OutPutUnitOfMeasure = report.ExamQuestion.OutPutUnitOfMeasure;
                report.StudentName = report.ExamQuestion.StudentName;
                report.TeacherName = report.ExamQuestion.TeacherName;
                report.StudentResponse = report.ExamQuestion.StudentResponse;

               
                await _reportDBContext.Report.AddAsync(report);

                _reportDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddReports(List<DataModel.Report> reports)
        {
            try
            {
                foreach (var report in reports)
                {
                    report.InputUnitOfMeasure = report.ExamQuestion.InputUnitOfMeasure;
                    report.InputValue = report.ExamQuestion.InputValue;
                    report.IsCorrect = report.ExamQuestion.IsCorrect;
                    report.StudentID = report.ExamQuestion.StudentID;
                    report.OutPutUnitOfMeasure = report.ExamQuestion.OutPutUnitOfMeasure;
                    report.StudentName = report.ExamQuestion.StudentName;
                    report.TeacherName = report.ExamQuestion.TeacherName;
                    report.StudentResponse = report.ExamQuestion.StudentResponse;
                   

                }
                await _reportDBContext.Report.AddRangeAsync(reports);
                _reportDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<DataModel.Report>> GetReportByUserID(int UserID, int examID)
        {
            return await _reportDBContext.Report.Where(x => x.StudentID == UserID && x.ExamId == examID).ToListAsync();
        }
    }
}
