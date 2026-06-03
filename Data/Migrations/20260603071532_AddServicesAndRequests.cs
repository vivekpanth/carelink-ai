using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CareLink.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddServicesAndRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    NeedsDescription = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Suburb = table.Column<string>(type: "TEXT", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Suburb = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    OpeningHours = table.Column<string>(type: "TEXT", nullable: true),
                    IsAIRecommended = table.Column<bool>(type: "INTEGER", nullable: false),
                    AiMatchScore = table.Column<double>(type: "REAL", nullable: false),
                    IsFree = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "TEXT", nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Address", "AiMatchScore", "Category", "CreatedAt", "CreatedByUserId", "Description", "IsAIRecommended", "IsFree", "OpeningHours", "Phone", "State", "Suburb", "Tags", "Title", "Website" },
                values: new object[,]
                {
                    { 1, "456 George St", 0.0, "Food", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(7420), null, "Emergency relief assistance including food parcels, utility bills, furniture, and clothing for people in need.", false, true, "Mon-Fri 9am-5pm", "02 9261 1433", "NSW", "Sydney", "food,emergency,clothing,furniture", "St Vincent de Paul Society", "https://www.vinnies.org.au" },
                    { 2, "Level 2, 323 Castlereagh St", 0.0, "Mental Health", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8410), null, "Free mental health support for young people aged 12-25. Counselling, group therapy, and crisis support.", false, true, "Mon-Fri 9am-6pm", "02 9114 4100", "NSW", "Sydney", "mental health,youth,counselling,crisis,anxiety,depression", "Headspace Mental Health Centre", "https://headspace.org.au" },
                    { 3, "Level 7, 580 George St", 0.0, "Housing", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8410), null, "Affordable and crisis housing solutions for homeless individuals and families.", false, true, "24/7 crisis line", "02 9219 2000", "NSW", "Sydney", "housing,homeless,crisis,accommodation,rent", "Mission Australia Housing", "https://www.missionaustralia.com.au" },
                    { 4, "10 Parker Street", 0.0, "Employment", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8410), null, "Employment assistance, resume support, job search coaching and skills training for disadvantaged job seekers.", false, true, "Mon-Thu 9am-4pm", "02 9891 3333", "NSW", "Parramatta", "employment,jobs,resume,skills,training", "Sydney Community Services", null },
                    { 5, "323 Castlereagh St", 0.0, "Legal", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8420), null, "Free legal advice for people who cannot afford a lawyer. Covers family law, criminal, civil, and immigration.", false, true, "Mon-Fri 9am-5pm", "1300 888 529", "NSW", "Sydney", "legal,law,family,criminal,immigration,advice", "Legal Aid NSW", "https://www.legalaid.nsw.gov.au" },
                    { 6, "Online Service", 0.0, "Health", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8420), null, "AI-powered telehealth platform connecting patients with GPs via video call. Bulk billed for concession card holders.", false, false, "7am-11pm daily", "1300 472 882", "All", "Australia-wide", "health,GP,doctor,telehealth,bulk billing,concession", "GP2U Telehealth", "https://www.gp2u.com.au" },
                    { 7, "140 Elizabeth St", 0.0, "Food", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8420), null, "Crisis food, emergency accommodation, financial counselling, and family support programs.", false, true, "Mon-Fri 8am-8pm", "02 9266 9222", "NSW", "Sydney", "food,emergency,financial,family,crisis", "Salvation Army Emergency Services", "https://www.salvationarmy.org.au" },
                    { 8, "Online / Phone", 0.0, "Mental Health", new DateTime(2026, 6, 3, 7, 15, 32, 244, DateTimeKind.Utc).AddTicks(8420), null, "24/7 mental health and crisis support. AI-powered chat and phone counselling for anxiety and depression.", false, true, "24/7", "1300 22 4636", "All", "Australia-wide", "mental health,crisis,anxiety,depression,suicide,24/7", "Beyond Blue Support Line", "https://www.beyondblue.org.au" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
