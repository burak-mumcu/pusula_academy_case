using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class Employee
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class Summary
{
    public List<string> EmployeeNames { get; set; } = new List<string>();
    public decimal TotalSalary { get; set; }
    public decimal AverageSalary { get; set; }
    public decimal HighestSalary { get; set; }
    public int EmployeeCount { get; set; }
}

public static class EmployeeXmlHandler
{
    public static string ExtractQualifiedEmployees(string xmlContent)
    {
        var document = XDocument.Parse(xmlContent);
        var employeeElements = document.Descendants("Person");

        var qualifiedEmployees = new List<Employee>();

        foreach (var element in employeeElements)
        {
            var name = element.Element("Name")?.Value ?? string.Empty;
            var ageText = element.Element("Age")?.Value ?? "0";
            var department = element.Element("Department")?.Value ?? string.Empty;
            var salaryText = element.Element("Salary")?.Value ?? "0";
            var hireDateText = element.Element("HireDate")?.Value ?? DateTime.MinValue.ToString();

            if (!int.TryParse(ageText, out int age))
                age = 0;

            if (!decimal.TryParse(salaryText, out decimal salary))
                salary = 0;

            if (!DateTime.TryParse(hireDateText, out DateTime hireDate))
                hireDate = DateTime.MinValue;

            if (age > 30 && department == "IT" && salary > 5000 && hireDate.Year < 2019)
            {
                qualifiedEmployees.Add(new Employee
                {
                    Name = name,
                    Age = age,
                    Department = department,
                    Salary = salary,
                    HireDate = hireDate
                });
            }
        }

        var summary = new Summary();

        if (qualifiedEmployees.Count > 0)
        {
            summary.EmployeeNames = qualifiedEmployees.Select(e => e.Name).OrderBy(n => n).ToList();
            summary.TotalSalary = qualifiedEmployees.Sum(e => e.Salary);
            summary.AverageSalary = qualifiedEmployees.Average(e => e.Salary);
            summary.HighestSalary = qualifiedEmployees.Max(e => e.Salary);
            summary.EmployeeCount = qualifiedEmployees.Count;
        }

        return JsonSerializer.Serialize(summary);
    }
}
