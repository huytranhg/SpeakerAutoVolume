// <copyright file="ShellView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation
{
    using System;
    using System.Windows;
    using SpeakerAutoVolume.Presentation.Properties;

    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private bool balloonTipShowed = false;
        private Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellView"/> class.
        /// </summary>
        public ShellView()
        {
            this.InitializeComponent();

            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.Icon = Properties.Resources.Icon;
            this.notifyIcon.Click += (object sender, EventArgs args) =>
            {
                this.ShowInTaskbar = true;
                this.Show();
                this.WindowState = WindowState.Maximized;
                this.notifyIcon.Visible = false;
            };

            this.settings = new Settings();
            if (this.settings.StartWithWindows)
            {
                this.ShowInTaskbar = false;

                // Set this manually only in case SpeakerAutoVolume starts with Windows.
                this.balloonTipShowed = true;
                this.MinimizeToTray();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.notifyIcon != null)
            {
                this.notifyIcon.Dispose();
                this.notifyIcon = null;
            }
        }

        /// <summary>
        /// Minimize to Tray
        /// </summary>
        public void MinimizeToTray()
        {
            this.Hide();

            if (this.notifyIcon != null)
            {
                this.notifyIcon.Visible = true;
                this.notifyIcon.BalloonTipText = "Speaker Auto Volume was minimized to tray.";

                if (!this.balloonTipShowed)
                {
                    this.notifyIcon.ShowBalloonTip(1000);
                    this.balloonTipShowed = true;
                }
            }
        }

        /// <summary>
        /// On State Change
        /// </summary>
        /// <param name="e"> Event Arguments)</param>
        protected override void OnStateChanged(EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.MinimizeToTray();
            }

            base.OnStateChanged(e);
        }
    }
}