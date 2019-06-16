using Flexion.Test.Application;
using Flexion.Test.Application.ApplicationInterface;
using Flexion.Test.Domain.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Collections.Generic;
using System.Linq;

namespace ExamUnitTest
{
    [TestClass]
    public class ExamUnitTest
    {
       
        private ITestApplicationDriver _driver;

        [TestInitialize]
        public void Initialize()
        {
            ExamFixer fixture = new ExamFixer();
            _driver = fixture.ServiceProvider.GetService<ITestApplicationDriver>();
            _driver.Initialize();
        }
        public ExamUnitTest()
        {
           

        }
        #region SuccessTest
        [TestMethod]
        public  void AddAnswer()
        {
            ExamQuestionAnswer answer = new ExamQuestionAnswer();
            var questions =  _driver.GetExamQuestions(1).GetAwaiter().GetResult();
            var question = questions.FirstOrDefault(x => x.ExamQuestionId == 1);
            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.Answer = 25;
            answer.IsCorrect = false;
            answer.ExamQuestion = question;

           
            answer.ExamQuestionId = question.ExamQuestionId;
            _driver.AddAnswer(answer);
            questions =  _driver.GetExamQuestions(1).GetAwaiter().GetResult();
            question = questions.FirstOrDefault(x => x.ExamQuestionId == 1);
          
             

            Assert.AreEqual(answer.Answer, question.Answer.Answer);

        }

        [TestMethod]
        public  void AddQuestion()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();
            var initializedQuestion = exam.ExamQuestion.FirstOrDefault();

            var questions =  _driver.GetExamQuestions(1).GetAwaiter().GetResult();
            ExamQuestion question = new ExamQuestion();
            question.ExamId = 1;
       
            question.InputValue = 23;
            question.SourceConversionId = initializedQuestion.DestinationConversionId;
            question.DestinationConversionId = initializedQuestion.SourceConversionId;
            question.Exam = exam;
            question.DestinationConversion = initializedQuestion.DestinationConversion;
            question.SourceConversion = initializedQuestion.SourceConversion;

            
            question.ExamQuestionId = questions.Count() + 1;

            var initialCount = questions.Count();
             _driver.AddQuestion(question);
            questions =  _driver.GetExamQuestions(1).GetAwaiter().GetResult();

            var postCount = questions.Count();


            Assert.AreNotEqual(initialCount, postCount);
        }

        [TestMethod]
        public void CreateExam()
        {
            var exam  = _driver.Initialize().GetAwaiter().GetResult();

            var exams =  _driver.GetExams().GetAwaiter().GetResult();

           
            var initialCount = exams.Count();
            var name = initialCount + 1;
            exam.ExamId = exams.Count() + 1;
            exam.DateCompleted = null;
            exam.DateCreated = DateTime.Now;
            exam.Description = "Exam " + name;
            exam.IsComplete = false;
            exam.IsCreated = true;
            exam.IsGraded = false;
            exam.TeacherId = 1;
            exam.StudentId = 1;

             _driver.CreateExam(exam).GetAwaiter().GetResult();
            exams = _driver.GetExams().GetAwaiter().GetResult();

            var postCount = exams.Count();


            Assert.AreNotEqual(initialCount, postCount);

        }

        [TestMethod]
        public  void GetConversion()
        {
            var result =  _driver.GetConversion(1).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public  void GetConversionTable()
        {
            var results =  _driver.GetConversionTable().GetAwaiter().GetResult();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public  void GetExam()
        {
            var result =  _driver.GetExam(1).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public  void GetExamByUser()
        {
            var result =  _driver.GetExamByUser(1).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public  void GetExams()
        {
            var results =  _driver.GetExams().GetAwaiter().GetResult();

            Assert.IsNotNull(results);
        }
        [TestMethod]
        public  void GetExamQuestions()
        {
            var results =  _driver.GetExamQuestions(1).GetAwaiter().GetResult();

            Assert.IsNotNull(results);
        }
        [TestMethod]
        public  void SubmitExamToStudent()
        {
            var exam =  _driver.Initialize().GetAwaiter().GetResult();
            var result =  _driver.SubmitExamToStudent(exam).GetAwaiter().GetResult();

            Assert.IsTrue(result);
        }

        [TestMethod]
       
        public  void SubmitExamToTeacher()
        {
            var exam =  _driver.Initialize().GetAwaiter().GetResult();
            exam.ExamQuestion.FirstOrDefault().ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            ExamQuestionAnswer answer = new ExamQuestionAnswer();
            answer.ExamQuestion = exam.ExamQuestion.FirstOrDefault();
            answer.ExamQuestionAnswerId = 1;
            answer.ExamQuestionId = answer.ExamQuestion.ExamQuestionId;
            answer.IsAnswered = true;
            answer.Answer = 100;
            answer.IsCorrect = false;
            exam.ExamQuestion.FirstOrDefault().Answer = answer;
            exam.ExamQuestion.FirstOrDefault().ExamQuestionAnswer.Add(answer);



            var result = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        public  void TestCelciusTofahrenheit()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 19;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 5;
            question.DestinationConversionId = 5;
            question.DestinationConversion.ConversionName = "Fahrenheit";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 66.2;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public  void TestCelciusToCelcius()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();
           
            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();
          

            question.InputValue = 100;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 3;
            question.DestinationConversionId = 3;
            question.DestinationConversion.ConversionName = "Celsius";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;

            ExamQuestionAnswer answer = new ExamQuestionAnswer();
           
            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 100;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();
           
            var reports =  _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult(); 
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestCelciusToCelciusZero()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 0;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 3;
            question.DestinationConversionId = 3;
            question.DestinationConversion.ConversionName = "Celsius";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public  void TestCelciusToRankine()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 15;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 518.67;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
           
        }

        [TestMethod]
        public  void TestCelciusToRankineRoundUp()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 19;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 525.90;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }

        [TestMethod]
        public  void TestCelciusToRankineRoundDown()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 20.2;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 528.00;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }

        [TestMethod]
        public  void TestGalonsToTableSpoon()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 1;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 9;
            question.DestinationConversionId = 9;
            question.DestinationConversion.ConversionName = "Table Spoon";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 256;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public  void TestGalonsLiters()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 12;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 7;
            question.DestinationConversionId = 7;
            question.DestinationConversion.ConversionName = "Liters";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 45.4249;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
      

        [TestMethod]
        public  void TestGalonToCups()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 12;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 12;
            question.DestinationConversionId = 12;
            question.DestinationConversion.ConversionName = "Cups";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";


            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 192;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
      
        [TestMethod]
        public  void TestGalonsToCubicInches()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 10;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 10;
            question.DestinationConversionId = 10;
            question.DestinationConversion.ConversionName = "Cubic Inches";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 2310;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public  void TestGalonsToCubicFeet()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 100;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 14;
            question.DestinationConversionId = 14;
            question.DestinationConversion.ConversionName = "Cubic Feet";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 13.3681;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestGalonsToGalons()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 100;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 15;
            question.DestinationConversionId = 15;
            question.DestinationConversion.ConversionName = "Galons";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 100;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsTrue(report.IsCorrect.Value);
        }

        [TestMethod]
        public  void GetExamByTeacher()
        {

            var results =  _driver.GetExamByTeacher(1).GetAwaiter().GetResult();

            Assert.IsNotNull(results);
        }
        #endregion

        #region FalureTests



        [TestMethod]
        
        public void GetExamFail()
        {
            _driver.GetExam(-1000);
        }

        [TestMethod]
        
        public void GetExamByUserFail()
        {
          var results =  _driver.GetExamByUser(-1000).GetAwaiter().GetResult();
            Assert.AreEqual(results.Count(), 0);
        }


        [TestMethod]
        
        public void GetExamQuestionsFail()
        {
           var results = _driver.GetExamQuestions(-1000).GetAwaiter().GetResult();
            Assert.AreEqual(results.Count(), 0);
        }
        [TestMethod]
       
        public void SubmitExamToStudentFail()
        {
            Exam exam = _driver.Initialize().GetAwaiter().GetResult();
            exam.ExamId = -1000;
           var results =  _driver.SubmitExamToStudent(exam).GetAwaiter().GetResult();
            Assert.IsFalse(results);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void SubmitExamToTeacherFail()
        {
            try
            {
                Exam exam = _driver.Initialize().GetAwaiter().GetResult();
                exam.ExamId = -1000;
                var result = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
      
        public void GetExamByTeacherFail()
        {
          var results =  _driver.GetExamByTeacher(-1000).GetAwaiter().GetResult();

            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void TestCelciusTofahrenheitFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 0;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;



            question.DestinationConversion.ConversionId = 5;
            question.DestinationConversionId = 5;
            question.DestinationConversion.ConversionName = "Fahrenheit";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestCelciusToCelciusFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 100;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 3;
            question.DestinationConversionId = 3;
            question.DestinationConversion.ConversionName = "Celsius";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 60;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestCelciusToCelciusZeroFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 0;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 3;
            question.DestinationConversionId = 3;
            question.DestinationConversion.ConversionName = "Celsius";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = -23;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestCelciusToRankineFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 0;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);

        }

        [TestMethod]
        public void TestCelciusToRankineRoundUpFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 15;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 518.1;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }

        [TestMethod]
        public void TestCelciusToRankineRoundDownFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 15;
            question.SourceConversion.ConversionId = 3;
            question.SourceConversionId = 3;
            question.SourceConversion.ConversionName = "Celsius";
            question.SourceConversion.ConversionTypeId = 2;
            question.SourceConversion.ConversionType.ConversionTypeId = 2;

            question.DestinationConversion.ConversionId = 6;
            question.DestinationConversionId = 6;
            question.DestinationConversion.ConversionName = "Rankine";
            question.DestinationConversion.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionTypeId = 2;
            question.DestinationConversion.ConversionType.ConversionName = "Temperature";
            question.SourceConversion.ConversionType.ConversionName = "Temperature";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 518.0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }

        [TestMethod]
        public void TestGalonsToTableSpoonFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 100;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 9;
            question.DestinationConversionId = 9;
            question.DestinationConversion.ConversionName = "Table Spoon";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestGalonsLitersFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 10;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 7;
            question.DestinationConversionId = 7;
            question.DestinationConversion.ConversionName = "Liters";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;
            
            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }


        [TestMethod]
        public void TestGalonToCupsFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 100;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 12;
            question.DestinationConversionId = 12;
            question.DestinationConversion.ConversionName = "Cups";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";

            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }

        [TestMethod]
        public void TestGalonsToCubicInchesFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 19;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 10;
            question.DestinationConversionId = 10;
            question.DestinationConversion.ConversionName = "Cubic Inches";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public  void TestGalonsToCubicFeetFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 10;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 14;
            question.DestinationConversionId = 14;
            question.DestinationConversion.ConversionName = "Cubic Feet";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();


            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }
        [TestMethod]
        public void TestGalonsToGalonsFail()
        {
            var exam = _driver.Initialize().GetAwaiter().GetResult();

            ExamQuestion question = exam.ExamQuestion.FirstOrDefault();


            question.InputValue = 1000;
            question.SourceConversion.ConversionId = 15;
            question.SourceConversionId = 15;
            question.SourceConversion.ConversionName = "Galons";
            question.SourceConversion.ConversionTypeId = 1;
            question.SourceConversion.ConversionType.ConversionTypeId = 1;

            question.DestinationConversion.ConversionId = 15;
            question.DestinationConversionId = 15;
            question.DestinationConversion.ConversionName = "Galons";
            question.DestinationConversion.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionTypeId = 1;
            question.DestinationConversion.ConversionType.ConversionName = "Volume";
            question.SourceConversion.ConversionType.ConversionName = "Volume";
            ExamQuestionAnswer answer = new ExamQuestionAnswer();

            answer.ExamQuestion = question;
            answer.IsAnswered = true;
            answer.ExamQuestionAnswerId = 1;
            answer.Answer = 0;
            answer.ExamQuestionId = question.ExamQuestionId;
            question.ExamQuestionAnswer = new List<ExamQuestionAnswer>();
            question.ExamQuestionAnswer.Add(answer);
            question.Answer = answer;

            _driver.AddAnswer(answer).GetAwaiter().GetResult();

            var reports = _driver.SubmitExamToTeacher(exam).GetAwaiter().GetResult();
            var report = reports.FirstOrDefault(x => x.ExamQuestion.ExamQuestionId == answer.ExamQuestionId);

            Assert.IsFalse(report.IsCorrect.Value);
        }

      
        #endregion
    }
}
