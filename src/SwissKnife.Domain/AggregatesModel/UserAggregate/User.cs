using System;
using System.Collections.Generic;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<WishProduct> WishProducts { get; set; }

        public User()
        {
            CreatedOn = DateTime.Now;
            LastEditedOn = DateTime.Now;
        }
    }
}