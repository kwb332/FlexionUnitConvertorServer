using Flexion.Report.Domain.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Flexion.Report.Infrastructure.InfrastructureInterface;
using Flexion.Report.Domain.DomainModel;

namespace Flexion.Report.Domain.DomainService
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<bool> AddReport(DomainModel.Report reportData)
        {
            var examQuestionsData =  new Infrastructure.DataModel.ExamQuestion()
            {
                ExamId = reportData.ExamId,
                InputUnitOfMeasure = reportData.InputUnitOfMeasure,
                InputValue = reportData.InputValue,
                StudentID = reportData.StudentID,
                StudentName = reportData.StudentName,
                StudentResponse = reportData.StudentResponse,
                IsCorrect = reportData.IsCorrect,
                OutPutUnitOfMeasure = reportData.OutPutUnitOfMeasure,
                TeacherName = reportData.TeacherName
            };
            var reportToAdd = new Report.Infrastructure.DataModel.Report()
            {
                ExamDate = reportData.ExamDate,
                ExamDescription = reportData.ExamDescription,
                ExamId = reportData.ExamId,
                ExamQuestion = examQuestionsData
            };
            return await _reportRepository.AddReport(reportToAdd);
        }

        public async Task<List<DomainModel.Report>> GetReportByID(int examID)
        {
            var reportData = await _reportRepository.GetReportByID(examID);
            var report = reportData.Select(x => new Domain.DomainModel.Report()
            {
                ExamId = x.ExamId,
                InputUnitOfMeasure = x.InputUnitOfMeasure,
                InputValue = x.InputValue,
                StudentID = x.StudentID,
                StudentName = x.StudentName,
                StudentResponse = x.StudentResponse,
                IsCorrect = x.IsCorrect,
                ExamDate = x.ExamDate,
                OutPutUnitOfMeasure = x.OutPutUnitOfMeasure,
                TeacherName = x.TeacherName,
                ExamQuestion = new ExamQuestion()
                {
                   
                    ExamId = x.ExamId,
                    InputUnitOfMeasure = x.InputUnitOfMeasure,
                    InputValue = x.InputValue,
                    StudentID = x.StudentID,
                    StudentName = x.StudentName,
                    StudentResponse = x.StudentResponse,
                    IsCorrect = x.IsCorrect,
                    OutPutUnitOfMeasure = x.OutPutUnitOfMeasure,
                    TeacherName = x.TeacherName
            
                    
                }
            }).ToList();
            

            return report;
        }

        private List<Infrastructure.DataModel.Report> GroupReportByExam(List<Infrastructure.DataModel.Report> reports)
        {
            List<Infrastructure.DataModel.Report> results = new List<Infrastructure.DataModel.Report>();
            var query = reports.GroupBy(
       report => report.ExamId,
       report => new Infrastructure.DataModel.Report()
       {
           ExamDate = report.ExamDate,
           ExamDescription = report.ExamDescription,
           ReportId = report.ReportId,
           ExamId = report.ExamId,
           InputUnitOfMeasure = report.InputUnitOfMeasure,
           InputValue = report.InputValue,
           StudentID = report.StudentID,
           IsCorrect = report.IsCorrect,
           OutPutUnitOfMeasure = report.OutPutUnitOfMeasure,
           StudentName = report.StudentName,
           StudentResponse = report.StudentResponse,
           TeacherName = report.TeacherName
       },
       (examID, examQuestions) => new
       {
           Key = examID,
           examResults = examQuestions

       });
            foreach(var curGroup in query)
            {
                var report = curGroup.examResults.FirstOrDefault();
                List<Infrastructure.DataModel.ExamQuestion> questionList = new List<Infrastructure.DataModel.ExamQuestion>();
                foreach(var curQuestion in curGroup.examResults)
                {
                    Infrastructure.DataModel.ExamQuestion question = new Infrastructure.DataModel.ExamQuestion()
                    {
                        ExamId = curQuestion.ExamId,
                        InputUnitOfMeasure = curQuestion.InputUnitOfMeasure,
                        InputValue = curQuestion.InputValue,
                        IsCorrect = curQuestion.IsCorrect,
                        OutPutUnitOfMeasure = curQuestion.OutPutUnitOfMeasure,
                        StudentID = curQuestion.StudentID,
                        StudentName = curQuestion.StudentName,
                        StudentResponse = curQuestion.StudentResponse,
                        TeacherName = curQuestion.TeacherName
                    };
                    questionList.Add(question);
                }
                report.ExamQuestions = questionList;
                results.Add(report);
            }
            return results;
        }
        public async Task<List<DomainModel.Report>> GetReportByUserID(int UserID, int examID)
        {
            var reportData = await _reportRepository.GetReportByUserID(examID, UserID);
            var report = reportData.Select(x => new Domain.DomainModel.Report()
            {
                ExamId = x.ExamId,
                InputUnitOfMeasure = x.InputUnitOfMeasure,
                InputValue = x.InputValue,
                StudentID = x.StudentID,
                StudentName = x.StudentName,
                StudentResponse = x.StudentResponse,
                IsCorrect = x.IsCorrect,
                ExamDate = x.ExamDate,
                OutPutUnitOfMeasure = x.OutPutUnitOfMeasure,
                TeacherName = x.TeacherName,
                ExamQuestion = new ExamQuestion()
                {

                    ExamId = x.ExamId,
                    InputUnitOfMeasure = x.InputUnitOfMeasure,
                    InputValue = x.InputValue,
                    StudentID = x.StudentID,
                    StudentName = x.StudentName,
                    StudentResponse = x.StudentResponse,
                    IsCorrect = x.IsCorrect,
                    OutPutUnitOfMeasure = x.OutPutUnitOfMeasure,
                    TeacherName = x.TeacherName


                }
            }).ToList();


            return report;
        }

       
    }
}
