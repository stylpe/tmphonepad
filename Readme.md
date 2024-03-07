# tmphonepad - TrackMaina Phone Pad

A hypothetically superior input device for TrackMania, with both the instant response of a keyboard and the analog precision of a thumbstick or joystick: A touchscreen!

This app uses [ViGEm](https://docs.nefarius.at/projects/ViGEm/) to create a virtual game controller device, and serves a tiny web app that you can open on a smartphone or tablet over the local network.

![screenshot](https://github.com/stylpe/tmphonepad/assets/99851/b80f99df-85ee-4034-accf-fcda33214132)

The only controls (for now) are left and right, which moves the virtual gamepad's left stick.

Status: Prototype v1, very bare bones proof of concept. Windows only, requires .Net 8 Runtime to run.

## Usage

1. Install https://github.com/nefarius/ViGEmBus/releases/tag/v1.22.0 (and hope the team that made that gives us news about its successor soon!)
2. Install .NET 8 Runtime Windows Hosting Bundle: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.2-windows-hosting-bundle-installer (not the SDK, unless you want to build the project yourself)
2. Download and extract tmphonepad.zip from https://github.com/stylpe/tmphonepad/releases/latest, then run it, which should show a console window and create a virtual controller device.
3. Scan the QR code from the console with your phone or tablet or other touchscreen device to open the web app
4. Rotate to landscape and hit the Fullscreen button in the corner (to exit on Android you usually swipe from the bottom edge)
5. Use your phone for steering and your keyboard for everything else!

## Troubleshooting
- I get all sorts of warnings when I run this!
  - Yes, I haven't set up any code signing yet so Windows won't trust anything automatically. Either trust me, or clone the repo to review the code and build and run this yourself.
- My phone can't scan the QR code!
  - Then you'll just have to type it yourself, boy!
- My phone can't open the web app!
  - Maybe your wi-fi router, firewall or other network device is preventing connectivity. Try googling, or try connecting your phone to your computer with a USB-C cable since that should also act as a network cable on most modern phones, possibly needing manual enabling, and you'll probably get a different IP address for this which you have to figure out yourself.
 - But ViGem is dead!
  - There is still hope: https://docs.nefarius.at/projects/VirtualPad/
- You forgot to meme!
  - Oh, right, *"Do you guys not have phones?!"*

## Plans

- Obviously, buttons for gas, brake, restart etc would be nice to have.
- Some design work is definitely needed, all I have now is basically debug colors.
- There are a few TODO comments about robustness and configurability.
- I should showcase the input lag you can expect, but my own impression as a filthy casual is that it's pretty decent.
- It could also be possible to make something equivalent for making your phone act as a bluetooth controller. Drawbacks would be it needs to be an app, and I'm not sure there's broad vendor/hardware compatibility.
- And tbh I don't like the name :P

Suggestions and contributions are welcome!

## Credits

Inspired by award-winning streamer and fellow Norwegian [Wirtual](https://www.twitch.tv/wirtual), the King of Controller Controversy. 
