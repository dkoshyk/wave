namespace SwissKnife.API.Data.Entities
{
    public class PollOption
    {
        public int Id { get; set; }
        public string Answers { get; set; }
        public int Vote { get; set; }

        public int PollId { get; set; }
        public virtual Poll Poll { get; set; }
    }
}