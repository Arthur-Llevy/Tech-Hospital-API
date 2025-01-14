using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Doctors_Available_Days_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsDaysAvailableEntity_Doctors_Doctor_Id",
                table: "DoctorsDaysAvailableEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorsDaysAvailableEntity",
                table: "DoctorsDaysAvailableEntity");

            migrationBuilder.RenameTable(
                name: "DoctorsDaysAvailableEntity",
                newName: "Doctors_Avaiable_Days");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorsDaysAvailableEntity_Doctor_Id",
                table: "Doctors_Avaiable_Days",
                newName: "IX_Doctors_Avaiable_Days_Doctor_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors_Avaiable_Days",
                table: "Doctors_Avaiable_Days",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Avaiable_Days_Doctors_Doctor_Id",
                table: "Doctors_Avaiable_Days",
                column: "Doctor_Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Avaiable_Days_Doctors_Doctor_Id",
                table: "Doctors_Avaiable_Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors_Avaiable_Days",
                table: "Doctors_Avaiable_Days");

            migrationBuilder.RenameTable(
                name: "Doctors_Avaiable_Days",
                newName: "DoctorsDaysAvailableEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_Avaiable_Days_Doctor_Id",
                table: "DoctorsDaysAvailableEntity",
                newName: "IX_DoctorsDaysAvailableEntity_Doctor_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorsDaysAvailableEntity",
                table: "DoctorsDaysAvailableEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsDaysAvailableEntity_Doctors_Doctor_Id",
                table: "DoctorsDaysAvailableEntity",
                column: "Doctor_Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
