using API.Models.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for rules
/// </summary>
/// <param name="ruleRepository"></param>
[Route("[controller]/")]
public class RulesController(IRuleRepository ruleRepository)
    : ControllerBase
{
    /// <summary>
    /// Retrieves a rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the rule to retrieve.</param>
    /// <returns>A rule object if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RuleDto>> Get(int id)
    {
        var rule = await ruleRepository.Get(id);
        if (rule == null)
        {
            return NotFound();
        }

        return (RuleDto)rule;
    }

    /// <summary>
    /// Retrieves all rules.
    /// </summary>
    /// <returns>A list of all rules.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<RuleDto>> GetAll()
    {
        return (await ruleRepository.GetAll()).Select(rule => (RuleDto)rule);
    }

    /// <summary>
    /// Adds a new rule.
    /// </summary>
    /// <param name="rule">The rule to add.</param>
    /// <returns>A 201 Created response if the rule is successfully added; otherwise, a 400 Bad Request response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(RuleDto rule)
    {
        await ruleRepository.Create(new Rule
        {
            English = rule.English,
            Danish = rule.Danish
        });
        return Created();
    }

    /// <summary>
    /// Deletes a rule by its ID.
    /// </summary>
    /// <param name="id">The ID of the rule to delete.</param>
    /// <returns>A 200 OK response if the rule is successfully deleted.</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(int id)
    {
        await ruleRepository.Delete(id);
        return Ok();
    }
}