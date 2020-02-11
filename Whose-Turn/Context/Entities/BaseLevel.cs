using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class BaseLevel
    {
        public BaseLevel()
        {
            SubscriptionData = new HashSet<SubscriptionData>();
        }

        public Guid LevelId { get; set; }
        public string LevelData { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreateBy { get; set; }
        public bool Listed { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public Guid? ArchivedBy { get; set; }
        public string LevelName { get; set; }
        public Guid FacetId { get; set; }
        public int SlotCount { get; set; }
        public bool HasAvatar { get; set; }
        public string AvatarExtension { get; set; }

        public virtual Facets Facet { get; set; }
        public virtual LevelManifests LevelManifests { get; set; }
        public virtual ICollection<SubscriptionData> SubscriptionData { get; set; }
    }
}
