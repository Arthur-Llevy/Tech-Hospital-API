using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Added_Date_Column_To_Appointments_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Doctors_Days_Available_Id",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctors_Days_Available_Id",
                table: "Appointments",
                column: "Doctors_Days_Available_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_Avaiable_Days_Doctors_Days_Available_Id",
                table: "Appointments",
                column: "Doctors_Days_Available_Id",
                principalTable: "Doctors_Avaiable_Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_Avaiable_Days_Doctors_Days_Available_Id",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Doctors_Days_Available_Id",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Doctors_Days_Available_Id",
                table: "Appointments");
        }
    }
}
