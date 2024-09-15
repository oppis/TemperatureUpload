namespace TemperatureUpload.Models
{
    /// <summary>
    /// Model for temperature readout
    /// </summary>
    public class TemperatureModel
    {
        /// <summary>
        /// Sensor Name
        /// </summary>
        public string SensorName { get; set; }
        /// <summary>
        /// Temperature
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Humidity
        /// </summary>
        public double Humidity { get; set; }
    }
}