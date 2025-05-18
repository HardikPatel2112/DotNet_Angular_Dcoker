using static MyDotnetApi.Controllers.TimesheetController;
using System.Text.Json;

namespace MyDotnetApi.Controllers
{
    // Data service to read/write JSON file
    public static class TimesheetDataService
    {
        private static readonly string FilePath = "timesheet.json";

        public static List<TimesheetEntry> Load()
        {
            if (!File.Exists(FilePath)) return new List<TimesheetEntry>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TimesheetEntry>>(json) ?? new List<TimesheetEntry>();
        }

        public static void Save(List<TimesheetEntry> entries)
        {
            var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
