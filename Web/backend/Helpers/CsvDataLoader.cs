using Findgroup_Backend.Models;

namespace Findgroup_Backend.Helpers
{
    public static class CsvDataLoader
    {
        private const string ACTIVITY_CSV_PATH = @"..\Data\Seeders";
        public static IEnumerable<Activity> LoadActivity() 
        {
            using StreamReader reader = new(ACTIVITY_CSV_PATH + "activity.csv");
            string header = reader.ReadLine()!;
            while (!reader.EndOfStream) 
            {
                string[] csvLine = reader.ReadLine()!.Split(',');
                Activity activity = new()
                {
                    ProcessName = csvLine[0],
                    ActivityName = csvLine[1]
                };
                yield return activity;
            }
        }
    }
}
