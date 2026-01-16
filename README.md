
# Really simple digital twins mockup

This project is a simple digital twin of a server rack built with Unity for visualization and ASP.NET Core for backend data. It demonstrates how server status, temperature, and CPU usage can be fetched from a backend and visualized in Unity.

This is a mockup / demo and uses simulated data from the backend.

More Updates to come!
Feel free to contribute

## Contents:
- [Showcase](#Showcase)
- [Features](#Features)
- [Structure](#Structure)
- [Setup](#Setup)
---
## Showcase
![Showcase](https://raw.githubusercontent.com/Melyguy/Digital-Twins-Unity/main/images/Showcase1.png)
Here we can see that the servers that are set as online have a green cube in the center. While the servers that are offline have a red cube.
![Showcase](https://raw.githubusercontent.com/Melyguy/Digital-Twins-Unity/main/images/ControlPanel.png)
Now we see that if the degrees are over 60 that the text that shows it is yellow and if its over 80 it becomes red.
![Showcase](https://raw.githubusercontent.com/Melyguy/Digital-Twins-Unity/main/images/ControlPanel2.png)
---
## Features
- 3D server rack visualization in unity
- Server GameObjects change color based on **temperature** and **online status**
- Backend provides **JSON server data** via a REST API
- Supports **periodic polling** to simulate live updates
- Clean separation between **Unity frontend** and **.NET backend**

### Color Mapping

| Temperature / Status | Color  |
|---------------------|--------|
| < 60°C              | Green  |
| 60–80°C             | Yellow |
| > 80°C              | Red    |
---

## Structure

DigitalTwin/  
├─ UnityClient/ # Unity project  
│ ├─ Assets/  
│ │ ├─ Scripts/   
│ │ └─ ServerState.cs    
│ │	└─ ServerDataClient.cs   
│ │ └─ ServerRackManager.cs  
│ │ └─ Prefabs/  
│ │ └─ Server.prefab  
│ └─ UnityClient.sln  
│  
├─ Backend/ # ASP.NET Core backend  
│ ├─ ServerTwin.Api/  
│ │ ├─ Program.cs  
│ │ └─ ServerTwin.Api.csproj  
│ └─ Backend.sln  
└─ README.md

---

## Setup

### Prerequisites

- Unity 6000.2.13
- .NET 8 SDK (or later)
- Visual Studio or VS Code (optional)

---

