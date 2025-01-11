# Futurift Plugin for Unity

[Рус](README.md) | [Eng](README.en.md)

This plugin allows you to control the FutuRift chair from applications written in Unity.

## Adding the Plugin to Your Project

* Download one of the public versions of the plugin, which can be found [**here**](https://github.com/RTU-TVP/Futurift-Plugin-Unity/releases).

* Import the downloaded *unitypackage* into your Unity project.

* After adding it, Unity will automatically pull in the plugin, allowing you to use it.

## Using the Plugin

The basic element for controlling the chair is the **FutuRiftController** class.

```cs
// The <c>FutuRiftController</c> class is designed to control the FutuRift device by sending UDP messages.
class FutuRiftController
{
    // A property for getting and setting the pitch angle.
    // Tilt forward or backward.
    // The value is limited to the range from -15 to 21.
    float Pitch { get; set; }

    // A property for getting and setting the roll angle.
    // Tilt to the left or right.
    // The value is limited to the range from -18 to 18.
    float Roll { get; set; }

    // Constructor of the <c>FutuRiftController</c> class.
    // "ip" - The IP address to which the UDP message will be sent.
    // "port" - The port through which the UDP message will be sent.
    public FutuRiftController(string ip = "127.0.0.1", int port = 6065)

    // Method to start sending UDP messages.
    void Start();
    
    // Method to stop sending UDP messages.
    void Stop();
}
```

### Network Control via the Futurift Controller

For convenient usage and testing, you can use [special software](https://github.com/RTU-TVP/Futurift-Controller-Emulator) that acts as both a chair emulator and a controller.

This controller receives commands via UDP. To use it, you should provide the IP address and port in the constructor.

```cs
// ...
private FutuRiftController controller;

void OnEnable()
{
    controller = new FutuRiftController("127.0.0.1", 6065);
    controller.Start();
}
// ...
```

#### Pitch

A property that changes or returns the forward/backward tilt of the chair. It can range from -15 to 21.

#### Roll

A property that changes or returns the left/right tilt of the chair. It can range from -18 to 18.

> If values outside this range are passed, they will be clamped to the allowable range. For example, when assigning:

```cs
sample.Roll = 120f;
sample.Pitch = -16f
```

the **Roll** property will be set to 18, and **Pitch** will be set to -15.

### Start()

A method that begins sending control commands to the chair.

### Stop()

A method that stops controlling the chair.

## License

This project is licensed under the MIT license – see the LICENSE file for details.

## Contact

Futurift Plugin Unity is a project by the RTU IT LAB department and was improved by RTU TVP student Kirill Shutov. If you have any questions, please contact me by email: <i@shutovks.ru>.
