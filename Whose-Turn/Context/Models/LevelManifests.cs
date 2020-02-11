using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class LevelManifests
    {
        public Guid Id { get; set; }
        public Guid LevelId { get; set; }
        public string LevelType { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool FeeIncludedInPrice { get; set; }

        public virtual BaseLevel Level { get; set; }
    }
}
