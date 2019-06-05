using System;
using System.Collections.Generic;

namespace Flexion.Test.Domain.DomainModel
{
    public  class ExamQuestion
    {
       
        public int ExamQuestionId { get; set; }
        public int ExamId { get; set; }
        public int SourceConversionId { get; set; }
        public int DestinationConversionId { get; set; }
        public double? InputValue { get; set; }
        public Conversion DestinationConversion { get; set; }
        public Exam Exam { get; set; }
        public Conversion SourceConversion { get; set; }
        public ExamQuestionAnswer Answer { get; set; }
        public List<ExamQuestionAnswer> ExamQuestionAnswer { get; set; }
    }
}
