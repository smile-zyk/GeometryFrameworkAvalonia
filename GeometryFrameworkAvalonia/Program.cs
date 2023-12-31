﻿using Avalonia;
using System;

namespace GeometryFrameworkAvalonia
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .With(new X11PlatformOptions { UseGpu = true, UseDeferredRendering = false })
                //.With(new MacOSPlatformOptions { UseGpu = true })
                .With(new Win32PlatformOptions { UseWgl = true });
    }
}