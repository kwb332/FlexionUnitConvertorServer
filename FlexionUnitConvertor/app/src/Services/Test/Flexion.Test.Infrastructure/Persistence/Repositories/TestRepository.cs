using Flexion.Test.Infrastructure.DataModel;
using Flexion.Test.Infrastructure.InfrastructureInterface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flexion.Test.Infrastructure.Persistence.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly examDBContext _examDBContext;

        public TestRepository(examDBContext examDBContext)
        {
            _examDBContext = examDBContext;
        }
        public async Task<bool> AddAnswer(ExamQuestionAnswer answer)
        {
            try
            {
                var questionAnswer = await _examDBContext.ExamQuestionAnswer.FirstOrDefaultAsync(x => x.ExamQuestionId == answer.ExamQuestionId);
                if (questionAnswer != null)
                {
                    questionAnswer.Answer = answer.Answer;
                    _examDBContext.Update(questionAnswer);
                    _examDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    await _examDBContext.ExamQuestionAnswer.AddAsync(answer);
                    _examDBContext.SaveChanges();
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
                await _examDBContext.ExamQuestion.AddAsync(question);
                _examDBContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateExam(Exam exam)
        {
            try
            {
                await _examDBContext.Exam.AddAsync(exam);
                _examDBContext.SaveChanges();
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
                return await _examDBContext.Conversion.Include(x=>x.ConversionType).FirstOrDefaultAsync(x=>x.ConversionId == conversionID);
                
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
                return await _examDBContext.Conversion.AsNoTracking().Include(x=>x.ConversionType).ToListAsync();

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
                return await _examDBContext.Exam.FirstOrDefaultAsync(x=>x.ExamId == examID);

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Exam>> GetExamByUser(int userID)
        {
            try
            {
                return await _examDBContext.Exam.AsNoTracking().Where(x => x.StudentId == userID && x.IsCreated == true).ToListAsync();

            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Exam>> GetExamByTeacherID(int teacherID)
        {
            try
            {
                return await _examDBContext.Exam.AsNoTracking().Where(x => x.TeacherId == teacherID).ToListAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Exam>> GetExams()
        {
            try
            {
                return await _examDBContext.Exam.AsNoTracking().ToListAsync();

            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> asyncmitExamToStudent(Exam exam)
        {
            try
            {
                await _examDBContext.Exam.AddAsync(exam);
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
                var entity = await _examDBContext.Exam.FirstOrDefaultAsync(x => x.ExamId == exam.ExamId);
                _examDBContext.Entry(entity).State = EntityState.Detached;
                _examDBContext.Exam.Update(exam);
                _examDBContext.SaveChanges();
                return true;
              
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> GradeResponse(ExamQuestionAnswer answer)
        {
            try
            {
                var entity = await _examDBContext.ExamQuestionAnswer.FirstOrDefaultAsync(x => x.ExamQuestionAnswerId == answer.ExamQuestionAnswerId);
                _examDBContext.Entry(entity).State = EntityState.Detached;
                _examDBContext.ExamQuestionAnswer.Update(answer);
                _examDBContext.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SubmitExamToStudent(Exam exam)
        {
            try
            {
                var entity = await _examDBContext.Exam.FirstOrDefaultAsync(x => x.ExamId == exam.ExamId);
             
                _examDBContext.Entry(entity).State = EntityState.Detached;
                exam.Description = entity.Description;
                exam.DateCreated = entity.DateCreated;
                exam.TeacherId = entity.TeacherId;
                exam.StudentId = entity.StudentId;
               
                _examDBContext.Exam.Update(exam);
                _examDBContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ExamQuestion>> GetExamQuestions(int examID)
        {
            return await _examDBContext.ExamQuestion.AsNoTracking().Include(x=>x.ExamQuestionAnswer).Include(x=>x.DestinationConversion).Include(x => x.DestinationConversion.ConversionType).Include(x => x.SourceConversion.ConversionType).Include(x=>x.SourceConversion).Include(x=>x.Exam).Where(x=>x.ExamId == examID).ToListAsync();
        }
    }
}
