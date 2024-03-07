# tmphonepad - TrackMaina Phone Pad

A hypothetically superior input device for TrackMania, with both the instant response of a keyboard and the analog precision of a thumbstick or joystick.

Status: Prototype v1, very bare bones proof of concept. Windows only, requires .Net 8 Runtime to run.

## Usage

1. Install https://github.com/nefarius/ViGEmBus/releases/tag/v1.22.0 (and hope the team that made that gives us news about its successor soon!)
2. Install .NET 8 Runtime Windows Hosting Bundle: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.2-windows-hosting-bundle-installer (not the SDK, unless you want to build the project yourself)
2. Download and extract tmphonepad.zip from https://github.com/stylpe/tmphonepad/releases/latest, then run it, which should show a console window and create a virtual controller device 
3. Scan the QR code from the console with your phone or tablet or other touchscreen device (or manually type the address in a browser)
4. Rotate to landscape and hit the Fullscreen button in the corner (to exit on Android you usually swipe from the bottom edge)
5. Use your phone for steering and your keyboard for everything else!

## Plans

Obviously, buttons for gas, brake, restart etc would be nice to have.

There are a few TODO comments about robustness, and configurability.

It could also be possible to make something equivalent for making your phone act as a bluetooth controller. Drawbacks would be it needs to be an app.

And tbh I don't like the name :P

## Credits

Inspired by award-winning streamer [Wirtual](https://www.twitch.tv/wirtual), the King of Controller Controversy. 
