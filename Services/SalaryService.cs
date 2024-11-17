using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class SalaryService
{
    private readonly FoodAppG4Context _context;

    public SalaryService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Salary> GetAllSalarys()
    {
        return _context.Salaries.ToList();
    }

    public Salary? GetSalaryById(int id)
    {
        return _context.Salaries.Find(id);
    }

    public Salary AddSalary(Salary salary)
    {
        _context.Salaries.Add(salary);
        _context.SaveChanges();
        return salary;
    }

    public bool UpdateSalary(int id, Salary salary)
    {
        if (id != salary.SalaryId)
        {
            return false;
        }

        _context.Entry(salary).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Salaries.Any(e => e.SalaryId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteSalary(int id)
    {
        var salary = _context.Salaries.Find(id);
        if (salary == null)
        {
            return false;
        }

        _context.Salaries.Remove(salary);
        _context.SaveChanges();
        return true;
    }
}
