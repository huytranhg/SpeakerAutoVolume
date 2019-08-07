
// <copyright file="ShellViewModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using System.Timers;
    using System.Windows;
    using Caliburn.Micro;
    using SpeakerAutoVolume.Domain;
    using SpeakerAutoVolume.Persistence;
    using SpeakerAutoVolume.Presentation.Events;
    using SpeakerAutoVolume.Presentation.ViewModels.Flyouts;

    /// <summary>
    /// Class ShellViewModel, data model for ShellView window.
    /// </summary>
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IDisposable, IShell, IHandle<DeleteScheduleEvent>
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string ProgramName = "SpeakerAutoVolume";
        private readonly IEventAggregator eventAggregator;
        private readonly Timer presentationTimer;
        private readonly SQLiteConnection sQLiteConnection;
        private readonly SpeakerAutoVolumeSQLiteDatabaseAccess speakerAutoVolumeSQLite;
        private readonly SpeakerAutoVolumeDatabaseService speakerAutoVolumeDatabaseService;
        private readonly Settings speakerAutoVolumeSettings;
        private readonly Timer timer;

        private WindowState windowState;
        private DeleteScheduleEvent deleteScheduleEvent;
        private SpeakerAutoVolumeTimer speakerAutoVolumeTimer;

        private bool isCheckedToggleSwitch;
        private bool isCheckedStartsWithWindows;
        private bool isCheckedStubbornMode;

        private BindableCollection<ScheduleModel> listSchedule;
        private ScheduleModel selectedListSchedule;
        private BindableCollection<ScheduleDetailModel> listScheduleDetail;
        private ScheduleDetailModel selectedScheduleDetail;
        private int selectedFromTime;
        private int selectedToTime;
        private string textError;
        private ScheduleModel tempSchedule;
        private ScheduleDetailModel tempScheduleDetail;
        private List<ScheduleDetailModel> tempListScheduleDetailModel;

        private bool isEnabledToggleSwitch;
        private bool visibilityLabel;
        private bool visibilityButtonAddSchedule;
        private bool visibilityButtonDeleteSchedule;
        private bool visibilityButtonScheduleDetail;
        private bool visibilityButtonSave;
        private bool visibilityTextBoxError;
        private bool isEnabledScheduleName;
        private bool isEnableTimeRangeControls;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event Aggregator.</param>
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.FlyoutViewModels = new BindableCollection<FlyoutBaseViewModel>();
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            this.presentationTimer = new Timer(Persistence.Constants.TimerInterval);
            this.presentationTimer.Elapsed += this.TimerElapsed;
            this.timer = new Timer(Persistence.Constants.TimerInterval);
            this.sQLiteConnection = new SQLiteConnection(SpeakerAutoVolumeSQLiteConnection.LoadConnectionString());
            this.speakerAutoVolumeSQLite = new SpeakerAutoVolumeSQLiteDatabaseAccess(this.sQLiteConnection);
            this.speakerAutoVolumeDatabaseService = new SpeakerAutoVolumeDatabaseService(this.speakerAutoVolumeSQLite);
            this.speakerAutoVolumeSettings = new Settings();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window state is maximized or minimized.
        /// </summary>
        public WindowState WindowState
        {
            get
            {
                return this.windowState;
            }

            set
            {
                this.windowState = value;
                this.NotifyOfPropertyChange(() => this.WindowState);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether toggle switch is on.
        /// </summary>
        public bool IsCheckedToggleSwitch
        {
            get
            {
                return this.isCheckedToggleSwitch;
            }

            set
            {
                this.isCheckedToggleSwitch = value;
                this.NotifyOfPropertyChange(() => this.IsCheckedToggleSwitch);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Starts with Windows option is on.
        /// </summary>
        public bool IsCheckedStartsWithWindows
        {
            get
            {
                return this.isCheckedStartsWithWindows;
            }

            set
            {
                this.isCheckedStartsWithWindows = value;
                this.NotifyOfPropertyChange(() => this.IsCheckedStartsWithWindows);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether stubborn mode is on.
        /// </summary>
        public bool IsCheckedStubbornMode
        {
            get
            {
                return this.isCheckedStubbornMode;
            }

            set
            {
                this.isCheckedStubbornMode = value;
                this.NotifyOfPropertyChange(() => this.IsCheckedStubbornMode);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is switch visible.
        /// </summary>
        public bool IsEnabledToggleSwitch
        {
            get
            {
                return this.isEnabledToggleSwitch;
            }

            set
            {
                this.isEnabledToggleSwitch = value;
                this.NotifyOfPropertyChange(() => this.IsEnabledToggleSwitch);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether label schedule name is visible.
        /// </summary>
        public bool VisibilityLabel
        {
            get
            {
                return this.visibilityLabel;
            }

            set
            {
                this.visibilityLabel = value;
                this.NotifyOfPropertyChange(() => this.VisibilityLabel);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether add schedule button is visible.
        /// </summary>
        public bool VisibilityButtonAddSchedule
        {
            get
            {
                return this.visibilityButtonAddSchedule;
            }

            set
            {
                this.visibilityButtonAddSchedule = value;
                this.NotifyOfPropertyChange(() => this.VisibilityButtonAddSchedule);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether delete schedule button is visible.
        /// </summary>
        public bool VisibilityButtonDeleteSchedule
        {
            get
            {
                return this.visibilityButtonDeleteSchedule;
            }

            set
            {
                this.visibilityButtonDeleteSchedule = value;
                this.NotifyOfPropertyChange(() => this.VisibilityButtonDeleteSchedule);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether schedule detail button is visible.
        /// </summary>
        public bool VisibilityButtonScheduleDetail
        {
            get
            {
                return this.visibilityButtonScheduleDetail;
            }

            set
            {
                this.visibilityButtonScheduleDetail = value;
                this.NotifyOfPropertyChange(() => this.VisibilityButtonScheduleDetail);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether save button is visible.
        /// </summary>
        public bool VisibilityButtonSave
        {
            get
            {
                return this.visibilityButtonSave;
            }

            set
            {
                this.visibilityButtonSave = value;
                this.NotifyOfPropertyChange(() => this.VisibilityButtonSave);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether error text box is visible.
        /// </summary>
        public bool VisibilityTextBoxError
        {
            get
            {
                return this.visibilityTextBoxError;
            }

            set
            {
                this.visibilityTextBoxError = value;
                this.NotifyOfPropertyChange(() => this.VisibilityTextBoxError);
            }
        }

        /// <summary>
        /// Gets or sets text for error.
        /// </summary>
        public string TextError
        {
            get
            {
                return this.textError;
            }

            set
            {
                this.textError = value;
                this.NotifyOfPropertyChange(() => this.TextError);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether schedule name list box is enable.
        /// </summary>
        public bool IsEnabledScheduleName
        {
            get
            {
                return this.isEnabledScheduleName;
            }

            set
            {
                this.isEnabledScheduleName = value;
                this.NotifyOfPropertyChange(() => this.IsEnabledScheduleName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether time controls are visible.
        /// </summary>
        public bool IsEnableTimeRangeControls
        {
            get
            {
                return this.isEnableTimeRangeControls;
            }

            set
            {
                this.isEnableTimeRangeControls = value;
                this.NotifyOfPropertyChange(() => this.IsEnableTimeRangeControls);
            }
        }

        /// <summary>
        /// Gets or sets list hour.
        /// </summary>
        public List<HourModel> ListHour { get; set; }

        /// <summary>
        /// Gets or sets list minute.
        /// </summary>
        public List<MinuteModel> ListMinute { get; set; }

        /// <summary>
        /// Gets or sets list volume.
        /// </summary>
        public List<VolumeModel> ListVolume { get; set; }

        /// <summary>
        /// Gets or sets Selected Hour.
        /// </summary>
        public HourModel SelectedFromHour { get; set; }

        /// <summary>
        /// Gets or sets Selected Minute.
        /// </summary>
        public MinuteModel SelectedFromMinute { get; set; }

        /// <summary>
        /// Gets or sets Selected Hour.
        /// </summary>
        public HourModel SelectedToHour { get; set; }

        /// <summary>
        /// Gets or sets Selected Minute.
        /// </summary>
        public MinuteModel SelectedToMinute { get; set; }

        /// <summary>
        /// Gets or sets Selected Volume.
        /// </summary>
        public VolumeModel SelectedVolume { get; set; }

        /// <summary>
        /// Gets or sets List Schedule.
        /// </summary>
        public BindableCollection<ScheduleModel> ListSchedule
        {
            get
            {
                return this.listSchedule;
            }

            set
            {
                this.listSchedule = value;
                this.NotifyOfPropertyChange(() => this.ListSchedule);
            }
        }

        /// <summary>
        /// Gets or sets Selected Schedule.
        /// </summary>
        public ScheduleModel SelectedSchedule
        {
            get
            {
                return this.selectedListSchedule;
            }

            set
            {
                this.selectedListSchedule = value;
                this.NotifyOfPropertyChange(() => this.SelectedSchedule);
            }
        }

        /// <summary>
        /// Gets or sets Selected List Schedule Detail.
        /// </summary>
        public BindableCollection<ScheduleDetailModel> ListScheduleDetail
        {
            get
            {
                return this.listScheduleDetail;
            }

            set
            {
                this.listScheduleDetail = value;
                this.NotifyOfPropertyChange(() => this.ListScheduleDetail);
            }
        }

        /// <summary>
        /// Gets or sets Selected Schedule Detail.
        /// </summary>
        public ScheduleDetailModel SelectedScheduleDetail
        {
            get
            {
                return this.selectedScheduleDetail;
            }

            set
            {
                this.selectedScheduleDetail = value;
                this.NotifyOfPropertyChange(() => this.SelectedScheduleDetail);
            }
        }

        /// <summary>
        /// Gets FlyoutViewModels.
        /// </summary>
        public IObservableCollection<FlyoutBaseViewModel> FlyoutViewModels { get; }

        /// <summary>
        /// Gets or sets Delete Schedule Event.
        /// </summary>
        public DeleteScheduleEvent DeleteScheduleEvent
        {
            get
            {
                return this.deleteScheduleEvent;
            }

            set
            {
                this.deleteScheduleEvent = value;
                this.NotifyOfPropertyChange(() => this.DeleteScheduleEvent);
            }
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
            if (this.sQLiteConnection != null)
            {
                this.sQLiteConnection.Close();
                this.sQLiteConnection.Dispose();
            }

            if (this.speakerAutoVolumeSQLite != null)
            {
                this.speakerAutoVolumeSQLite.Dispose();
            }

            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
            }

            if (this.speakerAutoVolumeTimer != null)
            {
                this.speakerAutoVolumeTimer.Stop();
                this.speakerAutoVolumeTimer.Dispose();
            }

            if (this.presentationTimer != null)
            {
                this.presentationTimer.Stop();
                this.presentationTimer.Dispose();
            }
        }

        /// <summary>
        /// Action after time range selection changed.
        /// </summary>
        public void ActionSelectionChangedTimeRange()
        {
            try
            {
                NLogger.Info($"ActionSelectionChangedTimeRange is running.");

                this.ConvertTimeRange();

                if (this.DatabaseConstraintTimeRangeWrongSequence()
                    || this.DatabaseConstraintTimeRangeOverlap()
                    || this.DatabaseConstraintTimeOverlap())
                {
                    this.GUISetVisibilityTextBoxError(true);
                    this.GUISetVisibilityButtonSave(false);
                }
                else
                {
                    this.GUISetVisibilityTextBoxError(false);
                    this.GUISetVisibilityButtonSave(true);
                }

                if (this.SelectedSchedule == null &&
                    this.ListSchedule.Count >= Constants.MaximumNumberOfSchedule)
                {
                    this.GUISetVisibilityButtonSave(false);
                }

                if (this.SelectedSchedule == null ||
                    this.ListScheduleDetail.Count == 0)
                {
                    this.GUISetEnableDisableToggleSwitch(false);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionSelectionChangedTimeRange is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action after schedule selected.
        /// </summary>
        public void ActionSelectionChangedListSchedule()
        {
            try
            {
                NLogger.Info($"ActionSelectionChangedListSchedule is running.");

                // All GUI functions.
                this.GUIClearScheduleDetail();

                if (this.SelectedSchedule != null)
                {
                    foreach (ScheduleDetailModel runningScheduleDetailModel in this.SelectedSchedule.ListScheduleDetail)
                    {
                        this.ListScheduleDetail.Add(runningScheduleDetailModel);
                    }
                }

                this.GUISortListScheduleDetail();
                this.GUISetSelectedScheduleDetail(null);
                this.GUISetVisibilityButtonDeleteSchedule(true);

                // Recheck if selected time range can be added to new schedule.
                this.ActionSelectionChangedTimeRange();
                this.GUISetVisibilityButtonScheduleDetail(false);

                if (this.ListScheduleDetail.Count > 0)
                {
                    this.GUISetEnableDisableToggleSwitch(true);
                }
                else
                {
                    this.GUISetEnableDisableToggleSwitch(false);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionSelectionChangedListSchedule is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action after schedule detail selected.
        /// </summary>
        public void ActionSelectionChangedListScheduleDetail()
        {
            try
            {
                NLogger.Info($"ActionSelectionChangedListScheduleDetail is running.");

                if (!this.IsCheckedToggleSwitch)
                {
                    this.GUISetVisibilityButtonScheduleDetail(true);
                }
                else
                {
                    this.GUISetVisibilityButtonScheduleDetail(false);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionSelectionChangedListScheduleDetail is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action affer Save button is pressed.
        /// </summary>
        public void ActionSave()
        {
            try
            {
                NLogger.Info($"ActionSave is running.");

                // GUI functions
                this.GUISetVisibilityButtonSave(false);

                if (this.SelectedSchedule == null)
                {
                    this.GUICreateNewTempSchedule();
                    this.ListSchedule.Add(this.tempSchedule);
                    this.SelectedSchedule = this.tempSchedule;
                    this.GUICreateNewTempScheduleDetail();
                    this.SelectedSchedule.ListScheduleDetail.Add(this.tempScheduleDetail);
                }
                else
                {
                    this.tempSchedule = this.SelectedSchedule;
                    this.GUICreateNewTempScheduleDetail();
                    this.SelectedSchedule.ListScheduleDetail.Add(this.tempScheduleDetail);
                }

                // Database accessing functions
                this.DatabaseSavingSelectedSchedule();

                // Clear GUI Schedule and Detail before reloading from DB
                this.GUIClearSchedule();
                this.GUIClearScheduleDetail();

                // Database reload
                this.DatabaseReload();

                // GUI functions
                this.GUISortListSchedule();
                this.GUISortListScheduleDetail();
                this.GUISetEnableDisableSchedule(true);
                this.GUISetSelectedSchedule(this.tempSchedule);
                this.GUISetSelectedScheduleDetail(this.tempScheduleDetail);
                this.GUISetVisibilityButtonScheduleDetail(true);

                if (this.SelectedSchedule.ListScheduleDetail.Count > 0)
                {
                    this.GUISetEnableDisableToggleSwitch(true);
                }
                else
                {
                    this.GUISetEnableDisableToggleSwitch(false);
                }

                if (this.ListSchedule.Count < Constants.MaximumNumberOfSchedule)
                {
                    this.GUISetVisibilityButtonAddSchedule(true);
                }
                else
                {
                    this.GUISetVisibilityButtonAddSchedule(false);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionSave is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action add schedule.
        /// </summary>
        public void ActionAddSchedule()
        {
            try
            {
                NLogger.Info($"ActionAddSchedule is running.");

                this.GUICreateNewTempSchedule();
                this.ListSchedule.Add(this.tempSchedule);
                this.SelectedSchedule = this.tempSchedule;
                this.DatabaseSavingSelectedSchedule();
                this.GUIClearSchedule();
                this.DatabaseReload();
                this.GUISortListSchedule();

                this.GUISetSelectedSchedule(this.tempSchedule);

                if (this.SelectedSchedule.ListScheduleDetail.Count > 0)
                {
                    this.GUISetEnableDisableToggleSwitch(true);
                }
                else
                {
                    this.GUISetEnableDisableToggleSwitch(false);
                }

                if (this.ListSchedule.Count < Constants.MaximumNumberOfSchedule)
                {
                    this.GUISetVisibilityButtonAddSchedule(true);
                }
                else
                {
                    this.GUISetVisibilityButtonAddSchedule(false);
                }

                this.ActionSelectionChangedTimeRange();
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionAddSchedule is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action delete schedule detail.
        /// </summary>
        public void ActionDeleteScheduleDetail()
        {
            try
            {
                NLogger.Info($"ActionDeleteScheduleDetail is running.");

                if (this.SelectedScheduleDetail != null)
                {
                    // Caching the selected schedule that contains detail to be deleted first.
                    this.tempSchedule = this.SelectedSchedule;

                    this.DatabaseDeleteScheduleDetail(this.SelectedScheduleDetail);

                    // GUI functions after deleting database's schedule detail;
                    this.GUISetVisibilityButtonScheduleDetail(false);
                    this.GUIClearSchedule();
                    this.GUIClearScheduleDetail();

                    // Database function to reload from database.
                    this.DatabaseReload();

                    // GUI functions
                    this.GUISortListSchedule();
                    this.GUISortListScheduleDetail();
                    this.GUISetSelectedSchedule(this.tempSchedule);
                    this.ActionSelectionChangedTimeRange();
                }
                else
                {
                    NLogger.Error($"Selected Schedule Detail is null.");
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionStubbornMode is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action toggle confirm delete schedule flyout.
        /// </summary>
        public void ActionToggleConfirmDeleteScheduleFlyout()
        {
            try
            {
                NLogger.Info($"ActionToggleConfirmDeleteScheduleFlyout is running.");

                if (this.SelectedSchedule != null)
                {
                    this.GUIShowViewsForDeleteConfirmation(this.SelectedSchedule);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionToggleConfirmDeleteScheduleFlyout is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action deleteSschedule.
        /// </summary>
        /// <param name="message"> Delete schedule confirmation.</param>
        public void ActionDeleteSchedule(DeleteScheduleEvent message)
        {
            try
            {
                NLogger.Info($"ActionDeleteSchedule is running.");

                // GUI function to close flyout view
                this.GUIHandleCloseFlyoutViews();

                // If confirmed then delete schedule from database
                if (message.Confirmed)
                {
                    // Database function
                    this.DatabaseHandleDeleteSchedule(message.ScheduleModel);
                }

                // GUI functions
                this.GUISetSelectedSchedule(null);
                this.GUISetSelectedScheduleDetail(null);

                // Clear GUI Schedule and Detail before reloading from DB
                this.GUIClearSchedule();
                this.GUIClearScheduleDetail();

                // Database reload
                this.DatabaseReload();

                // GUI functions
                this.GUISortListSchedule();
                this.GUISortListScheduleDetail();
                this.GUISetEnableDisableSchedule(true);
                this.GUISetVisibilityButtonAddSchedule(true);
                this.GUISetVisibilityButtonDeleteSchedule(true);
                this.GUISetVisibilityButtonScheduleDetail(true);
                this.GUISetEnableDisableToggleSwitch(true);
                this.GUISetVisibilityLabel(true);

                // Check if selected time range can be added after a schedule is deleted.
                this.ActionSelectionChangedTimeRange();

                if (this.SelectedScheduleDetail != null)
                {
                    this.GUISetVisibilityButtonScheduleDetail(true);
                }
                else
                {
                    this.GUISetVisibilityButtonScheduleDetail(false);
                }

                this.GUISetVisibilityButtonDeleteSchedule(false);

                if (this.ListSchedule.Count == 0)
                {
                    this.GUISetVisibilityButtonAddSchedule(false);
                }

                if (this.ListSchedule.Count == 0)
                {
                    this.GUISetEnableDisableToggleSwitch(false);
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionDeleteSchedule is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action switch.
        /// </summary>
        /// <param name="onOrOff"> Switch value of on or off.</param>
        public void ActionSwitch(bool onOrOff)
        {
            try
            {
                NLogger.Info($"ActionSwitch is running.");

                if (this.tempListScheduleDetailModel != null)
                {
                    this.tempListScheduleDetailModel.Clear();
                }

                if (onOrOff)
                {
                    if (this.ListScheduleDetail != null &&
                    this.ListScheduleDetail.Count > 0 &&
                    this.speakerAutoVolumeTimer != null)
                    {
                        foreach (ScheduleDetailModel runningScheduleDetailModel in this.ListScheduleDetail)
                        {
                            this.tempListScheduleDetailModel.Add(runningScheduleDetailModel);
                        }

                        this.speakerAutoVolumeTimer.Start(this.tempListScheduleDetailModel);
                        this.presentationTimer.Start();
                        this.GUISetEnableDisableSchedule(false);
                    }

                    // Saving setting Enable of SpeakerAutoVolume.
                    this.speakerAutoVolumeSettings.Enable = true;
                    this.speakerAutoVolumeSettings.ActivatedSchedule = this.SelectedSchedule.Name;
                    this.speakerAutoVolumeSettings.Save();

                    // GUI functions.
                    this.GUISetEnableDisableTimeRangeControls(false);
                    this.GUISetVisibilityButtonAddSchedule(false);
                    this.GUISetVisibilityButtonDeleteSchedule(false);
                    this.GUISetVisibilityButtonScheduleDetail(false);
                    this.GUISetVisibilityTextBoxError(false);
                    this.GUISetVisibilityButtonSave(false);
                    this.GUISetEnableDisableSchedule(false);
                }
                else
                {
                    this.GUISetEnableDisableSchedule(true);

                    // Saving setting Enable of SpeakerAutoVolume.
                    this.presentationTimer.Stop();
                    this.speakerAutoVolumeSettings.Enable = false;
                    this.speakerAutoVolumeSettings.ActivatedSchedule = string.Empty;
                    this.speakerAutoVolumeSettings.Save();

                    // GUI functions.
                    this.GUISetEnableDisableTimeRangeControls(true);
                    this.GUISetVisibilityButtonAddSchedule(true);
                    this.GUISetVisibilityButtonDeleteSchedule(true);
                    this.GUISetVisibilityButtonScheduleDetail(true);
                    this.GUISetVisibilityButtonSave(false);

                    if (this.SelectedScheduleDetail == null)
                    {
                        this.GUISetVisibilityButtonScheduleDetail(false);
                    }

                    if (this.GUICheckListScheduleFull())
                    {
                        this.GUISetVisibilityButtonAddSchedule(false);
                    }
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionSwitch is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action toggle Stubborn Mode.
        /// </summary>
        /// <param name="isChecked"> Toogle button value of on or off.</param>
        public void ActionStubbornMode(bool isChecked)
        {
            try
            {
                NLogger.Info($"ActionStubbornMode is running.");

                if (!isChecked)
                {
                    this.speakerAutoVolumeSettings.StubbornMode = true;
                    this.speakerAutoVolumeSettings.Save();
                    this.speakerAutoVolumeTimer.StubbornMode = true;
                }
                else
                {
                    this.speakerAutoVolumeSettings.StubbornMode = false;
                    this.speakerAutoVolumeSettings.Save();
                    this.speakerAutoVolumeTimer.StubbornMode = false;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionStubbornMode is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Action toggle Starts with Windows.
        /// </summary>
        /// <param name="isChecked"> Toogle button value of on or off.</param>
        public void ActionStartsWithWindows(bool isChecked)
        {
            try
            {
                NLogger.Info($"ActionStartsWithWindows is running.");

                if (!isChecked)
                {
                    this.speakerAutoVolumeSettings.StartWithWindows = true;
                    this.speakerAutoVolumeSettings.Save();
                    ShortcutStartsWithWindows.CreateStartupShortcut(
                        ProgramName,
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                        System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    this.speakerAutoVolumeSettings.StartWithWindows = false;
                    this.speakerAutoVolumeSettings.Save();
                    ShortcutStartsWithWindows.DeleteStartupShortcut(
                        ProgramName,
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup));
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while ActionStartsWithWindows is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Handle for event of delete schedule.
        /// </summary>
        /// <param name="message"> Delete schedule confirmation.</param>
        public void Handle(DeleteScheduleEvent message)
        {
            try
            {
                NLogger.Info($"Handle Delete Schedule is running.");

                this.ActionDeleteSchedule(message);
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while Handle Delete Schedule is running: {ex.Message}.");
            }
        }

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            try
            {
                if (this.PrecheckDatabaseFileExisted())
                {
                    NLogger.Info($"OnInitialize is running.");

                    // Run base function first.
                    base.OnInitialize();

                    // ShellViewModel's initialize functions.
                    this.InitializePropertiesAndVariables();
                    this.InitializeSettings();
                    this.InitializeWindowsState();

                    // ShellViewModel's initialize GUI functions.
                    this.InitializeGUIFromToTimeRangeAndVolume();
                    this.InitializeGUIFlyoutViewModels();

                    // Reload from database.
                    this.DatabaseReload();

                    // GUI functions.
                    this.GUISortListSchedule();
                    this.GUISortListScheduleDetail();

                    if (this.ListSchedule.Count > 0)
                    {
                        this.GUISetVisibilityLabel(true);
                        this.GUISetEnableDisableSchedule(true);
                    }

                    if (this.GUICheckListScheduleFull())
                    {
                        this.GUISetVisibilityButtonAddSchedule(false);
                    }

                    // GUI action in case the switch is on
                    if (this.speakerAutoVolumeSettings.Enable == true)
                    {
                        this.GUIActionToggleSwitch();
                    }

                    if (this.SelectedSchedule == null ||
                        this.ListScheduleDetail.Count == 0)
                    {
                        this.GUISetEnableDisableToggleSwitch(false);
                    }
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while OnInitialize is running: {ex.Message}.");
            }
        }

        private void InitializePropertiesAndVariables()
        {
            this.ListSchedule = new BindableCollection<ScheduleModel>();
            this.ListScheduleDetail = new BindableCollection<ScheduleDetailModel>();
            this.ListHour = new List<HourModel>();
            this.ListMinute = new List<MinuteModel>();
            this.ListVolume = new List<VolumeModel>();
            this.tempListScheduleDetailModel = new List<ScheduleDetailModel>();
            this.speakerAutoVolumeTimer = new SpeakerAutoVolumeTimer(this.timer)
            {
                StubbornMode = this.speakerAutoVolumeSettings.StubbornMode,
            };
        }

        private void InitializeSettings()
        {
            this.IsCheckedToggleSwitch = this.speakerAutoVolumeSettings.Enable;
            this.IsCheckedStartsWithWindows = this.speakerAutoVolumeSettings.StartWithWindows;
            this.IsCheckedStubbornMode = this.speakerAutoVolumeSettings.StubbornMode;
        }

        private void InitializeWindowsState()
        {
            if (this.IsCheckedStartsWithWindows)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void InitializeGUIFromToTimeRangeAndVolume()
        {
            this.CreateListHour();
            this.CreateListMinute();
            this.CreateListVolume();

            this.GUISetIsEnableTimeRangeControls(true);
        }

        private void InitializeGUIFlyoutViewModels()
        {
            this.FlyoutViewModels.Add(new FlyoutLeftViewModel(this.eventAggregator));
            this.FlyoutViewModels.Add(new FlyoutRightViewModel(this.eventAggregator));
        }

        private bool DatabaseConstraintTimeRangeWrongSequence()
        {
            if ((this.selectedFromTime < this.selectedToTime) ||
                ((this.selectedToTime == 0) && (this.selectedFromTime > 0)))
            {
                return false;
            }
            else
            {
                this.TextError = Properties.Resources.WrongTimeSequence;
                return true;
            }
        }

        private bool DatabaseConstraintTimeRangeOverlap()
        {
            if (this.ListScheduleDetail != null)
            {
                foreach (ScheduleDetailModel runningScheduleDetailModel in this.ListScheduleDetail)
                {
                    if ((int.Parse(runningScheduleDetailModel.StartHour.ToString("00")
                      + runningScheduleDetailModel.StartMinute.ToString("00")) <
                      this.selectedToTime
                       &&
                      int.Parse(runningScheduleDetailModel.EndHour.ToString("00")
                      + runningScheduleDetailModel.EndMinute.ToString("00")) >
                      this.selectedToTime) ||
                      (int.Parse(runningScheduleDetailModel.StartHour.ToString("00")
                        + runningScheduleDetailModel.StartMinute.ToString("00")) <
                        this.selectedFromTime &&
                        int.Parse(runningScheduleDetailModel.EndHour.ToString("00")
                        + runningScheduleDetailModel.EndMinute.ToString("00")) >
                        this.selectedFromTime))
                    {
                        this.TextError = Properties.Resources.TimeRangeOverlap;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DatabaseConstraintTimeOverlap()
        {
            if (this.ListScheduleDetail != null)
            {
                foreach (ScheduleDetailModel runningScheduleDetailModel in this.ListScheduleDetail)
                {
                    if ((int.Parse(runningScheduleDetailModel.StartHour.ToString("00")
                      + runningScheduleDetailModel.StartMinute.ToString("00")) >=
                      this.selectedFromTime)
                       &&
                      (int.Parse(runningScheduleDetailModel.EndHour.ToString("00")
                      + runningScheduleDetailModel.EndMinute.ToString("00")) <=
                      this.selectedToTime))
                    {
                        this.TextError = Properties.Resources.TimeOverlap;
                        return true;
                    }
                }
            }

            return false;
        }

        private void DatabaseReload()
        {
            this.DatabasePopulateListSchedule();
            this.DatabasePopulateListScheduleDetail();
        }

        private void DatabasePopulateListSchedule()
        {
            List<ScheduleModel> listSchedule = this.speakerAutoVolumeDatabaseService.SelectAllSchedules();

            foreach (ScheduleModel runningSchedule in listSchedule)
            {
                this.ListSchedule.Add(runningSchedule);
            }

            listSchedule.Clear();
            listSchedule = null;
        }

        private void DatabasePopulateListScheduleDetail()
        {
            List<ScheduleDetailModel> listScheduleDetailModel =
            this.speakerAutoVolumeDatabaseService.SelectAllScheduleDetails();

            foreach (ScheduleModel runningScheduleModel in this.ListSchedule)
            {
                foreach (ScheduleDetailModel runningScheduleDetailModel in listScheduleDetailModel)
                {
                    if (runningScheduleModel.Name == runningScheduleDetailModel.ScheduleName)
                    {
                        runningScheduleModel.ListScheduleDetail.Add(runningScheduleDetailModel);
                    }
                }
            }

            listScheduleDetailModel.Clear();
            listScheduleDetailModel = null;
        }

        private void DatabaseSavingSelectedSchedule()
        {
            if (this.SelectedSchedule != null)
            {
                this.DatebaseInsertSchedule(this.SelectedSchedule);

                foreach (ScheduleDetailModel runningScheduleDetailModel in this.SelectedSchedule.ListScheduleDetail)
                {
                    if (!this.DatebaseCheckScheduleDetailExist(runningScheduleDetailModel))
                    {
                        if (runningScheduleDetailModel.EndHour == 0 &&
                            runningScheduleDetailModel.EndMinute == 0)
                        {
                            runningScheduleDetailModel.EndHour = 24;
                        }

                        this.DatebaseInsertScheduleDetail(runningScheduleDetailModel);
                    }
                }
            }
            else
            {
                NLogger.Error($"Selected Schedule is null.");
            }
        }

        private bool DatebaseCheckScheduleDetailExist(ScheduleDetailModel scheduleDetailModel)
        {
            List<int> queryOutput = this.speakerAutoVolumeDatabaseService.SelectScheduleDetailExist(scheduleDetailModel);

            if (queryOutput.Count == 1 && queryOutput[0] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DatebaseInsertSchedule(ScheduleModel scheduleModel)
        {
            this.speakerAutoVolumeDatabaseService.InsertSchedule(scheduleModel);
        }

        private void DatebaseInsertScheduleDetail(ScheduleDetailModel scheduleDetailModel)
        {
            this.speakerAutoVolumeDatabaseService.InsertScheduleDetail(scheduleDetailModel);
        }

        private void DatabaseHandleDeleteSchedule(ScheduleModel scheduleModel)
        {
            this.speakerAutoVolumeDatabaseService.DeleteScheduleDetails(scheduleModel);
            this.speakerAutoVolumeDatabaseService.DeleteSchedule(scheduleModel);
        }

        private void DatabaseDeleteScheduleDetail(ScheduleDetailModel scheduleDetailModel)
        {
            this.speakerAutoVolumeDatabaseService.DeleteScheduleDetail(scheduleDetailModel);
        }

        private void GUISetIsEnableTimeRangeControls(bool onOrOff)
        {
            this.IsEnableTimeRangeControls = onOrOff;
        }

        private void GUISetVisibilityTextBoxError(bool onOrOff)
        {
            this.VisibilityTextBoxError = onOrOff;
        }

        private void GUISetVisibilityButtonSave(bool onOrOff)
        {
            this.VisibilityButtonSave = onOrOff;
        }

        private void GUISetEnableDisableSchedule(bool onOrOff)
        {
            this.VisibilityLabel = onOrOff;
            this.IsEnabledScheduleName = onOrOff;
        }

        private void GUISetEnableDisableTimeRangeControls(bool onOrOff)
        {
            this.IsEnableTimeRangeControls = onOrOff;
        }

        private void GUISetVisibilityButtonAddSchedule(bool onOrOff)
        {
            this.VisibilityButtonAddSchedule = onOrOff;
        }

        private void GUISetVisibilityButtonDeleteSchedule(bool onOrOff)
        {
            this.VisibilityButtonDeleteSchedule = onOrOff;
        }

        private void GUISetVisibilityButtonScheduleDetail(bool onOrOff)
        {
            this.VisibilityButtonScheduleDetail = onOrOff;
        }

        private void GUISetEnableDisableToggleSwitch(bool onOrOff)
        {
            this.IsEnabledToggleSwitch = onOrOff;
        }

        private void GUISetVisibilityLabel(bool onOrOff)
        {
            this.VisibilityLabel = onOrOff;
        }

        private void GUICreateNewTempSchedule()
        {
            if (this.ListSchedule.Count == 0)
            {
                this.tempSchedule = new ScheduleModel
                {
                    Name = Properties.Resources.Schedule + " 1",
                };
            }
            else
            {
                string scheduleName = string.Empty;
                for (int i = 1; i <= Constants.MaximumNumberOfSchedule; i++)
                {
                    scheduleName = Properties.Resources.Schedule + " " + i.ToString();

                    if (!this.GUICheckScheduleNameExisted(scheduleName))
                    {
                        this.tempSchedule = new ScheduleModel
                        {
                            Name = scheduleName,
                        };
                        break;
                    }
                }
            }
        }

        private void GUICreateNewTempScheduleDetail()
        {
            this.tempScheduleDetail = new ScheduleDetailModel
            {
                StartHour = (byte)this.SelectedFromHour.SpeakerAutoVolumeHour,
                StartMinute = (byte)this.SelectedFromMinute.SpeakerAutoVolumeMinute,
                EndHour = (byte)this.SelectedToHour.SpeakerAutoVolumeHour,
                EndMinute = (byte)this.SelectedToMinute.SpeakerAutoVolumeMinute,
                Volume = (byte)this.SelectedVolume.SpeakerVolume,
                ScheduleName = this.SelectedSchedule.Name,
            };
        }

        private void GUIClearSchedule()
        {
            if (this.ListSchedule != null)
            {
                this.ListSchedule.Clear();
            }
        }

        private void GUIClearScheduleDetail()
        {
            if (this.ListScheduleDetail != null)
            {
                this.ListScheduleDetail.Clear();
            }
        }

        private void GUISortListSchedule()
        {
            if (this.ListSchedule != null)
            {
                this.ListSchedule =
                    new BindableCollection<ScheduleModel>(this.ListSchedule.OrderBy(
                        runningSchedule => int.Parse(runningSchedule.Name.Substring(Properties.Resources.Schedule.Length))));
            }
        }

        private void GUISortListScheduleDetail()
        {
            if (this.ListScheduleDetail != null)
            {
                this.ListScheduleDetail =
                    new BindableCollection<ScheduleDetailModel>(this.ListScheduleDetail.OrderBy(
                        runningDetail => (runningDetail.StartHour.ToString("00")
                        + runningDetail.StartMinute.ToString("00"))));
            }
        }

        private void GUISetSelectedSchedule(int index)
        {
            if (this.ListSchedule != null && this.ListSchedule.Count > 0)
            {
                this.SelectedSchedule = this.ListSchedule[index];
            }
        }

        private void GUISetSelectedSchedule(ScheduleModel scheduleModel)
        {
            if (scheduleModel != null)
            {
                for (int i = 0; i < this.ListSchedule.Count; i++)
                {
                    if (this.ListSchedule[i].Name == scheduleModel.Name)
                    {
                        this.GUISetSelectedSchedule(i);
                        break;
                    }
                }
            }
        }

        private void GUISetSelectedScheduleDetail(int index)
        {
            if (this.SelectedSchedule != null && this.SelectedSchedule.ListScheduleDetail.Count > 0)
            {
                if (this.SelectedSchedule.ListScheduleDetail.Count > 0)
                {
                    this.SelectedScheduleDetail = this.SelectedSchedule.ListScheduleDetail[index];
                }
            }
        }

        private void GUISetSelectedScheduleDetail(ScheduleDetailModel scheduleDetailModel)
        {
            if (this.SelectedSchedule != null
                && this.SelectedSchedule.ListScheduleDetail.Count > 0)
            {
                if (scheduleDetailModel != null)
                {
                    for (int i = 0; i < this.SelectedSchedule.ListScheduleDetail.Count; i++)
                    {
                        if (this.SelectedSchedule.ListScheduleDetail[i].ScheduleName == scheduleDetailModel.ScheduleName &&
                            this.SelectedSchedule.ListScheduleDetail[i].StartHour == scheduleDetailModel.StartHour &&
                            this.SelectedSchedule.ListScheduleDetail[i].StartMinute == scheduleDetailModel.StartMinute &&
                            this.SelectedSchedule.ListScheduleDetail[i].EndHour == scheduleDetailModel.EndHour &&
                            this.SelectedSchedule.ListScheduleDetail[i].EndMinute == scheduleDetailModel.EndMinute)
                        {
                            this.GUISetSelectedScheduleDetail(i);
                            this.GUISetVisibilityButtonScheduleDetail(false);
                            break;
                        }
                    }
                }
                else
                {
                    this.SelectedScheduleDetail = null;
                }
            }
        }

        private bool GUICheckListScheduleFull()
        {
            if (this.ListSchedule.Count >= Constants.MaximumNumberOfSchedule)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GUICheckScheduleNameExisted(string scheduleName)
        {
            foreach (ScheduleModel runningScheduleModel in this.ListSchedule)
            {
                if (runningScheduleModel.Name == scheduleName)
                {
                    return true;
                }
            }

            return false;
        }

        private void GUIShowViewsForDeleteConfirmation(ScheduleModel scheduleModel)
        {
            if (scheduleModel != null)
            {
                this.DeleteScheduleEvent = new DeleteScheduleEvent
                {
                    ScheduleModel = scheduleModel,
                    Confirmed = false,
                };

                this.FlyoutViewModels[0].Header = Properties.Resources.DeleteMessageLeft + " " + scheduleModel.Name + "?";
                this.FlyoutViewModels[1].Header = Properties.Resources.DeleteMessageRight + " " + scheduleModel.Name + "?";
                this.FlyoutViewModels[0].DeleteScheduleEvent = this.DeleteScheduleEvent;
                this.FlyoutViewModels[1].DeleteScheduleEvent = this.DeleteScheduleEvent;

                if (this.FlyoutViewModels[0].IsOpen == false)
                {
                    this.FlyoutViewModels[0].IsOpen = true;
                    this.FlyoutViewModels[1].IsOpen = false;
                }
                else
                {
                    this.FlyoutViewModels[0].IsOpen = false;
                    this.FlyoutViewModels[1].IsOpen = true;
                }
            }
        }

        private void GUIHandleCloseFlyoutViews()
        {
            this.FlyoutViewModels[0].IsOpen = false;
            this.FlyoutViewModels[1].IsOpen = false;
        }

        private void GUIActionToggleSwitch()
        {
            bool activatedScheduleFound = false;

            foreach (ScheduleModel runningScheduleModel in this.ListSchedule)
            {
                if (runningScheduleModel.Name == this.speakerAutoVolumeSettings.ActivatedSchedule)
                {
                    this.GUISetSelectedSchedule(runningScheduleModel);
                    this.ActionSelectionChangedListSchedule();
                    this.ActionSwitch(this.speakerAutoVolumeSettings.Enable);
                    activatedScheduleFound = true;
                    break;
                }
            }

            if (!activatedScheduleFound)
            {
                this.IsCheckedToggleSwitch = false;
                this.IsCheckedStubbornMode = false;

                this.speakerAutoVolumeSettings.Enable = this.IsCheckedToggleSwitch;
                this.speakerAutoVolumeSettings.StubbornMode = this.IsCheckedStubbornMode;
                this.speakerAutoVolumeSettings.ActivatedSchedule = string.Empty;

                this.speakerAutoVolumeSettings.Save();
            }
        }

        private void CreateListHour()
        {
            for (int i = 0; i < 24; i++)
            {
                this.ListHour.Add(new HourModel { SpeakerAutoVolumeHour = i });
            }
        }

        private void CreateListMinute()
        {
            for (int i = 0; i < 60; i = i + 5)
            {
                this.ListMinute.Add(new MinuteModel { SpeakerAutoVolumeMinute = i });
            }
        }

        private void CreateListVolume()
        {
            this.ListVolume = new List<VolumeModel>();

            for (int i = 0; i <= 100; i++)
            {
                this.ListVolume.Add(new VolumeModel { SpeakerVolume = i });
            }
        }

        private void ConvertTimeRange()
        {
            if (this.SelectedFromHour != null &&
                this.SelectedFromMinute != null &&
                this.SelectedToHour != null &&
                this.SelectedToMinute != null)
            {
                if ((this.SelectedToHour.SpeakerAutoVolumeHour == 0) &&
                    (this.SelectedToMinute.SpeakerAutoVolumeMinute == 0))
                {
                    this.selectedFromTime = int.Parse(this.SelectedFromHour.SpeakerAutoVolumeHour.ToString("00")
                     + this.SelectedFromMinute.SpeakerAutoVolumeMinute.ToString("00"));
                    this.selectedToTime = int.Parse("2400");
                }
                else
                {
                    this.selectedFromTime = int.Parse(this.SelectedFromHour.SpeakerAutoVolumeHour.ToString("00")
                        + this.SelectedFromMinute.SpeakerAutoVolumeMinute.ToString("00"));
                    this.selectedToTime = int.Parse(this.SelectedToHour.SpeakerAutoVolumeHour.ToString("00")
                        + this.SelectedToMinute.SpeakerAutoVolumeMinute.ToString("00"));
                }
            }
        }

        private void TimerElapsed(object source, ElapsedEventArgs e)
        {
            if (this.speakerAutoVolumeTimer.ActivatedScheduleDetailModel != null)
            {
                this.GUISetSelectedScheduleDetail(this.speakerAutoVolumeTimer.ActivatedScheduleDetailModel);
            }
            else
            {
                this.GUISetSelectedScheduleDetail(null);
            }
        }

        private bool PrecheckDatabaseFileExisted()
        {
            bool databaseFileExisted = File.Exists("SpeakerAutoVolumeDatabase.db");

            if (!databaseFileExisted)
            {
                NLogger.Error($"SQLite database file doesn't exist.");
                return databaseFileExisted;
            }
            else
            {
                return databaseFileExisted;
            }
        }
    }
}
