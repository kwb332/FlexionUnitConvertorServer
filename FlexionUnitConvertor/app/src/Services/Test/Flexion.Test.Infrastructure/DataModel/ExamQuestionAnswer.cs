using System;
using System.Collections.Generic;

namespace Flexion.Test.Infrastructure.DataModel
{ 
    public partial class ExamQuestionAnswer
    {
        public int ExamQuestionAnswerId { get; set; }
        public int ExamQuestionId { get; set; }
        public double? Answer { get; set; }
        public bool? IsAnswered { get; set; }
        public bool? IsCorrect { get; set; }

        public ExamQuestion ExamQuestion { get; set; }
    }
}
