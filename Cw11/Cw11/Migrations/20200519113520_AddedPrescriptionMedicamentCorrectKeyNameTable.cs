using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw11.Migrations
{
    public partial class AddedPrescriptionMedicamentCorrectKeyNameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament");

            migrationBuilder.AddPrimaryKey(
                name: "Prescription_Medicament_PK",
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Prescription_Medicament_PK",
                table: "Prescription_Medicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription_Medicament",
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription" });
        }
    }
}
