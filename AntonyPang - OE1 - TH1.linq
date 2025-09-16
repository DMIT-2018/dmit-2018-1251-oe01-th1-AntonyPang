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
			&& x.CampusVenue.Location.ToUpper() != "SCHEDULED ROOM"
			&& x.Name.ToUpper() != "BTECH CLUB MEETING")
	.OrderBy(x => x.StartDate)
	.Select(x => new
		{
			StartDate = x.StartDate,
			Location = x.CampusVenue.Location,
			Club = x.Club.ClubName,
			Activity = x.Name
		})
	.Dump();
	
// Question 2
Programs
	.Select(x => new
		{
			School = x.SchoolCode.ToUpper() == "SAMIT" ? "School of Applied Media and IT"
				   : x.SchoolCode.ToUpper() == "SEET" ? "School of Electrical Engineering Technology"
				   : "Unknown",
			Program = x.ProgramName,
			RequiredCourseCount = x.ProgramCourses.Count(x => x.Required),
			OptionalCourseCount = x.ProgramCourses.Count(x => !x.Required)
		})
	.Where(x => x.RequiredCourseCount >= 22)
	.OrderBy(x => x.Program)
	.Dump();
	
// Question 3
Students
	.Where(x => x.Countries.CountryName.ToUpper() != "CANADA" && x.StudentPayments.Count == 0)
	.OrderBy(x => x.LastName)
	.Select(x => new
		{
			StudentNumber = x.StudentNumber,
			CountryName = x.Countries.CountryName,
			FullName = x.FirstName + " " + x.LastName,
			ClubMembershipCount = x.ClubMembers.Count == 0 ? "None" : x.ClubMembers.Count().ToString()
		})
	.Dump();