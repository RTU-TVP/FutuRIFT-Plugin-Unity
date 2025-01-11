# FutuRIFT Plugin for Unity

[Рус](README.md) | [Eng](README.en.md)

The plugin allows you to interact with the program that controls the FutuRIFT chair in Unity.

## Adding a plugin to a project

You can install the plugin in any of the following ways

* Download unitypackage:
* Download one of the public versions of the plugin, which can be found [**here**](https://github.com/RTU-TVP/FutuRIFT-Plugin-Unity/releases ).
  * Import the downloaded *unitypackage* into your Unity project.
  * Once added, Unity will automatically pull up the plugin, allowing you to use it.

* Using Package Manager (for Unity 2019.3 and higher):
* Open Package Manager in Unity.
  * Click on the **+** button in the upper-left corner of the Package Manager window.
  * Select **Add package from git URL**.
  * Enter a link to the plugin repository: `https://github.com/RTU-TVP/FutuRIFT-Plugin-Unity .git?path=src/FutuRIFT-Plugin/Assets/Plugins/FutuRIFT`

* Download the source code:
* Clone the plugin repository.
  * Copy the **Plugins/FutuRIFT** folder to the **Assets** folder of your project.

## Using the Plugin

The basic element for controlling the chair is the **FutuRIFTController** class.

```cs
// The <c>FutuRIFTController</c> class is designed to control the FutuRIFT device by sending UDP messages.
class FutuRIFTController
{
    // A property for getting and setting the pitch angle.
    // Tilt forward or backward.
    // The value is limited to the range from -15 to 21.
    float Pitch { get; set; }

    // A property for getting and setting the roll angle.
    // Tilt to the left or right.
    // The value is limited to the range from -18 to 18.
    float Roll { get; set; }

    // Constructor of the <c>FutuRIFTController</c> class.
    // "sender" is an object that implements the ISender interface for sending data.
    public FutuRIFTController(ISender sender)

    // Method to start sending UDP messages.
    void Start();
    
    // Method to stop sending UDP messages.
    void Stop();
}
```

### Network Control via the FutuRIFT Controller

For convenient usage and testing, you can use [special software](https://github.com/RTU-TVP/FutuRIFT-Controller-Emulator) that acts as both a chair emulator and a controller.

This controller receives commands via UDP. To use it, you should provide the IP address and port in the constructor.

```cs
// ...
private FutuRIFTController controller;

void OnEnable()
{
    var sender = new UdpSender("127.0.0.1", 6065);
    controller = new FutuRIFTController(sender);
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

FutuRIFT Plugin Unity is a project by the RTU IT LAB department and was improved by RTU TVP student Kirill Shutov. If you have any questions, please contact me by email: <i@shutovks.ru>.
