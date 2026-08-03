using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Controllers
builder.Services.AddControllers();

// 2. Register In-Memory DbContext into Dependency Injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

// 3. Configure HTTP Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

// 4. Map Controller endpoints
app.MapControllers();

app.Run();