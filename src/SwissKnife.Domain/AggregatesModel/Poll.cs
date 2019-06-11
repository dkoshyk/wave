using System.Collections.Generic;

namespace SwissKnife.API.Data.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<PollOption> PollOptions { get; set; }
    }
}