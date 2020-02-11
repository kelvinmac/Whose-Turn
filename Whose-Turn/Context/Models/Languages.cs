using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Languages
    {
        public Languages()
        {
            ServerToLanguages = new HashSet<ServerToLanguages>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LanguageJson { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<ServerToLanguages> ServerToLanguages { get; set; }
    }
}
