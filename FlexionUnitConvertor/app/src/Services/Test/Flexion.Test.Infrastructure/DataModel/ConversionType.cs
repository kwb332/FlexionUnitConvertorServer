using System;
using System.Collections.Generic;

namespace Flexion.Test.Infrastructure.DataModel
{
      public partial class ConversionType
    {
        public ConversionType()
        {
            //
            Conversion = new HashSet<Conversion>();
        }

        public int ConversionTypeId { get; set; }
        public string ConversionName { get; set; }

        public ICollection<Conversion> Conversion { get; set; }
    }
}
