using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sdao.AppModel.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence(
                name: "t_container_seq",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false, defaultValueSql: "nextval('shared.\"t_container_seq\"')"),
                    categoryId = table.Column<long>(type: "int8", nullable: false),
                    containerName = table.Column<string>(type: "text", nullable: false),
                    createtime = table.Column<string>(type: "text", nullable: true, defaultValue: "2017-08-20 20:46:19"),
                    createuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<int>(type: "int4", nullable: false),
                    json = table.Column<string>(type: "text", nullable: true),
                    updatetime = table.Column<string>(type: "text", nullable: true, defaultValue: "2017-08-20 20:46:19"),
                    updateuserid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropSequence(
                name: "t_container_seq",
                schema: "shared");
        }
    }
}
