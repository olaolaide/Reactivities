using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
try
{
    var context = service.GetRequiredService<DataContext>();
    context.Database.Migrate();
    await Seed.SeedData(context);
}
catch (Exception e)
{
   var logger = service.GetRequiredService<ILogger<Program>>();
   logger.LogError(e, "An error occurred while migrating the database.");
}


app.Run();