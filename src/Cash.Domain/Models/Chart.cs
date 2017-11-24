using System;

namespace Cash.Domain.Models
{
    public class Chart : EntityWithFullStatisticalData
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
    }
}
