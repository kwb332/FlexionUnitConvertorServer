using System;
using System.Collections.Generic;

namespace Flexion.Test.Domain.DomainModel
{
    public  class Exam
    {
        public Exam()
        {
          
        }
        public int ExamId { get; set; }
        public String Description { get; set; }
        public int StudentId { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsGraded { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int? TeacherId { get; set; }
        public bool? IsCreated { get; set; }
        public List<ExamQuestion> ExamQuestion { get; set; }
    }
}
