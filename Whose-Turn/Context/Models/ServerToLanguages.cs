using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerToLanguages
    {
        public Guid Id { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? ServerServerId { get; set; }

        public virtual Languages Language { get; set; }
        public virtual Servers ServerServer { get; set; }
    }
}
