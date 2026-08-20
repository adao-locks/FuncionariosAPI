using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Migrations;
using Domain.Entities;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(Data.AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.11");

            modelBuilder.Entity("Domain.Entities.Funcionario", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
                b.Property<string>("Nome").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<string>("Cargo").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<decimal>("Salario").HasColumnType("decimal(18,2)");
                b.Property<string>("Departamento").IsRequired().HasColumnType("nvarchar(max)");
                b.Property<bool>("Ativo").HasColumnType("bit");
                b.HasKey("Id");
                b.ToTable("Funcionarios");
            });
        }
    }
}
