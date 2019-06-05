using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flexion.Test.API.Models.GraphQLModels.ModelTypes
{
    public class ConversionType : ObjectGraphType<Domain.DomainModel.Conversion>
    {
        public ConversionType()
        {
            Field(x => x.ConversionId, true);
            Field(x => x.ConversionName);
            Field(x => x.ConversionTypeId);
            Field("ConversionTypeName",x => x.ConversionType.ConversionName,true);
            Field(x => x.ConversionValue,true);
            
        }
    }
}
