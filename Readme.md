# tmphonepad - TrackMaina Phone Pad

A hypothetically superior input device for TrackMania, with both the instant response of a keyboard and the analog precision of a thumbstick or joystick: A touchscreen!

This app uses [ViGEm](https://docs.nefarius.at/projects/ViGEm/) to create a virtual game controller device, and serves a tiny web app that you can open on a smartphone or tablet over the local network.

[Demo](https://github.com/stylpe/tmphonepad/assets/99851/15707cd8-eff5-4f4d-ab6d-e8e301970e49)

The only controls (for now) are left and right, which moves the virtual gamepad's left stick.

Status: Prototype v2, very bare bones proof of concept. Windows only, requires .Net 8 Runtime to run. Estimated network latency is shown in the bottom right corner, and is only about 1-3ms if you connect your phone to your PC via USB-C and set it to Tethering mode, though ViGEm and other factors might add a few more ms total.

Tested on Windows 11 and Sony Xperia 1 IV running Android 14. Should work on all Windows systems where .NET 8 is supported and most likely both Android and iPhone (which I don't have myself so cannot test)

## Usage

1. Install https://github.com/nefarius/ViGEmBus/releases/tag/v1.22.0 (and hope the team that made that gives us news about its successor soon!)
2. Install .NET 8 Runtime Windows Hosting Bundle: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.2-windows-hosting-bundle-installer (not the SDK, unless you want to build the project yourself)
2. (Optional for minimum latency) Connect your phone to your PC with a USB-C to USB-C cable, and set it to Tethering mode on your phone (at least on Android)
3. Download and extract tmphonepad.zip from https://github.com/stylpe/tmphonepad/releases/latest, then run it, which should show a console window and create a virtual controller device.
4. Scan the QR code from the console with your phone or tablet or other touchscreen device to open the web app (also check the messages above it in case your PC has more than one connected network)
5. Rotate to landscape and hit the Fullscreen button in the corner (to exit on Android you usually swipe from the bottom edge)
6. Use your phone for steering and your keyboard for everything else!

## Troubleshooting

- I get all sorts of warnings when I run this!
  - Yes, I haven't set up any code signing yet so Windows won't trust anything automatically. Either trust me, or clone the repo to review the code and build and run this yourself.
- My phone can't scan the QR code!
  - Then you'll just have to type it yourself, boy!
- My phone can't open the web app!
  - Maybe your wi-fi router, firewall or other network device is preventing connectivity. Try googling, or try connecting your phone to your computer with a USB-C cable in Tethering mode since that should also act as a network cable between most modern phones and PCs, and you'll probably get a different IP address for this which you have to figure out yourself. 
 - But ViGem is dead!
  - There is still hope: https://docs.nefarius.at/projects/VirtualPad/
- You forgot to meme!
  - Oh, right, *"Do you guys not have phones?!"*

## Plans

- Obviously, buttons for gas, brake, restart etc would be nice to have.
- Some design work is definitely needed, all I have now is basically debug colors.
- There are a few TODO comments about robustness and configurability. Adjustable deadzone is on top of the config list.
- ✅ I should showcase the input lag you can expect, but my own impression as a filthy casual is that it's pretty decent.
- It could also be possible to make something equivalent for making your phone act as a bluetooth controller. Drawbacks would be it needs to be an app, and I'm not sure there's broad vendor/hardware compatibility.
- Rumble?
- And tbh I don't like the name :P

Suggestions and contributions are welcome!

## Credits

Inspired by award-winning streamer and fellow Norwegian [Wirtual](https://www.twitch.tv/wirtual), the King of Controller Controversy. 
