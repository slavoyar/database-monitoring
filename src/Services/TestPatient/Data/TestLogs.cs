using System.Diagnostics;
using System.Runtime.InteropServices;
using TestPatient.Models;

namespace TestPatient.Data
{
    public static class TestLogs
    {
        public static List<LogModel> GetWindowsLogs(string hostGuid)
        {
            var logsList = new List<LogModel>();

            if ( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
            {
                var random = new Random();
                var randomNumber = random.Next(1, 5);

                for ( int i = 1; i < randomNumber; i++ )
                {
                    Array values = Enum.GetValues(typeof(EventLogEntryType));
                    var randomInt = random.Next(values.Length);
                    var randomValue = values.GetValue(randomInt);
                    EventLogEntryType randomEventLogEntryType = (EventLogEntryType)randomValue;

                    var text = $"Random log: test event logs {randomEventLogEntryType}";

                    var source = "MiauApplication";
                    var log = "MiauEventLog";

                    if ( !EventLog.SourceExists(source) )
                    {
                        EventLog.CreateEventSource(source, log);
                        // The source is created.  Exit the application to allow it to be registered.
                        break;
                    }

                    EventLog demoLog = new(log)
                    {
                        Source = source
                    };

                    demoLog.WriteEntry(text, randomEventLogEntryType);

                    var lastEntry = demoLog.Entries[^1];

                    var newLog = new LogModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ServerId = hostGuid,
                        CriticalStatus = "CriticalStatus",
                        ErrorState = randomEventLogEntryType.ToString(),
                        ServiceType = "App",
                        ServiceName = lastEntry.MachineName,
                        CreatedAt = lastEntry.TimeGenerated.ToString(),
                        Message = text,
                        Sended = 0
                    };

                    logsList.Add(newLog);
                }
            }

            return logsList;
        }

        public static List<LogModel> GetTestLogs(string hostGuid)
        {
            var logsList = new List<LogModel>();

            var random = new Random();
            var randomNumber = random.Next(1, 5);

            for ( int i = 0; i < randomNumber; i++ )
            {
                Array values = Enum.GetValues(typeof(EventLogEntryType));
                var randomInt = random.Next(values.Length);
                var randomValue = values.GetValue(randomInt);
                EventLogEntryType randomEventLogEntryType = (EventLogEntryType)randomValue;

                var text = $"Random log: test event logs {randomEventLogEntryType}";

                var newLog = new LogModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ServerId = hostGuid,
                    CriticalStatus = "CriticalStatus",
                    ErrorState = randomEventLogEntryType.ToString(),
                    ServiceType = "App",
                    ServiceName = "TestMachine",
                    CreatedAt = DateTime.Now.ToString(),
                    Message = text,
                    Sended = 0
                };

                logsList.Add(newLog);
            }

            return logsList;
        }
    }
}