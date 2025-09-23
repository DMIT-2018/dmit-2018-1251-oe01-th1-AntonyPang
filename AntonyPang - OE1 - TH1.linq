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
	.Select(x => new
		{
			StartDate = x.StartDate,
			Location = x.CampusVenue.Location,
			Club = x.Club.ClubName,
			Activity = x.Name
		})
	.OrderBy(x => x.StartDate)
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
	
// Question 4
Employees
	.Where(x => x.Position.Description.ToUpper() == "INSTRUCTOR" && x.ClassOfferings.Count() >= 1 && x.ReleaseDate.Value == null)
	.OrderByDescending(x => x.ClassOfferings.Count())
	.ThenBy(x => x.LastName)
	.Select(x => new
		{
			ProgramName = x.Program.ProgramName,
			FullName = x.FirstName + " " + x.LastName,
			WorkLoad = x.ClassOfferings.Count() > 24 ? "High"
					 : x.ClassOfferings.Count() > 8 ? "Med"
					 : "Low"
		})
	.Dump();
	
// Question 5
Clubs
	.Select(x => new
		{
			Supervisor = x.Employee == null ? "Unknown" : x.Employee.FirstName + " " + x.Employee.LastName,
			ClubName = x.ClubName,
			MemberCount = x.ClubMembers.Count(),
			Activities = x.ClubActivities.Count() == 0 ? "None Scheduled" : x.ClubActivities.Count().ToString()
		})
		.OrderByDescending(x => x.MemberCount)
		.Dump();