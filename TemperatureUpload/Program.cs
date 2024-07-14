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
                for (int i = 0; i < 50; i++)
                {
                    TemperatureModel currentTemperature = ReadSensor.GetSensorTemperatureData(currentSensor);
                    InfluxDBClient.SaveTemperatureData(currentTemperature);
                    Thread.Sleep(2000);  
                }

            }
        }

        /// <summary>
        /// Function for initializing the settings for the connection to the Influx database
        /// </summary>
        public static void InitializeSettings()
        {
            string settingsJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Settings.json"));

            SettingsRead = JsonSerializer.Deserialize<Settings>(settingsJson);
        }
    }
}