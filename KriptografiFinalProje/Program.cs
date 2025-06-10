var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services
builder.Services.AddScoped<KriptografiFinalProje.Services.SHAmetin>();
builder.Services.AddScoped<KriptografiFinalProje.Services.SHAdosya>();
builder.Services.AddScoped<KriptografiFinalProje.Services.AESsifrele>();
builder.Services.AddScoped<KriptografiFinalProje.Services.AEScoz>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
