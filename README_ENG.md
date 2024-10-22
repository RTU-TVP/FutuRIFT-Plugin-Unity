# Futurift plugin Unity

[Rus](README.md) | [Eng](README_ENG.md)

This plugin allows you to control the FutuRift chair from applications written in Unity.

## Adding the Plugin to a Project

* You need to download one of the public versions of the plugin, which can be found [**here**](https://github.com/RTU-TVP/Futurift-Plugin-Unity/releases).

* The downloaded *unitypackage* needs to be imported into your Unity project.

* After adding it, Unity will automatically integrate the plugin and allow you to use it.

## Using the Plugin

The basic control element for the chair is the **FutuRiftController** class:

```cs
class FutuRiftController
{
    float Pitch { get; set; }
    float Roll { get; set; }
    bool IsConnected { get; }
    void Start();
    void Stop();
}
```

### Controlling Over the Network via Futurift Controller

For convenient use and testing, you can use [special software](https://github.com/RTU-TVP/Futurift-Controller-Emulator), which acts as a chair and controller emulator.

This controller accepts commands via UDP. To use it, you need to initialize it with the `UdpOptions` constructor.

```cs
// ...
private FutuRiftController controller;

void OnEnable()
{
    controller = new FutuRiftController(new UdpPortSender(new UdpOptions 
    {
        ip = "127.0.0.1", // IP address of the computer running the controller
        port = 6065 // Port configured for the controller
    }));
    controller.Start();
}
// ...
```

### Direct Control

If necessary, you can skip the intermediary software by using the `ComPortOptions` constructor to directly control the chair. You can also use the constructor that accepts `UdpOptions`.

```cs
// ...
private FutuRiftController controller;

void OnEnable()
{
    controller = new FutuRiftController(new ComPortSender(new ComPortOptions
    {
        ComPort = 3 // The port number to which the chair is connected
    }));
    controller.Start();
}
// ...
```

#### Pitch

This property sets or returns the forward/backward tilt of the chair and can take values from -15 to 21.

#### Roll

This property sets or returns the left/right tilt of the chair and can take values from -18 to 18.

> If values outside this range are provided, they will be clamped to the allowed limits. For example:

```cs
sample.Roll = 120f;
sample.Pitch = -16f;
```

The **Roll** property will be set to 18, and **Pitch** will be set to -15.

### Start()

This method starts sending control commands to the chair. If the port was successfully opened and data is being transmitted, the **IsConnected** property will be *true*.

### Stop()

This method stops controlling the chair. After calling this method, the port will no longer be used by the program.

## Important

In the project settings (Project Settings - Player), the **Api Compatibility Level** parameter must be set to .NET Framework.

## License

This project is licensed under the MIT license - details can be found in the LICENSE file.

## Contacts

Futurift plugin Unity is a project from the RTU IT LAB department and was further developed by RTU TVP student Shutov Kirill. If you have any questions, please contact me at <i@shutovks.ru>.
