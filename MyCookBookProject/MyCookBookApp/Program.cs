using MyCookBookApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<RecipeService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "youtube",
    pattern: "{controller=YouTube}/{action=Player}/{id?}");

app.MapControllerRoute(
    name: "recipe",
    pattern: "{controller=Recipe}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();