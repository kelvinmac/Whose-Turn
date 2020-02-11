using System;
namespace Whose_Turn.ConfigModels
{
    public class ServiceBusConfig
    {
        /// <summary>
        /// Gets or sets the error queue.
        /// </summary>
        public string ErrorQueue { get; set; }
        
        /// <summary>
        /// Gets or sets the api queue
        /// </summary>
        public string ApiQueue { get; set; }
    }
}
