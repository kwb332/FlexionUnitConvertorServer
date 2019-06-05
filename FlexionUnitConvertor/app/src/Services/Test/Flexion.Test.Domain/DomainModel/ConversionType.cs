using System;
using System.Collections.Generic;

namespace Flexion.Test.Domain.DomainModel
{
    public  class ConversionType
    {
 

        public int ConversionTypeId { get; set; }
        public string ConversionName { get; set; }

        public List<Conversion> Conversion { get; set; }
    }
}
