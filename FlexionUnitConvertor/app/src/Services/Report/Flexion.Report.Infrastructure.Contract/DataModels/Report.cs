using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flexion.Report.Infrastructure.DataModel
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int? ExamId { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamDescription { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public double? InputValue { get; set; }
        public string InputUnitOfMeasure { get; set; }
        public string OutPutUnitOfMeasure { get; set; }
        public double? StudentResponse { get; set; }
        public bool? IsCorrect { get; set; }
        public int StudentID { get;  set; }

        [NotMapped]
        public ExamQuestion ExamQuestion { get; set; }
        [NotMapped]
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}
