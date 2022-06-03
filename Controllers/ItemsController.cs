using Api.Context;
using Api.Models.Context;
using Api.Models.Payload;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[EnableCors("BasicPolicy")]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly TodoDb _db;

    public ItemsController(TodoDb todoDb)
    {
        _db = todoDb;
    }

    // CREATE Items

    [HttpPost]
    public async Task<IActionResult> Post(Item item)
    {
        if (item == null) return BadRequest("missing item data to create");

        try
        {
            Item newItem = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(item));
            await _db.Items.AddAsync(newItem);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex) when (ex.InnerException.Message.Contains("SQLite Error 19"))
        {
            return StatusCode(500, "Invalid Status Id");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }

        return Ok("Item created successfully");
    }

    // GET ITEMS

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Item> items = await _db.Items.ToListAsync();

        return Ok(items);
    }


    [HttpGet("{id?}")]
    public async Task<IActionResult> Get(int? id)
    {
        if (id != null && id > 0)
        {
            Item item = await _db.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (item != null) return Ok(item);
            return NotFound("Could not find an Item with that id");
        }

        return BadRequest("Missing id");
    }

    // UPDATE ITEMS

    [HttpPut]
    public async Task<IActionResult> Put(Item item)
    {
        if (item == null) return BadRequest("missing item data to update");

        try
        {
            Item updateItem = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(item));
            _db.Items.Update(updateItem);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex) when (ex.InnerException.Message.Contains("SQLite Error 19"))
        {
            return StatusCode(500, "Invalid Status Id");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }

        return Ok("Item updated successfully");
    }



    // DELETE ITEM(s)

    [HttpDelete]
    public async Task<IActionResult> Delete(DeletePayload request)
    {
        if (request.Ids.Count < 1) return BadRequest("Need at least 1 id to delete items");

        // track success/failure
        var couldNotFindIds = new List<int>();
        var successfulIds = new List<int>();


        foreach (var id in request.Ids)
        {
            var item = await _db.Items.FindAsync(id);
            if (item is null)
            {
                couldNotFindIds.Add(id);
                continue;
            }
            _db.Items.Remove(item);
            successfulIds.Add(id);
        }

        await _db.SaveChangesAsync();

        var payload = new Dictionary<string, List<int>>();
        payload.Add("Successfully Deleted items", successfulIds);
        payload.Add("Unable to find items", couldNotFindIds);
        return Ok(payload);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Items.FindAsync(id);
        if (item is null)
        {
            return NotFound("Could not find item with that id");
        }
        _db.Items.Remove(item);
        await _db.SaveChangesAsync();

        return Ok("Item deleted successfully");
    }

}