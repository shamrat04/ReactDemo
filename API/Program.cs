using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{

opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();


using var scope =app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    //Ran this code because sqllite wasn't creating efmigration history. Had to research. still wasn't able to see but saw it appeared later
    // context.Database.ExecuteSqlRaw(@"
      //  CREATE TABLE IF NOT EXISTS ""__EFMigrationsHistory"" (
        //    ""MigrationId"" TEXT NOT NULL CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY,
          //  ""ProductVersion"" TEXT NOT NULL
        //);");
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    
   var logger = services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex, "An error occoured during migration");
}

app.Run();
