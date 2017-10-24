using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EmbeddedStock2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "EsImage",
                columns: table => new
                {
                    ESImageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageMimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsImage", x => x.ESImageId);
                });

            migrationBuilder.CreateTable(
                name: "ComponentType",
                columns: table => new
                {
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datasheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageESImageId = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WikiLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentType", x => x.ComponentTypeId);
                    table.ForeignKey(
                        name: "FK_ComponentType_EsImage_ImageESImageId",
                        column: x => x.ImageESImageId,
                        principalTable: "EsImage",
                        principalColumn: "ESImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    ComponentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentNumber = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CurrentLoanInformationId = table.Column<long>(type: "bigint", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Component_ComponentType_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentType",
                        principalColumn: "ComponentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypeCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypeCategory", x => new { x.CategoryId, x.ComponentTypeId });
                    table.ForeignKey(
                        name: "FK_ComponentTypeCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentTypeCategory_ComponentType_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentType",
                        principalColumn: "ComponentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Component_ComponentTypeId",
                table: "Component",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentType_ImageESImageId",
                table: "ComponentType",
                column: "ImageESImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentTypeCategory_ComponentTypeId",
                table: "ComponentTypeCategory",
                column: "ComponentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "ComponentTypeCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ComponentType");

            migrationBuilder.DropTable(
                name: "EsImage");
        }
    }
}
