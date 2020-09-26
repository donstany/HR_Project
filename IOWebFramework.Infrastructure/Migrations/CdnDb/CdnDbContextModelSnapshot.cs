﻿// <auto-generated />
using System;
using IOWebFramework.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations.CdnDb
{
    [DbContext(typeof(CdnDbContext))]
    partial class CdnDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CDN.Core3.Data.Data.CdnFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ContentId")
                        .HasColumnName("content_id")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DateUploaded")
                        .HasColumnName("date_uploaded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileDescription")
                        .HasColumnName("file_description")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnName("file_name")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("FileSize")
                        .HasColumnName("file_size")
                        .HasColumnType("integer");

                    b.Property<string>("FileTitle")
                        .HasColumnName("file_title")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnName("source_id")
                        .HasColumnType("text");

                    b.Property<int>("SourceType")
                        .HasColumnName("source_type")
                        .HasColumnType("integer");

                    b.Property<string>("TenantId")
                        .HasColumnName("tenant_id")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UserUploaded")
                        .HasColumnName("user_uploaded")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("cdn_files");
                });

            modelBuilder.Entity("CDN.Core3.Data.Data.CdnFileContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CdnFileId")
                        .HasColumnName("cdn_file_id")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("CdnFileId");

                    b.ToTable("cdn_file_contents");
                });

            modelBuilder.Entity("CDN.Core3.Data.Data.CdnFileContent", b =>
                {
                    b.HasOne("CDN.Core3.Data.Data.CdnFile", "File")
                        .WithMany("FileContents")
                        .HasForeignKey("CdnFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}