using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalaryController : ControllerBase
{
    private readonly SalaryService _salaryService;
    private readonly ILogger<SalaryController> _logger;

    public SalaryController(SalaryService salaryService, ILogger<SalaryController> logger)
    {
        _salaryService = salaryService;
        _logger = logger;
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
        _logger.LogInformation("Salary called Post (POST) with Salary:{@Salary} ", salary);
        var createdSalary = _salaryService.AddSalary(salary);
        return CreatedAtAction(nameof(Get), new { id = createdSalary.SalaryId }, createdSalary);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Salary salary)
    {
        _logger.LogInformation("Salary called Put (PUT) with ID:{Id} and Salary:{@Salary} ", id, salary);
        if (!_salaryService.UpdateSalary(id, salary))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Salary called Delete (DELETE) with ID:{Id} ", id);
        if (!_salaryService.DeleteSalary(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
