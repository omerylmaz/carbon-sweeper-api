using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Houses_HouseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "GeneralConsumptionUser");

            migrationBuilder.DropTable(
                name: "TransportUser");

            migrationBuilder.DropIndex(
                name: "IX_Users_HouseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GeneralConsumptions");

            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "Transports",
                newName: "PublicTransportFootPrint");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "GeneralConsumptions",
                newName: "PaperProductFootPrint");

            migrationBuilder.AddColumn<int>(
                name: "GeneralConsumptionId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CarFootPrint",
                table: "Transports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Houses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DressingFootPrint",
                table: "GeneralConsumptions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ElectronicsFootPrint",
                table: "GeneralConsumptions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FoodFootPrint",
                table: "GeneralConsumptions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FunFootPrint",
                table: "GeneralConsumptions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GeneralConsumptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transports_UserId",
                table: "Transports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Users_UserId",
                table: "Transports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Users_UserId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_UserId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "GeneralConsumptionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarFootPrint",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "DressingFootPrint",
                table: "GeneralConsumptions");

            migrationBuilder.DropColumn(
                name: "ElectronicsFootPrint",
                table: "GeneralConsumptions");

            migrationBuilder.DropColumn(
                name: "FoodFootPrint",
                table: "GeneralConsumptions");

            migrationBuilder.DropColumn(
                name: "FunFootPrint",
                table: "GeneralConsumptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GeneralConsumptions");

            migrationBuilder.RenameColumn(
                name: "PublicTransportFootPrint",
                table: "Transports",
                newName: "Mileage");

            migrationBuilder.RenameColumn(
                name: "PaperProductFootPrint",
                table: "GeneralConsumptions",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Transports",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GeneralConsumptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GeneralConsumptionUser",
                columns: table => new
                {
                    GeneralConsumptionsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralConsumptionUser", x => new { x.GeneralConsumptionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GeneralConsumptionUser_GeneralConsumptions_GeneralConsumpti~",
                        column: x => x.GeneralConsumptionsId,
                        principalTable: "GeneralConsumptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralConsumptionUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportUser",
                columns: table => new
                {
                    TransportsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportUser", x => new { x.TransportsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TransportUser_Transports_TransportsId",
                        column: x => x.TransportsId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HouseId",
                table: "Users",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralConsumptionUser_UsersId",
                table: "GeneralConsumptionUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportUser_UsersId",
                table: "TransportUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Houses_HouseId",
                table: "Users",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");
        }
    }
}
