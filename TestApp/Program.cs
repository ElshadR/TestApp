using TestApp.Attributes;
using TestApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});
// Add services to the container.
builder.Services
               .AddDatabase(builder.Configuration.GetSection("ConnectionStrings:Db").Value)
               .AddUnitOfWork()
               .AddRepositories()
               .AddBusinessServices();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
