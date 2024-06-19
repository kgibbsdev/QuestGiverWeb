﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuestGiver.Server.Data;

#nullable disable

namespace QuestGiver.Server.Migrations
{
    [DbContext(typeof(QuestGiverDbContext))]
    partial class QuestGiverDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int?>("QuestLogId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "questLogId");

                    b.Property<int>("QuestsCompleted")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "questsCompleted");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "totalExperience");

                    b.HasKey("Id");

                    b.HasIndex("QuestLogId");

                    b.ToTable("Assignees", (string)null);
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "completedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<int>("ExperienceForCompletion")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "experienceForCompletion");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isAssigned");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isCompleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int>("Priority")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "priority");

                    b.Property<int?>("QuestLogId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "questLogId");

                    b.Property<int>("RefreshTimeInDays")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "refreshTimeInDays");

                    b.Property<int>("TimesCompleted")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "timesCompleted");

                    b.HasKey("Id");

                    b.HasIndex("QuestLogId");

                    b.ToTable("Quests", (string)null);
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.QuestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestsCompleted")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuestLogs", (string)null);
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.Assignee", b =>
                {
                    b.HasOne("QuestGiver.Shared.Models.QuestLog", "QuestLog")
                        .WithMany()
                        .HasForeignKey("QuestLogId");

                    b.Navigation("QuestLog");
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.Quest", b =>
                {
                    b.HasOne("QuestGiver.Shared.Models.QuestLog", null)
                        .WithMany("Quests")
                        .HasForeignKey("QuestLogId");
                });

            modelBuilder.Entity("QuestGiver.Shared.Models.QuestLog", b =>
                {
                    b.Navigation("Quests");
                });
#pragma warning restore 612, 618
        }
    }
}
