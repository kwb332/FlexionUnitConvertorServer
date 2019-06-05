using System;
using System.Collections.Generic;

namespace Flexion.Test.Infrastructure.DataModel
{
    public partial class Conversion
    {
        //class handles conversions
        public Conversion()
        {
            ExamQuestionDestinationConversion = new HashSet<ExamQuestion>();
            ExamQuestionSourceConversion = new HashSet<ExamQuestion>();
         }

        public int ConversionId { get; set; }
        public int ConversionTypeId { get; set; }
        public string ConversionName { get; set; }
        public double? ConversionValue { get; set; }

        public ConversionType ConversionType { get; set; }
        public ICollection<ExamQuestion> ExamQuestionDestinationConversion { get; set; }
        public ICollection<ExamQuestion> ExamQuestionSourceConversion { get; set; }
    }
}
