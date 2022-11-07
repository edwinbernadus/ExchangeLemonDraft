﻿// <auto-generated />
using System;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExchangeLemonCore.Migrations.LoggingExt
{
    [DbContext(typeof(LoggingExtContext))]
    partial class LoggingExtContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlueLight.Main.LogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddtionalContent");

                    b.Property<string>("CallerName");

                    b.Property<string>("ClassName");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("Duration");

                    b.Property<string>("ModuleName");

                    b.Property<string>("SessionId");

                    b.Property<DateTime?>("StartBatchDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("LogItems");
                });

            modelBuilder.Entity("BlueLight.Main.LogItemEventSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("DelayTime");

                    b.Property<string>("ExceptionMessage");

                    b.Property<bool>("IsDelay");

                    b.Property<bool>("IsRequest");

                    b.Property<string>("MethodName");

                    b.Property<Guid>("SessionId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("LogItemEventSources");
                });

            modelBuilder.Entity("BlueLight.Main.LogSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ErrorMessage");

                    b.Property<Guid>("GuidSession");

                    b.Property<bool>("IsError");

                    b.Property<bool>("IsStart");

                    b.Property<string>("StackTrace");

                    b.HasKey("Id");

                    b.ToTable("LogSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
