using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CareLink.Data.Migrations
{
    /// <inheritdoc />
    public partial class CleanSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Address", "AiMatchScore", "Category", "CreatedAt", "CreatedByUserId", "Description", "IsAIRecommended", "IsFree", "OpeningHours", "Phone", "State", "Suburb", "Tags", "Title", "Website" },
                values: new object[,]
                {
                    { 1, "456 George St", 0.0, "Food", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(1410), null, "Emergency relief assistance including food parcels, utility bills, furniture, and clothing for people in need.", false, true, "Mon-Fri 9am-5pm", "02 9261 1433", "NSW", "Sydney", "food,emergency,clothing,furniture", "St Vincent de Paul Society", "https://www.vinnies.org.au" },
                    { 2, "Level 2, 323 Castlereagh St", 0.0, "Mental Health", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2440), null, "Free mental health support for young people aged 12-25. Counselling, group therapy, and crisis support.", false, true, "Mon-Fri 9am-6pm", "02 9114 4100", "NSW", "Sydney", "mental health,youth,counselling,crisis,anxiety,depression", "Headspace Mental Health Centre", "https://headspace.org.au" },
                    { 3, "Level 7, 580 George St", 0.0, "Housing", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2440), null, "Affordable and crisis housing solutions for homeless individuals and families.", false, true, "24/7 crisis line", "02 9219 2000", "NSW", "Sydney", "housing,homeless,crisis,accommodation,rent", "Mission Australia Housing", "https://www.missionaustralia.com.au" },
                    { 4, "10 Parker Street", 0.0, "Employment", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2440), null, "Employment assistance, resume support, job search coaching and skills training for disadvantaged job seekers.", false, true, "Mon-Thu 9am-4pm", "02 9891 3333", "NSW", "Parramatta", "employment,jobs,resume,skills,training", "Sydney Community Services", null },
                    { 5, "323 Castlereagh St", 0.0, "Legal", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2450), null, "Free legal advice for people who cannot afford a lawyer. Covers family law, criminal, civil, and immigration.", false, true, "Mon-Fri 9am-5pm", "1300 888 529", "NSW", "Sydney", "legal,law,family,criminal,immigration,advice", "Legal Aid NSW", "https://www.legalaid.nsw.gov.au" },
                    { 6, "Online Service", 0.0, "Health", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2450), null, "AI-powered telehealth platform connecting patients with GPs via video call. Bulk billed for concession card holders.", false, false, "7am-11pm daily", "1300 472 882", "All", "Australia-wide", "health,GP,doctor,telehealth,bulk billing,concession", "GP2U Telehealth", "https://www.gp2u.com.au" },
                    { 7, "140 Elizabeth St", 0.0, "Food", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2450), null, "Crisis food, emergency accommodation, financial counselling, and family support programs.", false, true, "Mon-Fri 8am-8pm", "02 9266 9222", "NSW", "Sydney", "food,emergency,financial,family,crisis", "Salvation Army Emergency Services", "https://www.salvationarmy.org.au" },
                    { 8, "Online / Phone", 0.0, "Mental Health", new DateTime(2026, 6, 3, 7, 15, 51, 712, DateTimeKind.Utc).AddTicks(2450), null, "24/7 mental health and crisis support. AI-powered chat and phone counselling for anxiety and depression.", false, true, "24/7", "1300 22 4636", "All", "Australia-wide", "mental health,crisis,anxiety,depression,suicide,24/7", "Beyond Blue Support Line", "https://www.beyondblue.org.au" }
                });
        }
    }
}
