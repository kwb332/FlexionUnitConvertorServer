using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flexion.Test.Infrastructure.DataModel;

namespace Flexion.Test.Domain.DomainInterface
{
    public interface IConversionService
    {
        Task<bool> GradeExam(List<ExamQuestion> examQuestions, List<Conversion> convertionTable);
    }
}
