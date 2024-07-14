using UnitsNet;
using Iot.Device.DHTxx;

using TemperatureUpload.Models;

namespace TemperatureUpload
{
    public class ReadSensor
    {
        public static TemperatureModel GetSensorTemperatureData(Sensor sensor)
        {
            //Initialize variables for temperatures
            double temperature = default;
            int humidity = default;

            //Check SensorType
            switch (sensor.SensorTyp)
            {
                case 1: //DHT10
                    {
                        throw new NotImplementedException();
                    }
                    break;
                case 2: //DHT11
                    {
                        throw new NotImplementedException();
                    }
                    break;
                case 3: //DHT12
                    {
                        throw new NotImplementedException();
                    }
                    break;
                case 4: //DHT21
                    {
                        RelativeHumidity humidityDHT21 = default;
                        Temperature temperatureDHT21 = default;

                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);

                        // GPIO Pin
                        using Dht21 dht = new(sensor.GpioPin);
                        
                        //Read the current Temperature
                        bool success = dht.TryReadHumidity(out humidityDHT21) && dht.TryReadTemperature(out temperatureDHT21);

                        //Check if successful
                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT21.Value);
                            humidity = Convert.ToInt16(humidityDHT21.Value);
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            //TODO -> Reacting to errors
                        }
                    }
                    break;
                case 5: //DHT22
                    {
                        throw new NotImplementedException();
                    }
                    break;
                default:
                    throw new KeyNotFoundException();
            }

            TemperatureModel temperatureValue = new()
            {
                SensorName = sensor.SensorName,
                Temperature = temperature,
                Humidity = humidity,
            };

            return temperatureValue;
        }
    }
}