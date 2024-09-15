namespace TemperatureUpload.Models
{
    public class Settings
    {
        public InfluxDB InfluxDB {  get; set; } 
        public required List<Sensor> Sensor { get; set; } 
    }
    public class InfluxDB
    {
        public string WebAddress { get; set; }
        public string Token { get; set; }
        public string Org { get; set; }
        public string Bucket { get; set; }
    }
    public class Sensor
    {
        public int SensorTyp { get; set; }
        public int SensorId { get; set; }
        public int GpioPin { get; set; }
        public int ReadInterval { get; set; }
        public required string SensorName { get; set; }
    }
}