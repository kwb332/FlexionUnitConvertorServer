using Flexion.Test.Application.ApplicationInterface;
using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Test.Application
{
    public class TestApplicationDriver : ITestApplicationDriver
    {
        private readonly ITestService _testService;
        public TestApplicationDriver(ITestService testService)
        {
            _testService = testService;
        }
        public async Task<bool> AddAnswer(ExamQuestionAnswer answer)
        {
            return await _testService.AddAnswer(answer);
        }

        public async Task<bool> AddQuestion(ExamQuestion question)
        {
            return await _testService.AddQuestion(question);
        }

        public async Task<bool> CreateExam(Exam exam)
        {
           return await _testService.CreateExam(exam);
        }

        public async Task<Conversion> GetConversion(int conversionID)
        {
            return await _testService.GetConversion(conversionID);
        }

        public async Task<List<Conversion>> GetConversionTable()
        {
            return await _testService.GetConversionTable();
        }

        public async Task<Exam> GetExam(int examID)
        {
            return await _testService.GetExam(examID);
        }

        public async Task<List<Exam>> GetExamByUser(int userID)
        {
            return await _testService.GetExamByUser(userID);
        }

        public async Task<List<Exam>> GetExams()
        {
            return await _testService.GetExams();
        }
        public async Task<List<ExamQuestion>> GetExamQuestions(int examID)
        {
            return await _testService.GetExamQuestions(examID);
        }
        public async Task<bool> SubmitExamToStudent(Exam exam)
        {
            return await _testService.SubmitExamToStudent(exam);
        }

        public async Task<List<Report>> SubmitExamToTeacher(Exam exam)
        {
            return await _testService.SubmitExamToTeacher(exam);
        }

       
    }
}
