using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoPay.DataLayer.Migrations
{
    public partial class BatchCustomerDueDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GetDueDetailQuery",
                table: "RemoteDbConfigs",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdateDueDetailSp",
                table: "RemoteDbConfigs",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BatchCustomerDueDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_BatchCustomers = table.Column<int>(nullable: false),
                    RecType = table.Column<string>(maxLength: 128, nullable: false),
                    TransactionDate = table.Column<string>(maxLength: 128, nullable: true),
                    Reference = table.Column<string>(maxLength: 512, nullable: true),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    AmountDue = table.Column<string>(maxLength: 256, nullable: false),
                    YearMonth = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCustomerDueDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchCustomerDueDetails_BatchCustomers_FK_BatchCustomers",
                        column: x => x.FK_BatchCustomers,
                        principalTable: "BatchCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchCustomerDueDetails_FK_BatchCustomers",
                table: "BatchCustomerDueDetails",
                column: "FK_BatchCustomers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchCustomerDueDetails");

            migrationBuilder.DropColumn(
                name: "GetDueDetailQuery",
                table: "RemoteDbConfigs");

            migrationBuilder.DropColumn(
                name: "UpdateDueDetailSp",
                table: "RemoteDbConfigs");
        }
    }
}
