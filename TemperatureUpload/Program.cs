using System.Text.Json;
using TemperatureUpload.Models;

namespace TemperatureUpload
{
    internal class Program
    {
        public static Settings SettingsRead { get; set; }
        private static int CurrentSensorCount = 0;
        static void Main(string[] args)
        {
            try
            {
                //Read the Settings
                InitializeSettings();

                while (true)
                {
                    foreach (Sensor currentSensor in SettingsRead.Sensor)
                    {
                        Console.WriteLine("Start des Durchlaufes der Sensoren!");

                        Thread sensorRead = new(() => ReadCurrentSensor(currentSensor));
                        sensorRead.Start();
                    }
                    while (true) //Waiting for all current measurements 
                    {
                        Thread.Sleep(100);
                        if (CurrentSensorCount == 0)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Measurement with waiting time
        /// </summary>
        /// <param name="currentSensor"></param>
        private static void ReadCurrentSensor(Sensor currentSensor)
        {
            CurrentSensorCount++;

            TemperatureModel currentSensorValues = ReadSensor.GetSensorTemperatureData(currentSensor);
            Console.WriteLine(currentSensorValues.Temperature.ToString());
            Console.WriteLine(currentSensorValues.Humidity.ToString());

            if (!String.IsNullOrEmpty(SettingsRead.InfluxDB.WebAddress))
            {
                Console.WriteLine("Es wurde eine InfluxDB Datenbank angeben! Es werden die Werte in der Datenbank gesichert!");
                InfluxDBClient.SaveTemperatureData(currentSensorValues);
            }
            else
            {
                Console.WriteLine("Es wurden kein Influx DB Server angeben die Werte werden nicht gespeichert!");
            }

            Thread.Sleep(currentSensor.ReadInterval);
            CurrentSensorCount--;
        }

        /// <summary>
        /// Function for initializing the settings for the connection to the Influx database
        /// </summary>
        private static void InitializeSettings()
        {
            Console.WriteLine("Es werden die Einstellungen geladen!");

            string currentPath = Path.Combine(AppContext.BaseDirectory, "Settings.json");

            Console.WriteLine($"{currentPath}");
            string currentfile = File.ReadAllText(currentPath);
            SettingsRead = JsonSerializer.Deserialize<Settings>(currentfile);

            Console.WriteLine("Es wurden die Einstellungen geladen!");
        }

        /// <summary>
        /// Write Error Message to Log
        /// </summary>
        private static void WriteErrorLog(string ErrorMessage, string ErrorStackTrace)
        {
            DateTime dateTime = DateTime.Now;

            string currentLogName = dateTime.ToString("dd-MM-yyy_HHmm") + ".txt";
            string currentLogPath = Path.Combine(AppContext.BaseDirectory, currentLogName);

            string[] errorLog = [ErrorMessage, Environment.NewLine, ErrorStackTrace];
            File.WriteAllLines(currentLogPath, errorLog);
        }
    }
}