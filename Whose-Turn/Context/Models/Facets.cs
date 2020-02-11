using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Facets
    {
        public Facets()
        {
            BaseLevel = new HashSet<BaseLevel>();
            ServerFacets = new HashSet<ServerFacets>();
        }

        public Guid FacetId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Archived { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public Guid? ArchivedBy { get; set; }
        public string FacetName { get; set; }
        public Guid OwnerId { get; set; }

        public virtual ICollection<BaseLevel> BaseLevel { get; set; }
        public virtual ICollection<ServerFacets> ServerFacets { get; set; }
    }
}
