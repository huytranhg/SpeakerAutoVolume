// <copyright file="SpeakerAutoVolumeTimer.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using SpeakerAutoVolume.Domain;
    using SpeakerAutoVolume.Domain.Interfaces;

    /// <summary>
    /// Class SystemTimer.
    /// </summary>
    public class SpeakerAutoVolumeTimer : ISpeakerAutoVolumeTimer, IDisposable
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly Timer systemTimer;
        private List<ScheduleDetailModel> listScheduleDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerAutoVolumeTimer"/> class.
        /// </summary>
        /// <param name="timer"> Instance of Timer.</param>
        public SpeakerAutoVolumeTimer(Timer timer)
        {
            try
            {
                this.systemTimer = timer;

                if (this.PrecheckTimerExisted())
                {
                    this.systemTimer.Elapsed += this.TimerElapsed;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while creating SpeakerAutoVolumeTimer: {ex.Message}.");
            }
        }

        /// <summary>
        /// Gets Activated Schedule Detail.
        /// </summary>
        public ScheduleDetailModel ActivatedScheduleDetailModel { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Stubborn Mode is on or off.
        /// </summary>
        public bool StubbornMode { get; set; }

        /// <summary>
        /// Timer starts.
        /// </summary>
        /// <param name="listScheduleDetail"> List schedule detail.</param>
        public void Start(List<ScheduleDetailModel> listScheduleDetail)
        {
            if (this.PrecheckTimerExisted())
            {
                if (listScheduleDetail != null && listScheduleDetail.Count > 0)
                {
                    this.listScheduleDetail = listScheduleDetail;
                    this.systemTimer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Timer stops.
        /// </summary>
        public void Stop()
        {
            if (this.PrecheckTimerExisted())
            {
                this.systemTimer.Enabled = false;
            }
        }

         /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
            if (this.systemTimer != null)
            {
                this.systemTimer.Dispose();
            }
        }

        private void TimerElapsed(object source, ElapsedEventArgs e)
        {
            this.SpeakerChangeVolume();
        }

        private bool IsBetween(TimeSpan fromTime, TimeSpan toTime)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            return fromTime <= now && now <= toTime;
        }

        private void SpeakerChangeVolume()
        {
            try
            {
                if (this.listScheduleDetail != null && this.listScheduleDetail.Count > 0)
                {
                    TimeSpan fromTime;
                    TimeSpan toTime;
                    byte speakerVolume = 0;
                    bool scheduleFound = false;

                    foreach (ScheduleDetailModel runningScheduleDetailModel in this.listScheduleDetail)
                    {
                        fromTime = new TimeSpan((int)runningScheduleDetailModel.StartHour, (int)runningScheduleDetailModel.StartMinute, 0);
                        toTime = new TimeSpan((int)runningScheduleDetailModel.EndHour, (int)runningScheduleDetailModel.EndMinute, 0);

                        if (this.IsBetween(fromTime, toTime))
                        {
                            MMDeviceEnumeratorFactory.GetSpeakerVolume(ref speakerVolume);

                            if (speakerVolume != runningScheduleDetailModel.Volume)
                            {
                                if (this.ActivatedScheduleDetailModel != runningScheduleDetailModel)
                                {
                                    MMDeviceEnumeratorFactory.SetSpeakerVolume(runningScheduleDetailModel.Volume);
                                    this.ActivatedScheduleDetailModel = runningScheduleDetailModel;
                                }
                                else
                                {
                                    if (this.StubbornMode)
                                    {
                                        MMDeviceEnumeratorFactory.SetSpeakerVolume(runningScheduleDetailModel.Volume);
                                        this.ActivatedScheduleDetailModel = runningScheduleDetailModel;
                                    }
                                }
                            }
                            else
                            {
                                this.ActivatedScheduleDetailModel = runningScheduleDetailModel;
                            }

                            scheduleFound = true;
                        }
                    }

                    if (!scheduleFound)
                    {
                        this.ActivatedScheduleDetailModel = null;
                    }
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to change volume: {ex.Message}.");
            }
        }

        private bool PrecheckTimerExisted()
        {
            if (this.systemTimer == null)
            {
                NLogger.Info("Timer is null.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
