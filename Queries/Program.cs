
using Queries.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PlutoContext();

            Queries(ctx);

            Console.WriteLine("\nPress 'Enter' to explicit/eager/lazy loading");
            Console.ReadLine();

            Loading(ctx);

            Console.WriteLine("\nPress 'Enter' to change tracker states");
            Console.ReadLine();

            ChangeTracking(ctx);

            Console.WriteLine("\nPress 'Enter' to try repositories");
            Console.ReadLine();

            TryRepository(ctx);

            Console.WriteLine("\nPress 'Enter' to close");
            Console.ReadLine();
        }

        private static void TryRepository(PlutoContext ctx)
        {
            using (var uow = new UnitOfWork(ctx))
            {
                uow.Courses.ListWithAuthorsPaged(0, 10).ToList().ForEach(c => Console.WriteLine(c.Name));

                var course = uow.Courses.GetById(1);
                if (course != null)
                {
                    course.Name = "000000";

                }
                var author = uow.Authors.GetById(1);
                if (author != null)
                {
                    uow.Courses.RemoveRange(author.Courses);
                    uow.Authors.Remove(author);
                }
                uow.SaveChanges();

                uow.Courses.ListWithAuthorsPaged(0, 10).ToList().ForEach(c => Console.WriteLine(c.Name));
            }
        }

        private static void ChangeTracking(PlutoContext ctx)
        {
            ctx.Authors.Add(new Author { Name = "Petroff" });

            var author = ctx.Authors.Find(3);
            author.Name = "Updated";

            foreach (var entry in ctx.ChangeTracker.Entries())
            {
                Console.WriteLine(entry.State);
                if (entry.State == EntityState.Modified)
                {
                    entry.Reload();
                    Console.WriteLine("reload state: " + entry.State);
                }
            }
        }

        private static void Loading(PlutoContext ctx)
        {
            //lazy
            var author = ctx.Authors.FirstOrDefault();
            var authorCourses = author.Courses.ToList();

            //eager allows preload only all Courses
            var authorsEagerCourses = ctx.Authors.Include(a => a.Courses).ToList();

            //explicit allows preload filtered Courses
            var authorsExplicitCourses = ctx.Authors.ToList();
            var authorIds = authorsExplicitCourses.Select(a => a.Id);

            ctx.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();
        }

        private static void Queries(PlutoContext ctx)
        {
            Console.WriteLine("\n---Group example---");
            var groupQuery = ctx.Courses.GroupBy(c => c.Level).ToList();
            foreach (var item in groupQuery)
            {
                Console.WriteLine(string.Format("{0} ({1})", item.Key, item.Count()));
                item.ToList().ForEach(i => Console.WriteLine("-" + i.Name));
            }

            Console.WriteLine("\n---Join example---");
            var joinQuery = ctx.Courses.Join(ctx.Authors, c => c.AuthorId, a => a.Id,
                (c, a) => new { CourseName = c.Name, AuthorName = a.Name });

            joinQuery.ToList()
                .ForEach(j => Console.WriteLine(string.Format("{0} ({1})", j.AuthorName, j.CourseName)));

            Console.WriteLine("\n---GroupJoin example---");
            var groupJoinQuery = ctx.Authors.GroupJoin(ctx.Courses, a => a.Id, c => c.AuthorId,
                (a, courses) => new { AuthorName = a.Name, CoursesCount = courses.Count() });

            groupJoinQuery.ToList()
                .ForEach(gj => Console.WriteLine(string.Format("{0} ({1})", gj.AuthorName, gj.CoursesCount)));
        }
    }
}
