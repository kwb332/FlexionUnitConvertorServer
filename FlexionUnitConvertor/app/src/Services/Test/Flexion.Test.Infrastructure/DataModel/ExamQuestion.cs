using System;
using System.Collections.Generic;

namespace Flexion.Test.Infrastructure.DataModel
{
    public partial class ExamQuestion
    {
        public ExamQuestion()
        {
            ExamQuestionAnswer = new HashSet<ExamQuestionAnswer>();
        }

        public int ExamQuestionId { get; set; }
        public int ExamId { get; set; }
        public int SourceConversionId { get; set; }
        public int DestinationConversionId { get; set; }
         public double? InputValue { get; set; }
        public Conversion DestinationConversion { get; set; }
        public Exam Exam { get; set; }
        public Conversion SourceConversion { get; set; }
        public ICollection<ExamQuestionAnswer> ExamQuestionAnswer { get; set; }
    }
}
