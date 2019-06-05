using Flexion.Report.API.Models.GraphQLModels.ModelTypes;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Report.API.Models.GraphQLModels.InputTypes
{
    public class ReportInputType : InputObjectGraphType
    {
        public ReportInputType()
        {
            Name = "AddReportInput";
            Field<StringGraphType>("StudentName");
            Field<NonNullGraphType<StringGraphType>>("TeacherName");
            Field<NonNullGraphType<FloatGraphType>>("InputValue");
            Field<NonNullGraphType<StringGraphType>>("InputUnitOfMeasure");
            Field<NonNullGraphType<StringGraphType>>("OutPutUnitOfMeasure");
            Field<NonNullGraphType<FloatGraphType>>("StudentResponse");
            Field<NonNullGraphType<BooleanGraphType>>("IsCorrect");
            Field<NonNullGraphType<IntGraphType>>("StudentID");
            Field<NonNullGraphType<IntGraphType>>("ExamID");
            Field<NonNullGraphType<DateGraphType>>("ExamDate");
            Field<NonNullGraphType<StringGraphType>>("ExamDescription");

            

        }
    }
}

