using System;
using Newtonsoft.Json;

namespace Whose_Turn.Models
{
    public partial class ErrorModel
    {
        /// <summary>
        /// Gets or sets the error type
        /// </summary>
        public string Type { get; set; } = "Internal";

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the trace id
        /// </summary>
        public string TraceId { get; set; }
    }
}
