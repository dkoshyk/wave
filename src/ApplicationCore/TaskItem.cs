﻿namespace ApplicationCore;

public class TaskItem : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// Global, Personal
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// New, InProggress, Done
    /// TODO: how to make string enum?
    /// </summary>
    public string Status { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime? DeadlineOn { get; set; }
    public DateTime? ClosedOn { get; set; }

    public int? OwnerId { get; set; }
    public User Owner { get; set; }
}
