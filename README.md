# Wireless-Sensor-Network-IOT-Simulator
Simulates large number of sensor nodes, connected to a network, capable of moving, sensing etc.

Check following videos:
* [Video 1](https://www.youtube.com/watch?v=3kSEdBrZcMw)
* [Video 2](https://www.youtube.com/watch?v=fuGOqhb46Kw)
* [Video 3](https://www.youtube.com/watch?v=VclimiyCuks)

![image1](https://github.com/alikhorsandian/Wireless-Sensor-Network-IOT-Simulator/blob/master/doc/images/background.jpg)

## Node Simulator
WRSNSim is a simulator environment which simulates a network of stationary and mobile internet connected things that can pass and broadcast their sensory information and receive their actuators instruction to act with the environment. WRSNSim is initially developed to simulate wireless rechargeable sensor network but we are working to generalize it to a logistic platform.

![image2](https://github.com/alikhorsandian/Wireless-Sensor-Network-IOT-Simulator/blob/master/doc/images/result1.jpg)

## Node Types
The simulator creates two types of nodes (working concurrently), stationary and mobile. Every node have its own battery, sensor, motors and transceivers such as z-wave, zigbee or Wi-Fi. Mobile nodes has the ability to move and usually can have better features in a matter of battery capacity and transceivers power. However, stationery nodes have lower specifications. 

The simulator allows to test different algorithms for different environment. For example, static nodes can be assumed as vending machine with limited resources. And mobile nodes can be assumed as trucks providing supply for the vending machine. Different algorithm can be tested with the simulator to creates efficiency in using trucks and providing a great service. However, the project is created to be a test-bed to compare different type of routing protocols for the wireless sensor networks.

## System Architecture
![](https://github.com/alikhorsandian/Wireless-Sensor-Network-IOT-Simulator/blob/master/doc/images/architecture.jpg)


