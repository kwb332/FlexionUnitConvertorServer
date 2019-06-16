using Flexion.Test.Infrastructure.DataModel;
using Flexion.Test.Infrastructure.InfrastructureInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.Test.Infrastructure.Mock.Persistence.Repositories
{
    public class ExamRepositoryMock : ITestRepository
    {
        private readonly ExamMockDBContext _examMockDbContext;

        public ExamRepositoryMock(ExamMockDBContext examMockDBContext)
        {
            _examMockDbContext = examMockDBContext;
           
        }

        public async Task<Exam> Initialize()
        {
           return await _examMockDbContext.InitializeDB();
        }
        public async Task<bool> AddAnswer(ExamQuestionAnswer answer)
        {
            try
            {
                var questionAnswer = _examMockDbContext.ExamQuestionAnswer.FirstOrDefault(x => x.ExamQuestionId == answer.ExamQuestionId);
                if (questionAnswer != null)
                {
                    questionAnswer.Answer = answer.Answer;

                    return true;
                }
                else
                {
                   var question = _examMockDbContext.ExamQuestion.FirstOrDefault(x => x.ExamQuestionId == answer.ExamQuestionId);
                    question.ExamQuestionAnswer.Add(answer);
                    _examMockDbContext.ExamQuestionAnswer.Add(answer);

                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddQuestion(ExamQuestion question)
        {
            try
            {
                _examMockDbContext.ExamQuestion.Add(question);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateExam(Exam exam)
        {
            try
            {
                _examMockDbContext.Exam.Add(exam);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Conversion> GetConversion(int conversionID)
        {
            try
            {
                return  _examMockDbContext.Conversion.FirstOrDefault(x => x.ConversionId == conversionID);

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Conversion>> GetConversionTable()
        {
            try
            {
                return  _examMockDbContext.Conversion;

            }
            catch
            {
                return null;
            }
        }

        public async Task<Exam> GetExam(int examID)
        {
            try
            {
                return  _examMockDbContext.Exam.FirstOrDefault(x => x.ExamId == examID);

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Exam>> GetExamByTeacherID(int teacherID)
        {
            try
            {
                return _examMockDbContext.Exam.Where(x => x.TeacherId == teacherID).ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Exam>> GetExamByUser(int userID)
        {
            try
            {
                return _examMockDbContext.Exam.Where(x => x.StudentId == userID && x.IsCreated == true).ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ExamQuestion>> GetExamQuestions(int examID)
        {
            return  _examMockDbContext.ExamQuestion.Where(x => x.ExamId == examID).ToList();
        }

        public async Task<List<Exam>> GetExams()
        {
            try
            {
                return _examMockDbContext.Exam;

            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> GradeResponse(ExamQuestionAnswer answer)
        {
            try
            {
                var entity = _examMockDbContext.ExamQuestionAnswer.FirstOrDefault(x => x.ExamQuestionAnswerId == answer.ExamQuestionAnswerId);
                entity = answer;
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SubmitExamToStudent(Exam exam)
        {
            try
            {
                var entity = _examMockDbContext.Exam.FirstOrDefault(x => x.ExamId == exam.ExamId);
                entity.IsCreated = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SubmitExamToTeacher(Exam exam)
        {
            try
            {
                var entity =  _examMockDbContext.Exam.FirstOrDefault(x => x.ExamId == exam.ExamId);

                entity = exam;
                
                return true;

            }
            catch
            {
                return false;
            }
        }

    
    }
}
