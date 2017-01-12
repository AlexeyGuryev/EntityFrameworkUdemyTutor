
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

            Console.WriteLine("\nPress 'Enter' to explicit/eager loading");
            Console.ReadLine();

            //lazy
            var author = ctx.Authors.FirstOrDefault();
            var authorCourses = author.Courses.ToList();

            //eager allows preload only all Courses
            var authorsEagerCourses = ctx.Authors.Include(a => a.Courses).ToList();
            
            //explicit allows preload filtered Courses
            var authorsExplicitCourses = ctx.Authors.ToList();
            var authorIds = authorsExplicitCourses.Select(a => a.Id);

            ctx.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();

            Console.WriteLine("\nPress 'Enter' to close");
            Console.ReadLine();
        }
    }
}
