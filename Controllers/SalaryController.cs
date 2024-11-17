using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalaryController : ControllerBase
{
    private readonly SalaryService _salaryService;

    public SalaryController(SalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Salary>> Get()
    {
        var salarys = _salaryService.GetAllSalarys();
        return Ok(salarys);
    }

    [HttpGet("{id}")]
    public ActionResult<Salary> Get(int id)
    {
        var salary = _salaryService.GetSalaryById(id);
        if (salary == null)
        {
            return NotFound();
        }
        return Ok(salary);
    }

    [HttpPost]
    public ActionResult<Salary> Post(Salary salary)
    {
        var createdSalary = _salaryService.AddSalary(salary);
        return CreatedAtAction(nameof(Get), new { id = createdSalary.SalaryId }, createdSalary);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Salary salary)
    {
        if (!_salaryService.UpdateSalary(id, salary))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_salaryService.DeleteSalary(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
