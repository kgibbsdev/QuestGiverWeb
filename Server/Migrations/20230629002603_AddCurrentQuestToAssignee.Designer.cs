﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuestGiver.Server.Data;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    [DbContext(typeof(QuestGiverDbContext))]
    [Migration("20230629002603_AddCurrentQuestToAssignee")]
    partial class AddCurrentQuestToAssignee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuestGiver.Shared.Models.Assignee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CurrentQuestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentQuestId");

                    b.ToTable("Assignees", (string)null);
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("RefreshTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Quests", (string)null);
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.Assignee", b =>
                {
                    b.HasOne("QuestGiver.Shared.Models.Quest", "CurrentQuest")
                        .WithMany()
                        .HasForeignKey("CurrentQuestId");

                    b.Navigation("CurrentQuest");
                });
#pragma warning restore 612, 618
        }
    }
}
