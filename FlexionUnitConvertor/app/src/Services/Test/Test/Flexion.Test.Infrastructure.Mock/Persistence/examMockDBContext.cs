using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flexion.Test.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flexion.Test.Infrastructure.Mock.Persistence
{
    public partial class ExamMockDBContext : DbContext
    {
        public ExamMockDBContext()
        {
           

        }

        public async Task<Exam> InitializeDB()
        {
            Conversion = new List<Conversion>();
            DataModel.Conversion conversion = new Conversion();
            conversion.ConversionId = 1;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Temperature",
                ConversionTypeId = 2

            };
            conversion.ConversionName = "Kelvin";
            conversion.ConversionValue = 273.15;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 3;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Temperature",
                ConversionTypeId = 2

            };
            conversion.ConversionName = "Celsius";
            conversion.ConversionValue = 1;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 5;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Temperature",
                ConversionTypeId = 2

            };
            conversion.ConversionName = "Fahrenheit";
            conversion.ConversionValue = 33.8;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 6;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Temperature",
                ConversionTypeId = 2

            };
            conversion.ConversionName = "Rankine";
            conversion.ConversionValue = 493.47;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 7;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Liters";
            conversion.ConversionValue = 3.78541;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 9;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Tablespoons";
            conversion.ConversionValue = 256;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 10;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Cubic - inches";
            conversion.ConversionValue = 231;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 12;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Cups";
            conversion.ConversionValue = 16;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 14;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Cubic - feet";
            conversion.ConversionValue = 0.133681;
            Conversion.Add(conversion);

            conversion = new Conversion();
            conversion.ConversionId = 15;
            conversion.ConversionType = new DataModel.ConversionType()
            {
                ConversionName = "Volume",
                ConversionTypeId = 1

            };
            conversion.ConversionName = "Gallons";
            conversion.ConversionValue = 1;
            Conversion.Add(conversion);

            ConversionType = new List<ConversionType>();
            DataModel.ConversionType conversionType = new ConversionType();
            conversionType.ConversionTypeId = 1;
            conversionType.ConversionName = "Volume";
            ConversionType.Add(conversionType);

            conversionType = new ConversionType();
            conversionType.ConversionTypeId = 2;
            conversionType.ConversionName = "Temperature";
            ConversionType.Add(conversionType);

            Exam = new List<Exam>();
            Exam exam = new Exam();

            exam.ExamId = 1;
            exam.DateCompleted = null;
            exam.DateCreated = DateTime.Now;
            exam.Description = "Exam 1";
            exam.IsComplete = false;
            exam.IsCreated = true;
            exam.IsGraded = false;
            exam.TeacherId = 1;
            exam.StudentId = 1;

            ExamQuestion question = new ExamQuestion();
            question.ExamId = 1;
            question.ExamQuestionId = 1;
            question.InputValue = 23;
            question.SourceConversionId = 1;
            question.DestinationConversionId = 3;
           
           
            question.SourceConversion = new Conversion()
            {
                ConversionId = 1,
                ConversionTypeId = 2,
       
                ConversionName = "Kelvin",
                ConversionType = new ConversionType()
                {
                    ConversionTypeId = 2,
                    ConversionName = "Temperature"
                }
            };
            question.DestinationConversion = new Conversion()
            {
                ConversionId = 3,
                ConversionTypeId = 2,
                ConversionName = "Celsius",
                ConversionType = new ConversionType()
                {
                    ConversionTypeId = 2,
                    ConversionName = "Temperature"
                }
            };
           
            question.Exam = exam;
            
         
          

           
            exam.ExamQuestion = new List<ExamQuestion>();
            exam.ExamQuestion.Add(question);
           
            ExamQuestion = new List<ExamQuestion>();
            ExamQuestion.Add(question);
          
            Exam.Add(exam);
           
            ExamQuestionAnswer = new List<ExamQuestionAnswer>();

            return exam;
        }

        public virtual List<Conversion> Conversion { get; set; }
        public virtual List<ConversionType> ConversionType { get; set; }
        public virtual List<Exam> Exam { get; set; }
        public virtual List<ExamQuestion> ExamQuestion { get; set; }
        public virtual List<ExamQuestionAnswer> ExamQuestionAnswer { get; set; }


    }
}
