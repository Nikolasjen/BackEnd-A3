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
    public ActionResult<IEnumerable<Salary>> GetAllSalaries()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var salarys = _salaryService.GetAllSalarys();
        return Ok(salarys);
    }

    [HttpGet("{id}")]
    public ActionResult<Salary> GetSalaryById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var salary = _salaryService.GetSalaryById(id);
        if (salary == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(salary);
    }

    [HttpPost]
    public ActionResult<Salary> CreateSalary(Salary salary)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Salary: {@Salary}", "POST", userName, salary);
        var createdSalary = _salaryService.AddSalary(salary);
        return CreatedAtAction(nameof(GetAllSalaries), new { id = createdSalary.SalaryId }, createdSalary);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSalaryById(int id, Salary salary)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Salary: {@Salary}", "PUT", id, userName, salary);
        if (!_salaryService.UpdateSalary(id, salary))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSalaryById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_salaryService.DeleteSalary(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
