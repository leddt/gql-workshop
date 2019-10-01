using System;
using System.Collections.Generic;

namespace GqlWorkshop.DbModel
{
    public class Quote
    {
        public long Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string SaidBy { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}