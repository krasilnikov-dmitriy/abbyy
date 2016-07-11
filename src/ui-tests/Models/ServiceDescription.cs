using System;
using System.Collections.Generic;
using System.Linq;

namespace UITests.Models
{
    public enum ServiceType
    {
        Express,
        Standart,
        Professional,
        Expert
    }

    public class ServiceDescription
    {
        public ServiceType Type { get; set; }

        public Decimal Price { get; set; }

        public List<Feature> Features { get; set; } = new List<Feature>();

        public override string ToString()
        {
            return String.Format("ServiceDescription:[ Type = {0}, Price = {1}  \n Features: [ {2} ]]", Type, Price, String.Join(", ", Features.Where(f => f != null ).Select(f => f.ToString())));
        }
    }
}

