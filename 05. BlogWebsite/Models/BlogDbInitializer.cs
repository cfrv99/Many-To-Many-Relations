using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebsite.Models
{
    static public class BlogDbInitializer
    {
        static public void Seed(IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<BlogDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "Sport" });
                context.Categories.Add(new Category { Name = "Beer" });
                context.Categories.Add(new Category { Name = "Games" });
                context.Categories.Add(new Category { Name = "Study" });
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                context.Tags.Add(new Tag { Name = "One" });
                context.Tags.Add(new Tag { Name = "Two" });
                context.Tags.Add(new Tag { Name = "Three" });
                context.Tags.Add(new Tag { Name = "Four" });
                context.Tags.Add(new Tag { Name = "Five" });
                context.SaveChanges();
            }
        }
    }
}
