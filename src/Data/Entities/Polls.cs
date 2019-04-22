using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Polls
    {
        public Polls()
        {
            PollOptions = new HashSet<PollOptions>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public string QuestionText { get; set; }

        public ICollection<PollOptions> PollOptions { get; set; }
    }
}
