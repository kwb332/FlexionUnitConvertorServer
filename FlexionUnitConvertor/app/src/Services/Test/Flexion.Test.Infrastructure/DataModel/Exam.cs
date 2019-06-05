using System;
using System.Collections.Generic;

namespace Flexion.Test.Infrastructure.DataModel
{
    public partial class Exam
    {
        public Exam()
        {
            ExamQuestion = new List<ExamQuestion>();
        }

        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsGraded { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int? TeacherId { get; set; }
        public bool? IsCreated { get; set; }
        public List<ExamQuestion> ExamQuestion { get; set; }
        public string Description { get; set; }
    }
}
