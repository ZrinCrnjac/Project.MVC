using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Service.Database;
using Project.Service.Services;
using Project.MVC.AutoMapper;

var builder = WebApplication.CreateBuilder(args);


// Autofac DI
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<VehicleDatabase>();
        contextOptionsBuilder.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
        container.RegisterType<VehicleDatabase>().WithParameter("options",contextOptionsBuilder.Options).AsSelf().InstancePerLifetimeScope();
        container.RegisterType<VehicleMakeService>().As<IVehicleMakeService>().InstancePerLifetimeScope();
        container.RegisterType<VehicleModelService>().As<IVehicleModelService>().InstancePerLifetimeScope();
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

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