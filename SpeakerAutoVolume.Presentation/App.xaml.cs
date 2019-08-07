// <copyright file="App.xaml.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using SpeakerAutoVolume.Persistence;
    using SpeakerAutoVolume.Persistence.Interfaces;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string Unique = "8D479CB1-52CB-4017-ADAF-79EFE7252839";
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Settings speakerAutoVolumeSettings;

        private App()
        {
            this.speakerAutoVolumeSettings = new Settings();

            if (this.speakerAutoVolumeSettings.CultureInfo == string.Empty)
            {
                this.speakerAutoVolumeSettings.CultureInfo = Thread.CurrentThread.CurrentCulture.ToString();

                if (this.speakerAutoVolumeSettings.CultureInfo != "vi-VN")
                {
                    this.speakerAutoVolumeSettings.CultureInfo = "en-US";
                }

                this.speakerAutoVolumeSettings.Save();
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(this.speakerAutoVolumeSettings.CultureInfo);
            this.speakerAutoVolumeSettings = null;
        }

        /// <summary>
        /// Main function.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
                {
                    var application = new App();

                    application.InitializeComponent();
                    application.Run();

                    // Allow single instance code to perform cleanup operations
                    SingleInstance<App>.Cleanup();
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while SpeakerAutoVolume is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Signal External Command Line Arguments.
        /// </summary>
        /// <param name="args"> Arguments.</param>
        /// <returns> Boolean value.</returns>
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }
    }
}
