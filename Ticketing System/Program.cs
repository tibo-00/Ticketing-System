using Microsoft.EntityFrameworkCore;
using Ticketing_System.Data;
using Ticketing_System.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IConcertService, ConcertService>();
builder.Services.AddScoped<IConcertHallService, ConcertHallService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();


var app = builder.Build();

//Apply pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


public partial class Program {}