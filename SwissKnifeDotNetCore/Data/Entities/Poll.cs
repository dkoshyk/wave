using System.Collections.Generic;

namespace SwissKnifeDotNetCore.Data.Entities
{
    public class Poll
    {
        public Poll()
        {
            //PollOptions = new HashSet<PollOption>();
        }
        
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<PollOption> PollOptions { get; set; }
    }
}