using CompanySystemCodeFirst.Data;
using CompanySystemCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystemCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new CompanyContext();
            string[] menu =
            {
                "1 : Add Department",
                "2 : Add Employee",
                "3 : Add Project",
                "4 : Edit Employee",
                "5 : Edit Department",
                "6 : Edit Project",
                "7 : Delete Department",
                "8 : Delete Employee",
                "9 : Delete Project",
                "10 : Display Departments",
                "11 : Display Employees",
                "12 : Display Projects",
                "13 : Assign/Remove Employee to/from Project",
                "14 : Exit"
            };

            int index = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Select Operation\n");

                for (int i = 0; i < menu.Length; i++)
                {
                    Console.BackgroundColor = (i == index) ? ConsoleColor.DarkBlue : ConsoleColor.Black;
                    Console.ForegroundColor = (i == index) ? ConsoleColor.White : ConsoleColor.Cyan;
                    Console.WriteLine(menu[i]);
                }

                Console.ResetColor();
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.DownArrow)
                    index = (index + 1) % menu.Length;
                else if (key == ConsoleKey.UpArrow)
                    index = (index - 1 + menu.Length) % menu.Length;
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (index)
                    {
                        case 0: // Add Department
                            Console.Write("Enter Department Name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine($"You entered: {name}");

                            var dept = new Department { Name = name };

                            context.Departments.Add(dept);

                            try
                            {
                                context.SaveChanges();
                                Console.WriteLine("Department added.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.InnerException?.Message ?? ex.Message);
                            }


                            break;

                        case 1: // Add Employee
                            Console.Write("First Name: ");
                            var first = Console.ReadLine();
                            Console.Write("Last Name: ");
                            var last = Console.ReadLine();
                            Console.Write("Department ID: ");
                            int deptId = int.Parse(Console.ReadLine());
                            var emp = new Employee { FirstName = first, LastName = last, DepartmentId = deptId };
                            context.Employees.Add(emp);
                            context.SaveChanges();
                            Console.WriteLine("Employee added.");
                            break;

                        case 2: // Add Project
                            Console.Write("Project Name: ");
                            var pname = Console.ReadLine();
                            Console.Write("Start Date (yyyy-mm-dd): ");
                            var s = DateOnly.Parse(Console.ReadLine());
                            Console.Write("End Date (yyyy-mm-dd): ");
                            var e = DateOnly.Parse(Console.ReadLine());
                            var project = new Project { Name = pname, StartDate = s, EndDate = e };
                            context.Projects.Add(project);
                            context.SaveChanges();
                            Console.WriteLine("Project added.");
                            break;

                        case 3: // Edit Employee
                            Console.Write("Enter Employee ID: ");
                            int empId = int.Parse(Console.ReadLine());
                            var existingEmp = context.Employees.Find(empId);
                            if (existingEmp != null)
                            {
                                Console.Write($"First Name ({existingEmp.FirstName}): ");
                                var newF = Console.ReadLine();
                                Console.Write($"Last Name ({existingEmp.LastName}): ");
                                var newL = Console.ReadLine();
                                Console.Write($"Department ID ({existingEmp.DepartmentId}): ");
                                var newDeptId = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(newF)) existingEmp.FirstName = newF;
                                if (!string.IsNullOrWhiteSpace(newL)) existingEmp.LastName = newL;
                                if (int.TryParse(newDeptId, out int newDept)) existingEmp.DepartmentId = newDept;

                                context.SaveChanges();
                                Console.WriteLine("Employee updated.");
                            }
                            break;

                        case 4: // Edit Department
                            Console.Write("Enter Department ID: ");
                            int dId = int.Parse(Console.ReadLine());
                            var existingDept = context.Departments.Find(dId);
                            if (existingDept != null)
                            {
                                Console.Write($"Name ({existingDept.Name}): ");
                                var newN = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newN)) existingDept.Name = newN;
                                context.SaveChanges();
                                Console.WriteLine("Department updated.");
                            }
                            break;

                        case 5: // Edit Project
                            Console.Write("Enter Project ID: ");
                            int pId = int.Parse(Console.ReadLine());
                            var existingProj = context.Projects.Find(pId);
                            if (existingProj != null)
                            {
                                Console.Write($"Name ({existingProj.Name}): ");
                                var newName = Console.ReadLine();
                                Console.Write($"Start Date ({existingProj.StartDate}): ");
                                var newS = Console.ReadLine();
                                Console.Write($"End Date ({existingProj.EndDate}): ");
                                var newE = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(newName)) existingProj.Name = newName;
                                if (DateOnly.TryParse(newS, out DateOnly ns)) existingProj.StartDate = ns;
                                if (DateOnly.TryParse(newE, out DateOnly ne)) existingProj.EndDate = ne;

                                context.SaveChanges();
                                Console.WriteLine("Project updated.");
                            }
                            break;

                        case 6: // Delete Department
                            Console.Write("Enter Department ID to delete: ");
                            int delD = int.Parse(Console.ReadLine());
                            var deptToDel = context.Departments.Find(delD);
                            if (deptToDel != null)
                            {
                                context.Departments.Remove(deptToDel);
                                context.SaveChanges();
                                Console.WriteLine("Department deleted.");
                            }
                            break;

                        case 7: // Delete Employee
                            Console.Write("Enter Employee ID to delete: ");
                            int delE = int.Parse(Console.ReadLine());
                            var empToDel = context.Employees.Find(delE);
                            if (empToDel != null)
                            {
                                context.Employees.Remove(empToDel);
                                context.SaveChanges();
                                Console.WriteLine("Employee deleted.");
                            }
                            break;

                        case 8: // Delete Project
                            Console.Write("Enter Project ID to delete: ");
                            int delP = int.Parse(Console.ReadLine());
                            var projToDel = context.Projects.Find(delP);
                            if (projToDel != null)
                            {
                                context.Projects.Remove(projToDel);
                                context.SaveChanges();
                                Console.WriteLine("Project deleted.");
                            }
                            break;

                        case 9: // Display Departments
                            var deps = context.Departments.Include(d => d.Employees).ToList();
                            foreach (var d in deps)
                            {
                                Console.WriteLine($"Department {d.DepartmentId}: {d.Name}");
                                foreach (var em in d.Employees)
                                    Console.WriteLine($"   - {em.FirstName} {em.LastName}");
                            }
                            break;

                        case 10: // Display Employees
                            var emps = context.Employees
                                .Include(e => e.Department)
                                .Include(e => e.EmployeeProjects)
                                .ThenInclude(ep => ep.Project)
                                .ToList();

                            foreach (var employee in emps)
                            {
                                Console.WriteLine($"Employee {employee.EmployeeId}: {employee.FirstName} {employee.LastName}");
                                Console.WriteLine($"   Department: {employee.Department?.Name}");
                                Console.WriteLine("   Projects:");
                                foreach (var projectLink in employee.EmployeeProjects)
                                    Console.WriteLine($"      - {projectLink.Project.Name} as {projectLink.Role}");
                            }


                            break;

                        case 11: // Display Projects
                            var projs = context.Projects
                                .Include(p => p.EmployeeProjects)
                                .ThenInclude(ep => ep.Employee)
                                .ToList();

                            foreach (var p in projs)
                            {
                                Console.WriteLine($"Project {p.ProjectId}: {p.Name}");
                                Console.WriteLine("   Employees:");
                                foreach (var ep in p.EmployeeProjects)
                                    Console.WriteLine($"      - {ep.Employee.FirstName} {ep.Employee.LastName} as {ep.Role}");
                            }
                            break;

                        case 12: // Assign/Remove employee to/from project
                            Console.Write("Enter Employee ID: ");
                            int eid = int.Parse(Console.ReadLine());
                            Console.Write("Enter Project ID: ");
                            int pid = int.Parse(Console.ReadLine());

                            var link = context.EmployeeProjects.FirstOrDefault(x => x.EmployeeId == eid && x.ProjectId == pid);
                            if (link == null)
                            {
                                Console.Write("Enter Role: ");
                                var role = Console.ReadLine();
                                context.EmployeeProjects.Add(new EmployeeProject { EmployeeId = eid, ProjectId = pid, Role = role });
                                Console.WriteLine("Assigned.");
                            }
                            else
                            {
                                context.EmployeeProjects.Remove(link);
                                Console.WriteLine("Removed.");
                            }
                            context.SaveChanges();
                            break;

                        case 13: // Exit
                            exit = true;
                            Console.WriteLine("Program Ended.");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
