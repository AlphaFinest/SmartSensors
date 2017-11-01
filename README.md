# SmartSensors

ICB's final project for Telerik Academy Alpha course.

# 1	Project description
ICB wants a simple public IoT Sensor Portal to be developed. Referenced as The Application. The portal will fetch and store sensor data form various sources. It will support public sensor data access along with private sensors. The logged in users will be able to share sensors data between them.
# 2	Supported sensor types
ICB will provide a web API’s (with set of examples how to use it) to fetch sensors data at http://telerikacademy.icb.bg/
The following sensor types should be supported:
-	Temperature measured in °C
-	Humidity sensor measure in relative humidity (in percent )
-	Electric power consumption sensor in Watts 
-	Occupancy sensor (true or false) depending on whether there are persons or not in the measured room. True means there are persons in the room.
-	Window /door sensor. Measured with true/false depending whether the door is open or closed. True means the door is closed
-	Noise sensor with values in Decibels
# 3	Public part
## 3.1	Landing page
Application should have public landing page. The landing page should contain product description and links. Along with corresponding functionality:
-	Login
-	Register
-	View public sensors
## 3.2	Register
A register page must have at least username field, password field and email field. Email will be necessary if you decide app’s users to receive notification about the sensor, which is out of range (see Could Have Requirements 7.2 - 2)
## 3.3	View public sensors
The Application should have a page with a list of all public sensors of all registered users. It should be represented as a list, which supports paging and filtering. The page should be publicly accessible. 
Each sensor of the public sensor list should have user’s name, sensor’s name 
The user should have a detail sensor view which shows the current value of the sensor. Use three different colors for sensor data validity (e.g  you can use green/blue color for showing that value from the sensor is in acceptable range, red/orange color – value is out of acceptable range and grey color – there is no connection with the sensor. ) There should be possibility to view historical data for the sensor choosing from and to periods.
## 3.4	Sensor offline
The Application should handle when sensor is offline and show this to the users.
# 4	Private part
The Application should have a private section which is accessible only for logged users. This section should support the following functionality:
## 4.1	Register new sensor
The newly created sensor should have its own:
-	Name
-	Description
-	URL for fetching sensor data
-	Polling interval which specifies the amount of time to refresh sensor data
-	Measurement type which specifies the type of measurement (temperature, humidity etc. See 2 Supported sensor types)
-	Access (public or private) which specifies whether the sensor is publicly visible or only accessible for the user
-	Range of acceptable values (e.g. -40 °C to +100 °C)
## 4.2	View list of own sensors
The logged user should have a place where he/she can view his/hers registered sensors. For each sensor the list should include at least:
-	Name
-	Description
-	Current value
-	Access level (public or private)
-	Whether the sensor is shared and with whom

Like the public sensors, each of the own sensors should also have a detail sensor view which shows the current value of the sensor. There should be three different colors for sensor data validity and also there should be possibility to view historical data for the sensor choosing from and to periods.
## 4.3	Modify existing sensor
The logged user should be able to edit his/hers own sensors. The data which should be editable is the same as the data entered when registering the sensor (URL, polling interval, measurement type, access, etc.).
## 4.4	Share a private sensor
The logged user should have the ability to share his/hers private sensors with other users. Those other users should have Read-Only access to the original sensors.
# 5	Administration
Application must have three main roles:
-	Non-logged/non-registered users can see only public part.
-	A Logged user (non-administrator) has the abilities described in Section 4 (Private part).
-	An administrator has the same abilities like logged users and in addition he/she can:  
## 5.1	Edit registered users
Administrators can edit information for registered users.
## 5.2	Add new user
Each administrator can add new user, including administrator.
## 5.3	Edit sensors of registered users
Administrators can:
-	Modify/insert all information about existing sensors (name, description, URL for fetching sensor data, polling interval, measurement type, access level and share functionality) of each registered user, no matter if sensor is private or public.
-	Register new sensor for each already registered user.
-	View list of all sensors (public/private) of each registered user.
# 6	Sensor Data hub module
The Application should have a module which gathers data from the registered sensors and store their values for historical reasons. The module should contain also analytics.
## 6.1	Sensor Data Polling
The module should poll data from sensors based on their pooling interval setting. Note that a sensor could have pooling interval of 1 minute and another could have pooling interval set to 5 minutes. The data should be stored for historical reasons. Stored data should be used when showing sensor historical data. In order to make that requirement, you will be provided with windows service and a document which include steps how to install the service, examples how to use it and steps how to uninstall it. Your task is to create controller with action, that implement the logic for data polling and provide it to the service. The provided windows service will invoke the implemented controller action on a configurable interval. It’s the controller action responsibility to decide whether and which sensors data to poll.