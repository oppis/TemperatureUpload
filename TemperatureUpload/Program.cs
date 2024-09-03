using System.Text.Json;
using TemperatureUpload.Models;

namespace TemperatureUpload
{
    internal class Program
    {
        public static Settings SettingsRead { get; set; }

        static void Main(string[] args)
        {
            //Read the Settings
            InitializeSettings();

            //Loop for reading
            foreach (Sensor currentSensor in SettingsRead.Sensor) 
            {
                Console.WriteLine("Start des Durchlaufes der Sensoren!");

                for (int i = 0; i < 50; i++)
                {
                    TemperatureModel currentTemperature = ReadSensor.GetSensorTemperatureData(currentSensor);
                    Console.WriteLine(currentTemperature.Temperature.ToString());
                    //InfluxDBClient.SaveTemperatureData(currentTemperature);
                    Thread.Sleep(2000);  
                }

            }
        }

        /// <summary>
        /// Function for initializing the settings for the connection to the Influx database
        /// </summary>
        public static void InitializeSettings()
        {
            Console.WriteLine("Es werden die Einstellungen geladen!");

            string currentPath = Path.Combine(AppContext.BaseDirectory, "Settings.json");

            Console.WriteLine($"{currentPath}");
            string settingsJson = File.ReadAllText(currentPath);

            SettingsRead = JsonSerializer.Deserialize<Settings>(settingsJson);

            Console.WriteLine("Es wurden die Einstellungen geladen!");
        }
    }
}