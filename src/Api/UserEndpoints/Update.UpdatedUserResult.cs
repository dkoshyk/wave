﻿using System;

namespace Api.UserEndpoints
{
    public class UpdatedUserResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime? DeadlineOn { get; set; }
        public DateTime? ClosedOn { get; set; }
    }
}
