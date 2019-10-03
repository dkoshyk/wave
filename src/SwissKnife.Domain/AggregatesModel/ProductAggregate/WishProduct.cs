using System;
using SwissKnife.Domain.AggregatesModel.UserAggregate;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Domain.AggregatesModel.ProductAggregate
{
    public class WishProduct : Entity, IAggregateRoot
    {
        // Value Object pattern example persisted as EF Core 2.0 owned entity
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        public string Name { get; set; }
        public Guid ProductId {get;set;}
        public virtual Product Product {get;set;}
        public Guid OwnerId { get; set; }
        public virtual User User {get;set;}

        public WishProduct()
        {
            CreatedOn = DateTime.Now;
            LastEditedOn = DateTime.Now;
        }
    }
}
