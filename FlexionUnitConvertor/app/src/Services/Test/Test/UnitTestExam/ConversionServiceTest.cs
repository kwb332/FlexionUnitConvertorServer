using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestExam
{
    public class ConversionServiceTest : IConversionService
    {
     
        public ConversionServiceTest()
        {
           
        }
        public async Task<bool> GradeExam(List<ExamQuestion> examQuestions, List<Conversion> convertionTable)
        {
            try
            {
                var volumeConversions = convertionTable.Where(x => x.ConversionType.ConversionName == "Volume").ToList();
                foreach (var question in examQuestions)
                {
                    if (question.SourceConversion.ConversionType.ConversionName == "Volume")
                    {
                        GradVolumeConversion(question, volumeConversions);
                    }
                    else
                    {
                        GradeTemperatureConversion(question);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void GradeTemperatureConversion(ExamQuestion question)
        {
            var answer = question.ExamQuestionAnswer.FirstOrDefault().Answer;
            double? results = NormalizeToCelius(question);
            switch (question.DestinationConversion.ConversionName)
            {

                case "Kelvin":
                    results = results + 273.15;
                    break;
                case "Fahrenheit":
                    results = (results * 9 / 5) + 32;
                    break;
                case "Rankine":
                    results = (results * 9 / 5) + 491.67;
                    break;
            }

            var studentResponse = RoundOff(answer.Value, 10);
            results = RoundOff(results.Value, 10);
            if (results == studentResponse)
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
            else
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
           

        }
        private double? NormalizeToCelius(ExamQuestion question)
        {
            var answer = question.ExamQuestionAnswer.FirstOrDefault();
            double? results = null;
            switch (question.SourceConversion.ConversionName)
            {
                case "Celsius":
                    results = question.InputValue;
                    break;
                case "Kelvin":
                    results = question.InputValue - 273.15;
                    break;
                case "Fahrenheit":
                    results = (question.InputValue - 32) * (5 / 9);
                    break;
                case "Rankine":
                    results = (question.InputValue - 491.67) * (5 / 9);
                    break;
            }
            return results;
        }



        private void GradVolumeConversion(ExamQuestion question, List<Conversion> volumeConversions)
        {
            var inputValue = question.InputValue;
            var sourceType = volumeConversions.FirstOrDefault(x => x.ConversionId == question.SourceConversionId);
            var destinationType = volumeConversions.FirstOrDefault(x => x.ConversionId == question.DestinationConversionId);

            var baseConversion = inputValue / sourceType.ConversionValue;
            var correctAnswer = baseConversion * destinationType.ConversionValue;
            var studentResponse = RoundOff(question.ExamQuestionAnswer.FirstOrDefault().Answer.Value, 10);
            correctAnswer = RoundOff(correctAnswer.Value, 10);
            if (correctAnswer == studentResponse)
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
            else
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
            
        }
        public double RoundOff(double number, double interval)
        {
            double remainder = number % interval;
            number += (remainder < interval / 2) ? -remainder : (interval - remainder);
            return number;
        }
    }
}
