using System.Diagnostics.CodeAnalysis;

namespace shortenyour.link.Models
{
    public class Link
    {
        public int Id { get; set; }
        public int Click_Count { get; set; } = 0;
        public string OwnerMail { get; set; }
        public string OwnerId { get; set; }
        [AllowNull]
        public string originalUrl { get; set; }
        public string LinkUrl { get; set; }
        public decimal LinkBalance { get; set; }
    }
}
