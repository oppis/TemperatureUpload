using UnitsNet;
using System.Device.I2c;
using Iot.Device.DHTxx;
using Iot.Device.Sht3x;

using TemperatureUpload.Models;

namespace TemperatureUpload
{
    public class ReadSensor
    {
        public static TemperatureModel GetSensorTemperatureData(Sensor sensor)
        {
            //Initialize variables for temperatures
            double temperature = default;
            double humidity = default;

            Console.WriteLine("Start der Messung!");

            //Check SensorType
            switch (sensor.SensorTyp)
            {
                case 1: //DHT10
                    {
                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);
                        I2cConnectionSettings settings = new(sensor.SensorId, (byte)I2cAddress.AddrLow);
                        I2cDevice device = I2cDevice.Create(settings);

                        using Dht12 dht = new(device);
                        Temperature temperatureDHT10 = default;
                        RelativeHumidity humidityDHT10 = default;
                        bool success = dht.TryReadHumidity(out humidityDHT10) && dht.TryReadTemperature(out temperatureDHT10);

                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT10.Value);
                            humidity = humidityDHT10.Value;
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            throw new Exception("Error reading DHT sensor");
                        }
                    }
                    break;
                case 2: //DHT11
                    {
                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);
                        using Dht11 dht = new(sensor.GpioPin);

                        Temperature temperatureDHT11 = default;
                        RelativeHumidity humidityDHT11 = default;
                        bool success = dht.TryReadHumidity(out humidityDHT11) && dht.TryReadTemperature(out temperatureDHT11);

                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT11.Value);
                            humidity = humidityDHT11.Value;
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            throw new Exception("Error reading DHT sensor");
                        }
                    }
                    break;
                case 3: //DHT12
                    {
                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);
                        using Dht12 dht = new(sensor.GpioPin);

                        Temperature temperatureDHT12 = default;
                        RelativeHumidity humidityDHT12 = default;
                        bool success = dht.TryReadHumidity(out humidityDHT12) && dht.TryReadTemperature(out temperatureDHT12);

                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT12.Value);
                            humidity = humidityDHT12.Value;
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            throw new Exception("Error reading DHT sensor");
                        }
                    }
                    break;
                case 4: //DHT21
                    {
                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);                      
                        using Dht21 dht = new(sensor.GpioPin);

                        Temperature temperatureDHT21 = default;
                        RelativeHumidity humidityDHT21 = default;
                        bool success = dht.TryReadHumidity(out humidityDHT21) && dht.TryReadTemperature(out temperatureDHT21);

                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT21.Value);
                            humidity = humidityDHT21.Value;
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            throw new Exception("Error reading DHT sensor");
                        }
                    }
                    break;
                case 5: //DHT22
                    {
                        Console.WriteLine("GPIO-PIN: " + sensor.GpioPin);
                        using Dht22 dht = new(sensor.GpioPin);

                        Temperature temperatureDHT22 = default;
                        RelativeHumidity humidityDHT22 = default;
                        bool success = dht.TryReadHumidity(out humidityDHT22) && dht.TryReadTemperature(out temperatureDHT22);

                        if (success)
                        {
                            temperature = Math.Round(temperatureDHT22.Value);
                            humidity = humidityDHT22.Value;
                        }
                        else
                        {
                            Console.WriteLine("Error reading DHT sensor");
                            throw new Exception("Error reading DHT sensor");
                        }
                    }
                    break;
                case 6: //SHT3x
                    {
                        I2cConnectionSettings settings = new(sensor.SensorId, (byte)I2cAddress.AddrLow);
                        I2cDevice device = I2cDevice.Create(settings);
                        using Sht3x currentSensor = new(device);

                        temperature = currentSensor.Temperature.DegreesCelsius;
                        humidity = currentSensor.Humidity.Percent;
                        currentSensor.Heater = true;
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