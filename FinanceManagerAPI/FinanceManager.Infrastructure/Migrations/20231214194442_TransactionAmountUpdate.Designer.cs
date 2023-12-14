﻿// <auto-generated />
using System;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231214194442_TransactionAmountUpdate")]
    partial class TransactionAmountUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceManager.Infrastructure.Database.Models.AutomaticTransactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MerchantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PotId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PotId");

                    b.ToTable("AutomaticTransactions");
                });

            modelBuilder.Entity("FinanceManager.Infrastructure.Database.Models.HistoricData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountSaved")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AmountSpent")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Month")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("MonthlyIncome")
                        .HasColumnType("numeric");

                    b.Property<string>("PotName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HistoricData");
                });

            modelBuilder.Entity("FinanceManager.Infrastructure.Database.Models.Pots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSavingsPot")
                        .HasColumnType("boolean");

                    b.Property<decimal>("PotAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PotAmountLeft")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PotAmountSpent")
                        .HasColumnType("numeric");

                    b.Property<string>("PotName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pots");
                });

            modelBuilder.Entity("FinanceManagerAPI.Database.Models.Config", b =>
                {
                    b.Property<int>("ConfigName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ConfigName"));

                    b.Property<string>("HFPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HFUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MonzoAccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MonzoAccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MonzoClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MonzoClientSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MonzoRefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ConfigName");

                    b.ToTable("Config");
                });

            modelBuilder.Entity("FinanceManagerAPI.Database.Models.Transactions", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<string>("MerchantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PotId")
                        .HasColumnType("integer");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<long>("TransactionAmount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PotId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FinanceManager.Infrastructure.Database.Models.AutomaticTransactions", b =>
                {
                    b.HasOne("FinanceManager.Infrastructure.Database.Models.Pots", "Pot")
                        .WithMany()
                        .HasForeignKey("PotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pot");
                });

            modelBuilder.Entity("FinanceManagerAPI.Database.Models.Transactions", b =>
                {
                    b.HasOne("FinanceManager.Infrastructure.Database.Models.Pots", "Pot")
                        .WithMany()
                        .HasForeignKey("PotId");

                    b.Navigation("Pot");
                });
#pragma warning restore 612, 618
        }
    }
}
