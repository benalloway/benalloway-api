using Api.Context;
using Api.Models.Context;
using Api.Models.Payload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemStatusController : ControllerBase
{
    private readonly TodoDb _db;

    public ItemStatusController(TodoDb todoDb)
    {
        _db = todoDb;
    }

    // CREATE Status

    [HttpPost]
    public async Task<IActionResult> Post(ItemStatus status)
    {
        if (status == null || string.IsNullOrWhiteSpace(status.Status)) return BadRequest("Missing status data to create");

        try
        {
            ItemStatus newStatus = JsonConvert.DeserializeObject<ItemStatus>(JsonConvert.SerializeObject(status));
            await _db.ItemStatuses.AddAsync(newStatus);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }

        return Ok("Item Status created successfully");
    }

    // GET Statuses

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<ItemStatus> statuses = await _db.ItemStatuses.ToListAsync();

        return Ok(statuses);
    }

    [HttpGet("{id?}")]
    public async Task<IActionResult> Get(int? id)
    {
        if (id != null && id > 0)
        {
            ItemStatus status = await _db.ItemStatuses.FirstOrDefaultAsync(i => i.Id == id);

            if (status != null) return Ok(status);
            return NotFound("Could not find a status with that id");
        }

        return BadRequest("Missing id");
    }

    // UPDATE Status
    [HttpPut]
    public async Task<IActionResult> Put(ItemStatus status)
    {
        if (status == null) return BadRequest("missing status data to update");

        try
        {
            ItemStatus updateStatus = JsonConvert.DeserializeObject<ItemStatus>(JsonConvert.SerializeObject(status));
            _db.ItemStatuses.Update(updateStatus);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }

        return Ok("Status updated successfully");
    }



    // DELETE Status(es)
    [HttpDelete]
    public async Task<IActionResult> Delete(DeletePayload request)
    {
        if (request.Ids.Count < 1) return BadRequest("Need at least 1 id to delete statuses");

        // track success/failure
        var couldNotFindIds = new List<int>();
        var successfulIds = new List<int>();


        foreach (var id in request.Ids)
        {
            var status = await _db.ItemStatuses.FindAsync(id);
            if (status is null)
            {
                couldNotFindIds.Add(id);
                continue;
            }
            _db.ItemStatuses.Remove(status);
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
        var status = await _db.ItemStatuses.FindAsync(id);
        if (status is null)
        {
            return NotFound("Could not find status with that id");
        }
        _db.ItemStatuses.Remove(status);
        await _db.SaveChangesAsync();

        return Ok("Status deleted successfully");
    }

}