using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILogger logger) 
        {
            var policy = CreatePolicy(logger, nameof(AppDbContext));

            await policy.ExecuteAsync(async () =>
            {
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                    //await context.Database.EnsureCreatedAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    var users = Users();
                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();
                }

                if (!await context.Tasks.AnyAsync())
                {
                    var tasks = Tasks();
                    await context.Tasks.AddRangeAsync(tasks);
                    await context.SaveChangesAsync();
                }
            });
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger logger, string prefix, int retries = 3)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }

        public static List<User> Users()
        {
            int id = 1;

            var users = new List<User>()
            {
                new User
                {
                    //Id = id++,
                    Login = "user" + id,
                    Password = "12345678",
                    FirstName = "James",
                    LastName = "Smith",
                },
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "John",
                //    LastName = "Johnson",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Robert",
                //    LastName = "Williams",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Michael",
                //    LastName = "Brown",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "William",
                //    LastName = "Jones",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "David",
                //    LastName = "Garcia",
                //},
                //new User
                //{
                //   // Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Richard",
                //    LastName = "Miller",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Joseph",
                //    LastName = "Davis",
                //},
                //new User
                //{
                //    //Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Thomas",
                //    LastName = "Rodriguez",
                //},
                //new User
                //{
                //    Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Charles",
                //    LastName = "Martinez",
                //},
                //new User
                //{
                //    Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Patricia",
                //    LastName = "Lopez",
                //},
                //new User
                //{
                //    Id = id++,
                //    Login = "user" + id,
                //    Password = "12345678",
                //    FirstName = "Jennifer",
                //    LastName = "Gonzales",
                //},
            };

            return users;
        }

        public static List<TaskItem> Tasks()
        {
            int id = 1;

            var tasks = new List<TaskItem>()
            {
                new TaskItem()
                {
                    //Id = id++,
                    OwnerId = 1,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Title = "Go to shop",
                    Type = "task",
                    Status = "new"
                },
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to shop",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to home",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to school",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to kindergarten",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to office",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to bed",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to the cinema",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to meeting",
                //    Type = "task",
                //    Status = "new"
                //},
                //new TaskItem()
                //{
                //    Id = id++,
                //    OwnerId = 1,
                //    CreatedOn = DateTime.Now,
                //    UpdatedOn = DateTime.Now,
                //    Title = "Go to prison",
                //    Type = "task",
                //    Status = "new"
                //}
            };

            return tasks;
        }
    }
}
