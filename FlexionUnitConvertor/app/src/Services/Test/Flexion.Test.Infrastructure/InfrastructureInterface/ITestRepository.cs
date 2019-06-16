using Flexion.Test.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Test.Infrastructure.InfrastructureInterface
{
    public interface ITestRepository
    {
          Task<bool> GradeResponse(ExamQuestionAnswer answer);
        Task<bool> CreateExam(Exam exam);
        Task<bool> AddQuestion(ExamQuestion question);
        Task<bool> AddAnswer(ExamQuestionAnswer answer);
        Task<bool> SubmitExamToTeacher(Exam exam);
        Task<bool> SubmitExamToStudent(Exam exam);
        Task<List<Conversion>> GetConversionTable();
        Task<Conversion> GetConversion(int conversionID);
        Task<List<Exam>> GetExams();
        Task<Exam> GetExam(int examID);
        Task<List<Exam>> GetExamByUser(int userID);
        Task<List<ExamQuestion>> GetExamQuestions(int examID);
        Task<List<Exam>> GetExamByTeacherID(int teacherID);
        Task<Exam> Initialize();
    }
}
