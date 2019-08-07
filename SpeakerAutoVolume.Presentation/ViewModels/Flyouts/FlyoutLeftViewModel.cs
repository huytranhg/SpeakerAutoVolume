// <copyright file="FlyoutLeftViewModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.ViewModels.Flyouts
{
    using Caliburn.Micro;
    using MahApps.Metro.Controls;

    /// <summary>
    /// Flyout Left ViewModel.
    /// </summary>
    public class FlyoutLeftViewModel : FlyoutBaseViewModel
    {
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutLeftViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator.</param>
        public FlyoutLeftViewModel(IEventAggregator eventAggregator)
        {
            this.Position = Position.Left;
            this.Theme = FlyoutTheme.Adapt;
            this.eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Action Confirm Delete Schedule.
        /// </summary>
        public void ActionConfirmDeleteSchedule()
        {
            this.DeleteScheduleEvent.Confirmed = true;
            this.eventAggregator.PublishOnUIThread(this.DeleteScheduleEvent);
        }
    }
}
