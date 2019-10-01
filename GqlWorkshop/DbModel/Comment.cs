using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GqlWorkshop.DbModel
{
    public class Comment
    {
        public long Id { get; set; }

        public DateTime PostedAt { get; set; }
        public string Message { get; set; }
        public string PostedBy { get; set; }

        [ForeignKey("Quote")]
        public long QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}