using Flexion.Test.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Test.Domain.DomainInterface
{
    public interface ITestService
    {
            Task<bool> CreateExam(Exam exam);
            Task<bool> AddQuestion(ExamQuestion question);
            Task<bool> AddAnswer(ExamQuestionAnswer answer);
            Task<List<Report>> SubmitExamToTeacher(Exam exam);
            Task<bool> SubmitExamToStudent(Exam exam);
            Task<List<Conversion>> GetConversionTable();
            Task<Conversion> GetConversion(int conversionID);
            Task<List<Exam>> GetExams();
            Task<Exam> GetExam(int examID);

        Task<Exam> Initialize();
        Task<List<Exam>> GetExamByUser(int userID);
        Task<List<ExamQuestion>> GetExamQuestions(int examID);
        Task<List<Exam>> GetExamByTeacher(int teacherID);
    }
}
