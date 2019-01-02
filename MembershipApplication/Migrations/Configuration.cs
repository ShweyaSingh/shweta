namespace MembershipApplication.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using MembershipApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MembershipApplication.DAL.MembershipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MembershipApplication.DAL.MembershipContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            try
            {
                var User = new List<User>
                {
                    new User {
                        Name = "Deepak Kapoor",
                        Age = "26",
                        Gender = "Male",
                        PhoneNumber = "8447745452",
                        Address = "A-113, Pankha Road, Uttam Nagar,New Delhi 110059, Opp to uttam nagar east metro station and pillar, no-644",
                        Email ="deepakgomic@gmail.com",
                        Password = "police@10000",
                        IsAdmin = true,
                        MembershipNumber = "gomic1"
                    }
                };
                User.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }
    }
}
