using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class EmployeeSummary
{
    public List<string> Names { get; set; } = new List<string>();
    public decimal TotalSalary { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal MinimumSalary { get; set; }
    public decimal MaximumSalary { get; set; }
    public int EmployeeCount { get; set; }
}

public static class EmployeeFilter
{
    public static string GetFilteredEmployeeData(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
            return "{}";

        var selectedEmployees = new List<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)>();

        foreach (var emp in employees)
        {
            bool ageCondition = emp.Age >= 25 && emp.Age <= 40;
            bool deptCondition = emp.Department == "IT" || emp.Department == "Finance";
            bool salaryCondition = emp.Salary >= 5000 && emp.Salary <= 9000;
            bool hireDateCondition = emp.HireDate.Year > 2017;

            if (ageCondition && deptCondition && salaryCondition && hireDateCondition)
            {
                selectedEmployees.Add(emp);
            }
        }

        var orderedNames = selectedEmployees
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToList();

        var summary = new EmployeeSummary
        {
            Names = orderedNames,
            EmployeeCount = selectedEmployees.Count,
            TotalSalary = selectedEmployees.Sum(e => e.Salary),
            AverageSalary = selectedEmployees.Count > 0 ? Math.Round(selectedEmployees.Average(e => e.Salary), 2) : 0,
            MinimumSalary = selectedEmployees.Count > 0 ? selectedEmployees.Min(e => e.Salary) : 0,
            MaximumSalary = selectedEmployees.Count > 0 ? selectedEmployees.Max(e => e.Salary) : 0
        };

        return JsonSerializer.Serialize(summary);
    }
}
