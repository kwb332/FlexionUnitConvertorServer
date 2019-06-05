using System;
using System.Collections.Generic;

namespace Flexion.Test.Domain.DomainModel
{
    public partial class Report
    {

        public int? ReportID { get; set; }
        public int? ExamId { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamDescription { get; set; }
        public ExamQuestion ExamQuestion { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public double? InputValue { get; set; }
        public string InputUnitOfMeasure { get; set; }
        public string OutPutUnitOfMeasure { get; set; }
        public double? StudentResponse { get; set; }
        public bool? IsCorrect { get; set; }
        public int StudentID { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
        public int? TeacherID { get;  set; }
    }
}
