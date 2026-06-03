using CareLink.Models;

namespace CareLink.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext db)
    {
        if (db.Services.Any()) return;

        db.Services.AddRange(
            new Service { Title="St Vincent de Paul Society", Description="Emergency relief assistance including food parcels, utility bills, furniture, and clothing for people in need.", Category="Food", Address="456 George St", Suburb="Sydney", State="NSW", Phone="02 9261 1433", Website="https://www.vinnies.org.au", OpeningHours="Mon-Fri 9am-5pm", IsFree=true, Tags="food,emergency,clothing,furniture", CreatedAt=DateTime.UtcNow },
            new Service { Title="Headspace Mental Health Centre", Description="Free mental health support for young people aged 12-25. Counselling, group therapy, and crisis support.", Category="Mental Health", Address="Level 2, 323 Castlereagh St", Suburb="Sydney", State="NSW", Phone="02 9114 4100", Website="https://headspace.org.au", OpeningHours="Mon-Fri 9am-6pm", IsFree=true, Tags="mental health,youth,counselling,crisis,anxiety,depression", CreatedAt=DateTime.UtcNow },
            new Service { Title="Mission Australia Housing", Description="Affordable and crisis housing solutions for homeless individuals and families.", Category="Housing", Address="Level 7, 580 George St", Suburb="Sydney", State="NSW", Phone="02 9219 2000", Website="https://www.missionaustralia.com.au", OpeningHours="24/7 crisis line", IsFree=true, Tags="housing,homeless,crisis,accommodation,rent", CreatedAt=DateTime.UtcNow },
            new Service { Title="Sydney Community Services", Description="Employment assistance, resume support, job search coaching and skills training for disadvantaged job seekers.", Category="Employment", Address="10 Parker Street", Suburb="Parramatta", State="NSW", Phone="02 9891 3333", OpeningHours="Mon-Thu 9am-4pm", IsFree=true, Tags="employment,jobs,resume,skills,training", CreatedAt=DateTime.UtcNow },
            new Service { Title="Legal Aid NSW", Description="Free legal advice for people who cannot afford a lawyer. Covers family law, criminal, civil, and immigration.", Category="Legal", Address="323 Castlereagh St", Suburb="Sydney", State="NSW", Phone="1300 888 529", Website="https://www.legalaid.nsw.gov.au", OpeningHours="Mon-Fri 9am-5pm", IsFree=true, Tags="legal,law,family,criminal,immigration,advice", CreatedAt=DateTime.UtcNow },
            new Service { Title="GP2U Telehealth", Description="AI-powered telehealth platform connecting patients with GPs via video call. Bulk billed for concession card holders.", Category="Health", Address="Online Service", Suburb="Australia-wide", State="All", Phone="1300 472 882", Website="https://www.gp2u.com.au", OpeningHours="7am-11pm daily", IsFree=false, Tags="health,GP,doctor,telehealth,bulk billing,concession", CreatedAt=DateTime.UtcNow },
            new Service { Title="Salvation Army Emergency Services", Description="Crisis food, emergency accommodation, financial counselling, and family support programs.", Category="Food", Address="140 Elizabeth St", Suburb="Sydney", State="NSW", Phone="02 9266 9222", Website="https://www.salvationarmy.org.au", OpeningHours="Mon-Fri 8am-8pm", IsFree=true, Tags="food,emergency,financial,family,crisis", CreatedAt=DateTime.UtcNow },
            new Service { Title="Beyond Blue Support Line", Description="24/7 mental health and crisis support. AI-powered chat and phone counselling for anxiety and depression.", Category="Mental Health", Address="Online / Phone", Suburb="Australia-wide", State="All", Phone="1300 22 4636", Website="https://www.beyondblue.org.au", OpeningHours="24/7", IsFree=true, Tags="mental health,crisis,anxiety,depression,suicide,24/7", CreatedAt=DateTime.UtcNow }
        );
        db.SaveChanges();
    }
}
