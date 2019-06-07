using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Domain.DomainModel;
using Flexion.Test.Infrastructure.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Flexion.Test.Domain
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IConversionService _conversionService;
        public TestService(ITestRepository testRepository, IConversionService conversionService)
        {
            _conversionService = conversionService;
            _testRepository = testRepository;
        }
        public async Task<bool> AddAnswer(ExamQuestionAnswer answer)
        {
            var answerData = new Infrastructure.DataModel.ExamQuestionAnswer()
            {
                Answer = answer.Answer,
                ExamQuestionId = answer.ExamQuestionId,
                IsAnswered = true
               
            };
           return await _testRepository.AddAnswer(answerData);
        }

        public async Task<bool> AddQuestion(ExamQuestion question)
        {

            var questionData = new Infrastructure.DataModel.ExamQuestion()
            {
                SourceConversionId = question.SourceConversionId,
                DestinationConversionId = question.DestinationConversionId,
                ExamId = question.ExamId,
                InputValue = question.InputValue 
               
               
            };
            return await _testRepository.AddQuestion(questionData);
        }

        public async Task<bool> CreateExam(Exam exam)
        {
            var examData = new Infrastructure.DataModel.Exam()
            {
                DateCompleted = exam.DateCompleted,
                DateCreated = DateTime.Now.ToLocalTime(),
                ExamId = exam.ExamId,
                ExamQuestion = null,
                IsComplete = false,
                IsCreated = false,
                IsGraded = false,
                Description = exam.Description,
                StudentId = exam.StudentId,
                TeacherId = exam.TeacherId
               
                
            };

            return await _testRepository.CreateExam(examData);
        }

        public async Task<Conversion> GetConversion(int conversionID)
        {
            var conversion = await _testRepository.GetConversion(conversionID);
            var conversionObject = new Conversion()
            {
                ConversionId = conversion.ConversionId,
                ConversionName = conversion.ConversionName,
                ConversionType = new ConversionType()
                {
                    ConversionName = conversion.ConversionType.ConversionName
                },
                ConversionTypeId = conversion.ConversionTypeId,
                ConversionValue = conversion.ConversionValue
            };
            return conversionObject;
        }

        public async Task<List<Conversion>> GetConversionTable()
        {
            var conversions = await _testRepository.GetConversionTable();
            var conversionObjects = conversions.Select(x =>
              new Conversion()
              {
                  ConversionId = x.ConversionId,
                  ConversionName = x.ConversionName,
                  ConversionType = new ConversionType()
                  {
                      ConversionName = x.ConversionType.ConversionName
                  },
                  ConversionTypeId = x.ConversionTypeId,
                  ConversionValue = x.ConversionValue
              }).ToList();
            return conversionObjects;
        }

        public async Task<Exam> GetExam(int examID)
        {
            var examData = await _testRepository.GetExam(examID);
            var examObject = new Exam()
            {
                DateCompleted = examData.DateCompleted.Value.ToLocalTime(),
                DateCreated = examData.DateCreated,
                Description = examData.Description,
                ExamId = examData.ExamId,
                IsComplete = examData.IsComplete,
                IsCreated = examData.IsCreated,
                IsGraded = examData.IsGraded,
                StudentId = examData.StudentId,
                TeacherId = examData.TeacherId
                
            };
            return examObject;
        }

        public async Task<List<Exam>> GetExamByUser(int userID)
        {
            var examData = await _testRepository.GetExamByUser(userID);
            var examObjects = examData.Select(x=> new Exam()
            {
                DateCompleted = x.DateCompleted,
                DateCreated = x.DateCreated,
                Description = x.Description,
                ExamId = x.ExamId,
                IsComplete = x.IsComplete,
                IsCreated = x.IsCreated,
                IsGraded = x.IsGraded,
                StudentId = x.StudentId,
                TeacherId = x.TeacherId

            }).ToList();
            return examObjects;
        }

        public async Task<List<ExamQuestion>> GetExamQuestions(int examID)
        {
            try
            {
                var questions = await _testRepository.GetExamQuestions(examID);
                var questionObjects = questions.Select(x =>
                new ExamQuestion()
                {
                    InputValue = x.InputValue,
                    DestinationConversionId = x.DestinationConversionId,
                    DestinationConversion = new Conversion()
                    {
                        ConversionId = x.DestinationConversion.ConversionId,
                        ConversionName = x.DestinationConversion.ConversionName,
                        ConversionTypeId = x.DestinationConversion.ConversionTypeId,
                        ConversionType = new ConversionType()
                        {
                            ConversionName = x.DestinationConversion.ConversionType.ConversionName
                        }
                    },
                    SourceConversion = new Conversion()
                    {
                        ConversionId = x.SourceConversion.ConversionId,
                        ConversionName = x.SourceConversion.ConversionName,
                        ConversionTypeId = x.SourceConversion.ConversionTypeId,
                        ConversionType = new ConversionType()
                        {
                            ConversionName = x.SourceConversion.ConversionType.ConversionName
                        }
                    },
                    ExamId = x.ExamId,
                    ExamQuestionId = x.ExamQuestionId,
                    Exam = new Exam()
                    {
                        Description = x.Exam.Description
                    },
                    SourceConversionId = x.SourceConversionId,
                    Answer = new ExamQuestionAnswer()
                    {
                        Answer = x.ExamQuestionAnswer.FirstOrDefault() == null ? null : x.ExamQuestionAnswer.FirstOrDefault().Answer,
                        IsAnswered = x.ExamQuestionAnswer.FirstOrDefault() == null ? false : x.ExamQuestionAnswer.FirstOrDefault().IsAnswered,
                    }

                }).ToList();
                return questionObjects;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Exam>> GetExams()
        {
            var examData = await _testRepository.GetExams();
            var examObjects = examData.Select(x => new Exam()
            {
                DateCompleted = x.DateCompleted,
                DateCreated = x.DateCreated,
                Description = x.Description,
                ExamId = x.ExamId,
                IsComplete = x.IsComplete,
                IsCreated = x.IsCreated,
                IsGraded = x.IsGraded,
                StudentId = x.StudentId,
                TeacherId = x.TeacherId

            }).ToList();

            return examObjects;
        }

        public async Task<bool> SubmitExamToStudent(Exam exam)
        {
            var examData = new Infrastructure.DataModel.Exam()
            {
                ExamId = exam.ExamId,
                IsComplete = false,
                DateCreated = DateTime.Now.ToLocalTime(),
                IsGraded = false,
                StudentId = exam.StudentId,
                TeacherId = exam.TeacherId,
                IsCreated = true



            };
            return await _testRepository.SubmitExamToStudent(examData);
        }

        public async Task<List<Report>> SubmitExamToTeacher(Exam exam)
        {
            var examQuestions = await _testRepository.GetExamQuestions(exam.ExamId);
            var convertionTable = await _testRepository.GetConversionTable();
            bool isGraded = await _conversionService.GradeExam(examQuestions, convertionTable);
            if (isGraded)
            {
                var examData = new Infrastructure.DataModel.Exam()
                {
                    ExamId = exam.ExamId,
                    IsComplete = true,
                    IsGraded = true,
                    DateCompleted = DateTime.Now.ToLocalTime(),

                };
                await _testRepository.SubmitExamToStudent(examData);
            }
            var reports = examQuestions.Select(x =>
              new Report()
              {
                  ExamId = x.ExamId,
                  InputUnitOfMeasure = x.SourceConversion.ConversionName,
                  InputValue = x.InputValue,
                  StudentID = exam.StudentId,
                  StudentName = null,
                  StudentResponse = x.ExamQuestionAnswer.FirstOrDefault().Answer,
                  IsCorrect = x.ExamQuestionAnswer.FirstOrDefault().IsCorrect,
                  OutPutUnitOfMeasure = x.DestinationConversion.ConversionName,
                  TeacherName = null,
                  TeacherID = exam.TeacherId,
                  ExamDate = x.Exam.DateCreated,
                  ExamDescription = x.Exam.Description

              }).ToList();

            return reports;


        }
    }
}
