# Tutorials-net6-0-DataAccess-RazorPages
https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/?view=aspnetcore-6.0
https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/data/ef-rp/intro/samples

## Part 01: Get started with Razor Pages in ASP.NET Core
1) Create the web app project

        dotnet new webapp -o ContosoUniversity
        cd ContosoUniversity
        dotnet add package Microsoft.EntityFrameworkCore.SqlServer
        dotnet add package Microsoft.EntityFrameworkCore.Design
        dotnet add package Microsoft.EntityFrameworkCore.Tools
        dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
        dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
        
2) Set up the site style: Shared/_Layout, Index
3) Create data models: Student, Enrollment, Course
4) [Scaffold the Student pages](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-6.0 "ASP.NET Core scaffolding engine")

        dotnet aspnet-codegenerator razorpage -m Student -dc SchoolContext -udl -outDir Pages\Students --referenceScriptLibraries
        
5) Update the database context class & connection string (appsettings.json)
6) Add the database exception filter (Program.cs)
7) Performance considerations:
    - Only database interactions are executed asynchronously: ToListAsync, SingleOrDefaultAsync, FirstOrDefaultAsync, SaveChangesAsync
    - A query should limit rows with Take() or use paging, not load an arbitrary/infinite number of rows
    - Use FirstOrDefaultAsync() to read one entity; FindAsync() is optimized to look up a single entity but cannot use with Include()

8) Review and customize the Student scaffolded code: protect against overposting attacks
9) Add sorting, filtering, and paging functionality to the Students pages
10) Add grouping view-model and About page
11) Create an initial migration; remove EnsureCreated()

        dotnet ef migrations add "InitialCreate" --context SchoolContext --output-dir ./Data/Migrations
        dotnet ef database update
        
12) [Update to complex data models](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/complex-data-model?view=aspnetcore-6.0 "Create a complex data model"): Student, Instructor, OfficeAssignment, Course
13) Create many-to-many table with payload: Enrollment
    - many-to-many join table without payload is sometimes called a pure join table (PJT): Instructor and Course entities
14) Scaffold & modify the Course & Instructor pages
15) Add ConcurrencyToken to Department model; create migration

        dotnet ef migrations add RowVersion
        dotnet ef database update
        
16) Scaffold the Department pages; use "Reference Script Libraries" (jquery.validate) on pages with forms to validate user input

        dotnet aspnet-codegenerator razorpage -m Department -dc SchoolContext -udl -outDir Pages\Departments --referenceScriptLibraries
        
17) Update the Department pages to use ConcurrencyToken: Index, Edit, Delete
