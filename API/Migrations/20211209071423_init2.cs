using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_NIK",
                table: "tb_t_profilling");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_t_profilling",
                newName: "Nik");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_m_employee",
                newName: "Nik");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_m_account",
                newName: "Nik");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_employee_Nik",
                table: "tb_m_account",
                column: "Nik",
                principalTable: "tb_m_employee",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_Nik",
                table: "tb_t_profilling",
                column: "Nik",
                principalTable: "tb_m_account",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_employee_Nik",
                table: "tb_m_account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_Nik",
                table: "tb_t_profilling");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_t_profilling",
                newName: "NIK");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_m_employee",
                newName: "NIK");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_m_account",
                newName: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profilling_tb_m_account_NIK",
                table: "tb_t_profilling",
                column: "NIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
