﻿using BIlter.Commands;
using Nice3point.Revit.Toolkit.External;
using Serilog.Events;

namespace BIlter
{
    [UsedImplicitly]
    public class Application : ExternalApplication
    {
        public override void OnStartup()
        {
            CreateLogger();
            CreateRibbon();
        }

        public override void OnShutdown()
        {
            Log.CloseAndFlush();
        }

        private void CreateRibbon()
        {
            var panel = Application.CreatePanel("Commands", "BIlter");

            var showButton = panel.AddPushButton<Command>("Execute");
            showButton.SetImage("/BIlter;component/Resources/Icons/RibbonIcon16.png");
            showButton.SetLargeImage("/BIlter;component/Resources/Icons/RibbonIcon32.png");


            var winButton = panel.AddPushButton<RelayCommand>("SimpleMVVM");
            winButton.SetImage("/BIlter;component/Resources/Icons/Windows16.png");
            winButton.SetLargeImage("/BIlter;component/Resources/Icons/Windows32.png");
        }

        private static void CreateLogger()
        {
            const string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug(LogEventLevel.Debug, outputTemplate)
                .MinimumLevel.Debug()
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            {
                var e = (Exception)args.ExceptionObject;
                Log.Fatal(e, "Domain unhandled exception");
            };
        }
    }
}