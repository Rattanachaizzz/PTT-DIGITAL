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
                name: "file_paths",
                schema: "support",
                columns: table => new
                {
                    file_path_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    path_file = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_paths", x => x.file_path_id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "support",
                columns: table => new
                {
                    file_path_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    path_file = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.file_path_id);
                });

            migrationBuilder.CreateTable(
                name: "Station_masters",
                schema: "support",
                columns: table => new
                {
                    id_station = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pbl_station = table.Column<string>(type: "text", nullable: false),
                    bu_station = table.Column<string>(type: "text", nullable: false),
                    name_station = table.Column<string>(type: "text", nullable: false),
                    ip_bo = table.Column<string>(type: "text", nullable: false),
                    ip_sim = table.Column<string>(type: "text", nullable: false),
                    brand_station = table.Column<string>(type: "text", nullable: false),
                    pump_master = table.Column<string>(type: "text", nullable: false),
                    service_version = table.Column<string>(type: "text", nullable: false),
                    atg_alarm = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_masters", x => x.id_station);
                });

            migrationBuilder.CreateTable(
                name: "Users_managers",
                schema: "support",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    department = table.Column<string>(type: "text", nullable: false),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_managers", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_paths",
                schema: "support");

            migrationBuilder.DropTable(
                name: "images",
                schema: "support");

            migrationBuilder.DropTable(
                name: "Station_masters",
                schema: "support");

            migrationBuilder.DropTable(
                name: "Users_managers",
                schema: "support");
        }
    }
}
