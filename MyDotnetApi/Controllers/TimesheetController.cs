using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyDotnetApi.Controllers
{
    // Timesheet Controller (in Controllers/TimesheetController.cs)
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetController : ControllerBase
    {
        private static List<TimesheetEntry> _entries = TimesheetDataService.Load();

        [HttpGet]
        public IActionResult GetAll() => Ok(_entries);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimesheetEntry entry)
        {
            entry.Id = _entries.Count > 0 ? _entries.Max(e => e.Id) + 1 : 1;
            _entries.Add(entry);
            TimesheetDataService.Save(_entries);
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TimesheetEntry entry)
        {
            var existing = _entries.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            existing.Date = entry.Date;
            existing.StartTime = entry.StartTime;
            existing.EndTime = entry.EndTime;
            existing.ProjectName = entry.ProjectName;
            existing.Task = entry.Task;
            existing.Remarks = entry.Remarks;

            TimesheetDataService.Save(_entries);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _entries.FirstOrDefault(e => e.Id == id);
            if (existing == null) return NotFound();

            _entries.Remove(existing);
            TimesheetDataService.Save(_entries);
            return NoContent();
        }
        // Register controller in Program.cs or Startup.cs
        // No database used yet, just in-memory list

        public class TimesheetEntry
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string ProjectName { get; set; } = string.Empty;
            public string Task { get; set; } = string.Empty;
            public string Remarks { get; set; } = string.Empty;
        }

    }
}
