using System;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Domain.AggregatesModel.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        // Value Object pattern example persisted as EF Core 2.0 owned entity
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        private DateTime _createdDate;

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
