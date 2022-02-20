using Minecraft.Net;
using OpenTK.Windowing.Desktop;

GameWindowSettings gameWindowSettings = new()
{
};

NativeWindowSettings nativeWindowSettings = new()
{
    Size = new(1280, 720),
    Title = "Minecraft"
};

using var window = new Window(gameWindowSettings, nativeWindowSettings);
window.Run();