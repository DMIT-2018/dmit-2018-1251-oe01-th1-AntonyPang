<Query Kind="Statements">
  <Connection>
    <ID>bec619b4-a710-4125-ba4e-f6b755193f37</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>EVO\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

// Question 1
ClubActivities
	.Where(x => x.StartDate.Value >= new DateTime(2025, 1, 1)
			&& x.CampusVenue.Location != "Scheduled Room"
			&& x.Name != "BTech Club Meeting")
	.OrderBy(x => x.StartDate)
	.Select(x => new
		{
			x.StartDate,
			Location = x.CampusVenue.Location,
			Club = x.Club.ClubName,
			Activity = x.Name
		})
	.Dump();