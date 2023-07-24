using CompanyData.Data;
using CompanyData.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Companies"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Use EntityFramework, store data in memory (add a code that creates initial data at application startup)
using (var db = new DataContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Companies").Options))
{
    var initialCompanies = new[]
    {
        new Company { Id = 1, Name = "TechHub Solutions", City = "San Francisco", Address = "123 Main Street", State = "California", Phone = "(415) 555-1234" },
        new Company { Id = 2, Name = "GreenTech Industries", City = "New York City", Address = "456 Oak Avenue", State = "New York", Phone = "(212) 555-5678" },
        new Company { Id = 3, Name = "Starlight Enterprises", City = "Los Angeles", Address = "789 Elm Road", State = "California", Phone = "(213) 555-9876" },
        new Company { Id = 4, Name = "WebMasters Inc.", City = "Austin", Address = "101 Maple Lane", State = "Texas", Phone = "(512) 555-7890" },
        new Company { Id = 5, Name = "FreshFarms Co.", City = "Chicago", Address = "222 Pine Street", State = "Illinois", Phone = "(312) 555-2345" },
    };

    var initialEmployees = new[]
    {
        new Employee { Id = 1, FirstName = "John", LastName = "Doe", Title = "Mr.", Position = "Manager", BirthDate = new DateTime(1997,11,5), Company = initialCompanies[0] },
        new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Title = "Mr.", Position = "Engineer", BirthDate = new DateTime(1993,5,10), Company = initialCompanies[0] },
        new Employee { Id = 3, FirstName = "Michael", LastName = "Johnson", Title = "Mr.", Position = "Analyst", BirthDate = new DateTime(1998,9,6), Company = initialCompanies[1] },
        new Employee { Id = 4, FirstName = "Sarah", LastName = "Williams", Title = "Mrs.", Position = "Designer", BirthDate = new DateTime(1995,7,18), Company = initialCompanies[1] },
        new Employee { Id = 5, FirstName = "David", LastName = "Lee", Title = "Mr.", Position = "Developer", BirthDate = new DateTime(1990,4,1), Company = initialCompanies[2] },
        new Employee { Id = 6, FirstName = "Emily", LastName = "Brown", Title = "Mrs.", Position = "Sales Representative", BirthDate = new DateTime(1999,5,6), Company = initialCompanies[2] },
        new Employee { Id = 7, FirstName = "Daniel", LastName = "Davis", Title = "Mr.", Position = "Project Manager", BirthDate = new DateTime(1994,1,16), Company = initialCompanies[3] },
        new Employee { Id = 8, FirstName = "Olivia", LastName = "Wilson", Title = "Mrs.", Position = "Accountant", BirthDate = new DateTime(1991,10,30), Company = initialCompanies[3] },
        new Employee { Id = 9, FirstName = "Ethan", LastName = "Taylor", Title = "Mr.", Position = "HR Specialist", BirthDate = new DateTime(1991,7,21), Company = initialCompanies[4] },
        new Employee { Id = 10, FirstName = "Ava", LastName = "Martinez", Title = "Mrs.", Position = "Marketing Coordinator", BirthDate = new DateTime(1999,11,21), Company = initialCompanies[4] },
    };

    var initialHistories = new[]
    {
        new History { Id = 1, OrderDate = DateTime.Now, StoreCity = "Los Angeles", Company = initialCompanies[0] },
        new History { Id = 2, OrderDate = DateTime.Now, StoreCity = "Los Angeles", Company = initialCompanies[0] },
        new History { Id = 3, OrderDate = DateTime.Now, StoreCity = "Denver", Company = initialCompanies[1] },
        new History { Id = 4, OrderDate = DateTime.Now, StoreCity = "Denver", Company = initialCompanies[1] },
        new History { Id = 5, OrderDate = DateTime.Now, StoreCity = "Seattle", Company = initialCompanies[2] },
        new History { Id = 6, OrderDate = DateTime.Now, StoreCity = "Denver", Company = initialCompanies[2] },
        new History { Id = 7, OrderDate = DateTime.Now, StoreCity = "Seattle", Company = initialCompanies[3] },
        new History { Id = 8, OrderDate = DateTime.Now, StoreCity = "Denver", Company = initialCompanies[3] },
        new History { Id = 9, OrderDate = DateTime.Now, StoreCity = "Seattle", Company = initialCompanies[4] },
        new History { Id = 10, OrderDate = DateTime.Now, StoreCity = "Denver", Company = initialCompanies[4] },
    };

    var initialNotes = new[]
    {
        new Note { Id = 35700, Employee = initialEmployees[0] },
        new Note { Id = 35701, Employee = initialEmployees[1] },
        new Note { Id = 35702, Employee = initialEmployees[2] },
        new Note { Id = 35703, Employee = initialEmployees[3] },
        new Note { Id = 35704, Employee = initialEmployees[4] },
        new Note { Id = 35705, Employee = initialEmployees[5] },
        new Note { Id = 35706, Employee = initialEmployees[6] },
        new Note { Id = 35707, Employee = initialEmployees[7] },
        new Note { Id = 35708, Employee = initialEmployees[8] },
        new Note { Id = 35709, Employee = initialEmployees[9] },
    };

    db.Companies.AddRange(initialCompanies);
    db.Employees.AddRange(initialEmployees);
    db.Histories.AddRange(initialHistories);
    db.Notes.AddRange(initialNotes);
    db.SaveChanges();
}

app.Run();
