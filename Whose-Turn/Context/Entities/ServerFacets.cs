using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerFacets
    {
        public Guid ServerId { get; set; }
        public Guid FacetId { get; set; }

        public virtual Facets Facet { get; set; }
        public virtual Servers Server { get; set; }
    }
}
