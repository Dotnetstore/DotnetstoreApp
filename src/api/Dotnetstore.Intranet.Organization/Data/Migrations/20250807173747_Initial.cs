using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnetstore.Intranet.Organization.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicationuser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsGdpr = table.Column<bool>(type: "boolean", nullable: false),
                    LastName = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsMale = table.Column<bool>(type: "boolean", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "character varying(13)", unicode: false, maxLength: 13, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EmailAddressIsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    EmailAddressConfirmationCode = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    AccountIsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationuser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "applicationuserrole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsGdpr = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationuserrole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "applicationuserinrole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsGdpr = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicationuserinrole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applicationuserinrole_applicationuser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "applicationuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_applicationuserinrole_applicationuserrole_ApplicationUserRo~",
                        column: x => x.ApplicationUserRoleId,
                        principalTable: "applicationuserrole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applicationuser_EmailAddress",
                table: "applicationuser",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationuser_Id",
                table: "applicationuser",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationuserinrole_ApplicationUserId",
                table: "applicationuserinrole",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_applicationuserinrole_ApplicationUserRoleId",
                table: "applicationuserinrole",
                column: "ApplicationUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_applicationuserinrole_Id",
                table: "applicationuserinrole",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationuserrole_Id",
                table: "applicationuserrole",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicationuserinrole");

            migrationBuilder.DropTable(
                name: "applicationuser");

            migrationBuilder.DropTable(
                name: "applicationuserrole");
        }
    }
}
