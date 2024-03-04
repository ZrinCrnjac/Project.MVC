using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Project.Service.Database;

#nullable disable

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<VehicleDatabase>(options => options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=VehicleDatabase;Persist Security Info=True;Integrated Security=True"))
                .AddScoped<VehicleSeeder>()
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<VehicleSeeder>();
                seeder.Seed();
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
