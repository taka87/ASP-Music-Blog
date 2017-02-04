using Blog.Models;

namespace Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123", "System Administrator");
                CreateUser(context, "pesho@gmail.com", "123", "Peter Ivanov");
                CreateUser(context, "merry@gmail.com", "123", "Maria Petrova");
                CreateUser(context, "geshu@gmail.com", "123", "George Petrov");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");

                CreatePost(context,
                    title: "State of Rock music",
                    body: "What do you think about today's rock scene?",
                    date: new DateTime(2016, 03, 27, 17, 53, 48),
                    authorUsername: "pesho@gmail.com",
                    category: "Rock"
                );

                CreatePost(context,
                    title: "Upcoming concerts?",
                    body: "Will there be any concerts for the rest of the year from bands like Metallica,Bullet For My Valentine or Disturbed ?",
                    date: new DateTime(2016, 05, 11, 08, 22, 03),
                    authorUsername: "merry@gmail.com",
                    category: "Metal"
                );

                CreatePost(context,
                    title: "What happened to Classic Rock performers",
                    body: "What happened to artists like Alice Cooper, John Bon Jovi, Aerosmith ?  Why did they drop in popularity?",
                    date: new DateTime(2016, 03, 27, 17, 53, 48),
                    authorUsername: "merry@gmail.com",
                    category: "Rock"
                );

                CreatePost(context,
                    title: "Types of Heavy Metal",
                    body: "Which type of Heavy Metal do you guys prefer e.g Deaht Metal,Black Metal,Metalcore,Symphonic Metal?",
                    date: new DateTime(2016, 02, 18, 22, 14, 38),
                    authorUsername: "pesho@gmail.com",
                    category: "Metal"
                );

                CreatePost(context,
                    title: "The FOREIGNER concert",
                    body: "What do you guys think of their performace?",
                    date: new DateTime(2016, 04, 11, 19, 02, 05),
                    authorUsername: "geshu@gmail.com",
                    category: "Rock"
                );

                CreatePost(context,
                    title: "Headbanger",
                    body: "What do i need to become a Headbanger, or more commonly known a Metalhead?",
                    date: new DateTime(2016, 06, 30, 17, 36, 52),
                    authorUsername: "merry@gmail.com",
                    category: "Metal"
                );

                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context,
            string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string body, DateTime date, string authorUsername, string category)
        {
            var question = new Question();
            question.Title = title;
            question.Body = body;
            question.Date = date;
            question.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            question.Category = category;
            context.Questions.Add(question);
        }

    }
}
