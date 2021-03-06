﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Report.Domain.DomainModel
{
    public class ExamQuestion
    {
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public double? InputValue { get; set; }
        public string InputUnitOfMeasure { get; set; }
        public string OutPutUnitOfMeasure { get; set; }
        public double? StudentResponse { get; set; }
        public bool? IsCorrect { get; set; }
        public int StudentID { get; set; }
        public int? ExamId { get; set; }
    }
}
