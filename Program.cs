using Api.Context;
using Api.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("BasicPolicy",
        policy =>
        {
            policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


builder.Services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo Api",
        Description = "Making todos fun again",
        Version = "v1"
    });
});

// Setup db context
var connectionString = builder.Configuration.GetConnectionString("Todo") ?? "Data Source=Pizzas.db";
builder.Services.AddSqlite<TodoDb>(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
