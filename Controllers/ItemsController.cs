using Api.CosmosDB;
using Api.CosmosDB.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    public readonly IItemCosmosService _itemCosmosService;
    public ItemsController(IItemCosmosService itemCosmosService)
    {
        _itemCosmosService = itemCosmosService;
    }

    // public async Task<IActionResult> Get()
    // {
    //     var sqlCosmosQuery = "Select * from c";
    //     var result = await _itemCosmosService.Get(sqlCosmosQuery);
    //     return Ok(result);
    // }

    [HttpGet]
    [HttpGet("{id?}")]
    public async Task<IActionResult> Get(string id)
    {
        // List<Item> result;
        var sqlCosmosQuery = "Select * from c";

        if (!string.IsNullOrWhiteSpace(id))
        {
            sqlCosmosQuery += $" where 'id' = '{id}'";
        }

        var result = await _itemCosmosService.Get(sqlCosmosQuery);
        return Ok(result);
    }
}