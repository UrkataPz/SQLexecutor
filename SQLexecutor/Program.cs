var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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


app.UseHttpsRedirection();


//ruta para el query
app.MapControllerRoute(
   name: "default",
    pattern: "{controller=database}/{action=ExecuteQuery}/{id?}");


// Mapa la ruta predeterminada
//app.MapControllerRoute(
  //  name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");


//dejalo para ver despues
// Mapa la ruta para DatabaseController
//app.MapControllerRoute(
  //  name: "database",
    //pattern: "database/{action=ExecuteQuery}/{id?}",
    //defaults: new { controller = "Database" });

app.Run();
