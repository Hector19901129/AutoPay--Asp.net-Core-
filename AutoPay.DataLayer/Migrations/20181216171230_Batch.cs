using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoPay.DataLayer.Migrations
{
    public partial class Batch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_AspNetUsers = table.Column<string>(maxLength: 40, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    SqlQuery = table.Column<string>(maxLength: 4000, nullable: false),
                    CustomersCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemoteDbConfigs",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 40, nullable: false),
                    Server = table.Column<string>(maxLength: 256, nullable: false),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false),
                    Database = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteDbConfigs", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BatchCustomers",
                columns: table => new
                {
                    FK_Batches = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(maxLength: 256, nullable: false),
                    CustomerName = table.Column<string>(maxLength: 512, nullable: false),
                    AmountDue = table.Column<string>(maxLength: 256, nullable: false),
                    IsExistsInLocalDb = table.Column<string>(maxLength: 256, nullable: false),
                    PaymentStatus = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCustomers", x => new { x.FK_Batches, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_BatchCustomers_Batches_FK_Batches",
                        column: x => x.FK_Batches,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchCustomers");

            migrationBuilder.DropTable(
                name: "RemoteDbConfigs");

            migrationBuilder.DropTable(
                name: "Batches");
        }
    }
}
