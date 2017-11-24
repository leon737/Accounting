using System;

namespace Cash.Domain.Models
{
    public class Currency : EntityWithFullStatisticalData
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
