﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Sdao.AppModel.Data;
using System;

namespace Sdao.AppModel.API.Migrations
{
    [DbContext(typeof(AppModelContext))]
    [Migration("20170820124619_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("Relational:Sequence:shared.t_container_seq", "'t_container_seq', 'shared', '1000', '1', '', '', 'Int64', 'False'");

            modelBuilder.Entity("Sdao.AppModel.Model.Entities.Container", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("nextval('shared.\"t_container_seq\"')");

                    b.Property<long>("categoryId");

                    b.Property<string>("containerName")
                        .IsRequired();

                    b.Property<string>("createtime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("2017-08-20 20:46:19");

                    b.Property<string>("createuserid");

                    b.Property<int>("isdelete");

                    b.Property<string>("json");

                    b.Property<string>("updatetime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("2017-08-20 20:46:19");

                    b.Property<string>("updateuserid");

                    b.HasKey("id");

                    b.ToTable("Containers");
                });
#pragma warning restore 612, 618
        }
    }
}
