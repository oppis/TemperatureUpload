# TemperatureUpload
Save temperature values in InfluxDB. It is possible to measure several sensors simultaneously. Multiple sensors are then specified in the Sensor array in JSON
Sensor type is specified in SensorType as shown in the table. Sensor ID is for I2C sensors and GPIO pins for OneWire. SensorName is also written to the table. ReadInterval is for pausing the measurement
Error messages are stored in a single file in the executing folder.

# Sensor Types

| Sensor Type | ID  |
|-------------|-----|
| DHT10       | 1   |
| DHT11       | 2   |
| DHT12       | 3   |
| DHT21       | 4   |
| DHT22       | 5   |
| Sht3x       | 6   |

# Setting Influx DB
The connection details for InfluxDB are made in settings.json
- WebAdress
- Token
- Org
- Nucket
