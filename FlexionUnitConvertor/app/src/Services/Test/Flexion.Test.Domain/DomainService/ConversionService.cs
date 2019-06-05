using Flexion.Test.Domain.DomainInterface;
using Flexion.Test.Infrastructure.DataModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flexion.Test.Infrastructure.InfrastructureInterface;

namespace Flexion.Test.Domain.DomainService
{
    public class ConversionService : IConversionService
    {
        private readonly ITestRepository _testRepository;
        public ConversionService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<bool> GradeExam(List<ExamQuestion> examQuestions, List<Conversion> convertionTable)
        {
            try
            {
                var volumeConversions = convertionTable.Where(x => x.ConversionType.ConversionName == "Volume").ToList();
                foreach (var question in examQuestions)
                {
                   if(question.SourceConversion.ConversionType.ConversionName == "Volume")
                    {
                        await GradeVolumeConversion(question, volumeConversions);
                    }
                   else
                    {
                       await GradeTemperatureConversion(question);
                    }
                  

                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task GradeTemperatureConversion(ExamQuestion question)
        {
            var answer = question.ExamQuestionAnswer.FirstOrDefault().Answer;
            double? results = NormalizeToCelius(question);
            switch (question.DestinationConversion.ConversionName)
            {
                
                case "Kelvin":
                    results = results + 273.15;
                    break;
                case "Fahrenheit":
                    double FahrenheitMultiplier = 9.0 / 5.0;
                    results = (results * FahrenheitMultiplier) + 32;
                    break;
                case "Rankine":
                    double rankineMultiplier = 9.0 / 5.0;
                    results = (results * rankineMultiplier) + 491.67;
                    break;
            }

            var studentResponse = Math.Round(answer.Value, 1);
            results = Math.Round(results.Value, 1);
            if (results == studentResponse)
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
            else
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = false;
            }
          await  _testRepository.GradeResponse(question.ExamQuestionAnswer.FirstOrDefault());

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
                    double fahrenheitMultiplier = 5.0 / 9.0;
                    results = ((question.InputValue.Value - 32) * fahrenheitMultiplier);
                    break;
                case "Rankine":
                    double rankineMultiplier = 5.0 / 9.0;
                    results = (question.InputValue - 491.67) * rankineMultiplier;
                    break;
            }
            return results;
        }
        


        private async Task GradeVolumeConversion(ExamQuestion question, List<Conversion> volumeConversions)
        {
            var inputValue = question.InputValue;
            var sourceType = volumeConversions.FirstOrDefault(x => x.ConversionId == question.SourceConversionId);
            var destinationType = volumeConversions.FirstOrDefault(x => x.ConversionId == question.DestinationConversionId);

            var baseConversion = inputValue /sourceType.ConversionValue;
            var correctAnswer = baseConversion * destinationType.ConversionValue;
            var studentResponse = Math.Round(question.ExamQuestionAnswer.FirstOrDefault().Answer.Value, 1);
             correctAnswer = Math.Round(correctAnswer.Value, 1);
            if (correctAnswer == studentResponse)
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = true;
            }
            else
            {
                question.ExamQuestionAnswer.FirstOrDefault().IsCorrect = false;
            }
           await _testRepository.GradeResponse(question.ExamQuestionAnswer.FirstOrDefault());
        }
      
    }
}
