using SQLite;
using SQLite.Net.Attributes;

namespace App3.Models
{
    public class Rate
    {
        [PrimaryKey]
        public int RateId { get; set; }

        public string Code { get; set; }

        public double TaxRate { get; set; }

        public string Name { get; set; }

        public override int GetHashCode() => RateId;
    }
}
