using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class PollOptions
    {
        public int Id { get; set; }
        public string Answers { get; set; }
        public int PollId { get; set; }
        public int Vote { get; set; }

        public Polls Poll { get; set; }
    }
}
