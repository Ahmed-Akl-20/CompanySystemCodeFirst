using CompanySystemCodeFirst.Models;

public class Project
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public List<EmployeeProject> EmployeeProjects { get; set; } = new();
}
