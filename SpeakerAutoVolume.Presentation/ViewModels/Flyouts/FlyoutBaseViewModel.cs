// <copyright file="FlyoutBaseViewModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.ViewModels.Flyouts
{
    using Caliburn.Micro;
    using MahApps.Metro.Controls;
    using SpeakerAutoVolume.Presentation.Events;

    /// <summary>
    /// Flyout Base ViewModel.
    /// </summary>
    public abstract class FlyoutBaseViewModel : PropertyChangedBase
    {
        private string header;
        private bool isOpen;
        private Position position;
        private FlyoutTheme theme;

        /// <summary>
        /// Gets or sets Header.
        /// </summary>
        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                if (value == this.header)
                {
                    return;
                }

                this.header = value;
                this.NotifyOfPropertyChange(() => this.Header);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsOpen.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }

            set
            {
                if (value.Equals(this.isOpen))
                {
                    return;
                }

                this.isOpen = value;
                this.NotifyOfPropertyChange(() => this.IsOpen);
            }
        }

        /// <summary>
        /// Gets or sets Position.
        /// </summary>
        public Position Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (value == this.position)
                {
                    return;
                }

                this.position = value;
                this.NotifyOfPropertyChange(() => this.Position);
            }
        }

        /// <summary>
        /// Gets or sets Theme.
        /// </summary>
        public FlyoutTheme Theme
        {
            get
            {
                return this.theme;
            }

            set
            {
                if (value == this.theme)
                {
                    return;
                }

                this.theme = value;
                this.NotifyOfPropertyChange(() => this.Theme);
            }
        }

        /// <summary>
        /// Gets or sets Delete Schedule Event.
        /// </summary>
        public DeleteScheduleEvent DeleteScheduleEvent { get; set; }
    }
}