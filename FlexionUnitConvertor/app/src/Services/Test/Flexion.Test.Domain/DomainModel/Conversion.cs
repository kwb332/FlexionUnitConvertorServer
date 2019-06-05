using System;
using System.Collections.Generic;

namespace Flexion.Test.Domain.DomainModel
{
    public  class Conversion
    {
        
        public int ConversionId { get; set; }
        public int ConversionTypeId { get; set; }
        public string ConversionName { get; set; }
        public double? ConversionValue { get; set; }

        public ConversionType ConversionType { get; set; }
        public ICollection<ExamQuestion> ExamQuestionDestinationConversion { get; set; }
        public ICollection<ExamQuestion> ExamQuestionSourceConversion { get; set; }
    }
}
