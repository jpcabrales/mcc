namespace MoulaChallenge.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MoulaChallenge.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MoulaChallenge.Models.MoulaServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoulaChallenge.Models.MoulaServiceContext context)
        {
            context.Statuses.AddOrUpdate(s => s.Id,
                new Status() { Id = 1, Name = "Open" },
                new Status() { Id = 2, Name = "Closed" }
            );

            context.Accounts.AddOrUpdate(x => x.Id,
                new Account() { Id = 1, Name = "Juan de la Cruz" },
                new Account() { Id = 2, Name = "Huckleberry Finn" },
                new Account() { Id = 3, Name = "Uncle Sam" }
                );

            context.Payments.AddOrUpdate(x => x.Id,
                new Payment()
                {
                    Id = 1,
                    Date = new DateTime(2021, 1, 1),
                    Amount = 100.00,
                    Status = 2,
                    Reason = "Transaction completed.",
                    AccountId = 1
                },
                new Payment()
                {
                    Id = 2,
                    Date = new DateTime(2021, 2, 23),
                    Amount = 76.00,
                    Status = 2,
                    Reason = "Transaction completed.",
                    AccountId = 1
                },
                new Payment()
                {
                    Id = 3,
                    Date = new DateTime(2021, 1, 3),
                    Amount = 150.00,
                    Status = 2,
                    Reason = "Transaction completed.",
                    AccountId = 1
                },
                new Payment()
                {
                    Id = 1,
                    Date = new DateTime(2021, 2, 1),
                    Amount = 100.00,
                    Status = 2,
                    Reason = "Transaction completed.",
                    AccountId = 1
                }
                );
        }
    }
}
