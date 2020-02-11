using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class QrtzLocks
    {
        public string SchedName { get; set; }
        public string LockName { get; set; }
    }
}
