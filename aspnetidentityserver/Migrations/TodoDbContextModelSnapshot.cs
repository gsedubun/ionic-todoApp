﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using myAppApi.Models;
using System;

namespace myAppApi.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    partial class TodoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("myAppApi.Models.MyRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Role");

                    b.HasKey("ID");

                    b.ToTable("MyRoles");
                });

            modelBuilder.Entity("myAppApi.Models.MyUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("myAppApi.Models.MyUserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MyRoleID");

                    b.Property<int?>("MyUserID");

                    b.HasKey("ID");

                    b.HasIndex("MyRoleID");

                    b.HasIndex("MyUserID");

                    b.ToTable("MyUserRoles");
                });

            modelBuilder.Entity("myAppApi.Models.TodoItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("myAppApi.Models.MyUserRole", b =>
                {
                    b.HasOne("myAppApi.Models.MyRole", "MyRole")
                        .WithMany()
                        .HasForeignKey("MyRoleID");

                    b.HasOne("myAppApi.Models.MyUser", "MyUser")
                        .WithMany()
                        .HasForeignKey("MyUserID");
                });
#pragma warning restore 612, 618
        }
    }
}