
# Really simple digital twins mockup

This project is a simple digital twin of a server rack built with Unity for visualization and ASP.NET Core for backend data. It demonstrates how server status, temperature, and CPU usage can be fetched from a backend and visualized in Unity.

This is a mockup / demo and uses simulated data from the backend.


## Contents:
- [Features](#Features)
- [Structure](#Structure)
- [Setup](#Setup)
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
## Showcase
![Showcase](https://raw.githubusercontent.com/USERNAME/REPO/main/docs/showcase.png)

