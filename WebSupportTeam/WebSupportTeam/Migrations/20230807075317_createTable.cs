using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebSupportTeam.Migrations
{
    /// <inheritdoc />
    public partial class createTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "support");

            migrationBuilder.CreateTable(
                name: "file_path",
                schema: "support",
                columns: table => new
                {
                    file_path_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    path_file = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_path", x => x.file_path_id);
                });

            migrationBuilder.CreateTable(
                name: "image",
                schema: "support",
                columns: table => new
                {
                    file_path_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    path_file = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.file_path_id);
                });

            migrationBuilder.CreateTable(
                name: "station_master",
                schema: "support",
                columns: table => new
                {
                    id_station = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pbl_station = table.Column<string>(type: "text", nullable: true),
                    bu_station = table.Column<string>(type: "text", nullable: true),
                    name_station = table.Column<string>(type: "text", nullable: true),
                    ip_bo = table.Column<string>(type: "text", nullable: true),
                    ip_sim = table.Column<string>(type: "text", nullable: true),
                    brand_station = table.Column<string>(type: "text", nullable: true),
                    pump_master = table.Column<string>(type: "text", nullable: true),
                    service_version = table.Column<string>(type: "text", nullable: true),
                    atg_alarm = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_station_master", x => x.id_station);
                });

            migrationBuilder.CreateTable(
                name: "user_manager",
                schema: "support",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true),
                    department = table.Column<string>(type: "text", nullable: true),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_manager", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_path",
                schema: "support");

            migrationBuilder.DropTable(
                name: "image",
                schema: "support");

            migrationBuilder.DropTable(
                name: "station_master",
                schema: "support");

            migrationBuilder.DropTable(
                name: "user_manager",
                schema: "support");
        }
    }
}
