using InfluxDB.Client.Writes;

using TemperatureUpload.Models;

namespace TemperatureUpload
{
    /// <summary>
    /// Class for executing actions with the Influx database
    /// </summary>
    public class InfluxDBClient
    {
        /// <summary>
        /// Saving temperature and humidity values with sensor name
        /// </summary>
        /// <param name="temperatureName">Temperatur name</param>
        /// <param name="temperature">Temperature value emitted</param>
        /// <param name="humidity">Humidity value emitted</param>
        public static async void SaveTemperatureData(TemperatureModel temperature)
        {
            //Get Current Settings for InfluxDB
            Models.InfluxDB settingsInfluxDB = Program.SettingsRead.InfluxDB;
                
            //Create Connection to InfluxDB
            using var client = new InfluxDB.Client.InfluxDBClient(settingsInfluxDB.WebAddress, settingsInfluxDB.Token);
            var writeApiAsync = client.GetWriteApiAsync();

            //Create measuring point for transfer to the DB
            var point = PointData
                .Measurement(temperature.SensorName)
                .Field("temperature", temperature.Temperature)
                .Field("humidity", temperature.Humidity)
                .Timestamp(DateTime.Now, InfluxDB.Client.Api.Domain.WritePrecision.Ms);

            //Write measuring point in DB
            await writeApiAsync.WritePointAsync(point, settingsInfluxDB.Bucket, settingsInfluxDB.Org);

            client.Dispose();
        }
    }
}