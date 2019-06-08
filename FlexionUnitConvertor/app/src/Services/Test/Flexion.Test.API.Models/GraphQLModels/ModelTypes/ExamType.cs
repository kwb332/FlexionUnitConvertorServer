using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.ModelTypes
{
    public class ExamType : ObjectGraphType<Domain.DomainModel.Exam>
    {
        public ExamType()
        {
            Field(x => x.ExamId, true);
            Field(x => x.DateCompleted, true);
            Field(x => x.DateCreated,true);
            Field(x => x.Description,true);
            Field(x => x.IsComplete,true);
            Field(x => x.IsCreated,true);
            Field(x => x.IsGraded,true);
            Field(x => x.StudentName, true);
            Field(x => x.TeacherName, true);
            Field(x => x.StudentId,true);
            Field(x => x.TeacherId,true);
       
            Field<ExamType>("ExamQuestion", resolve: context => context.Source.ExamQuestion);
          

        }
    }
}
