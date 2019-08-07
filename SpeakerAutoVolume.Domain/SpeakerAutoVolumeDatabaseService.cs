// <copyright file="SpeakerAutoVolumeDatabaseService.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    using System.Collections.Generic;
    using AutoMapper;
    using SpeakerAutoVolume.Domain.DTOs;
    using SpeakerAutoVolume.Domain.Interfaces;

    /// <summary>
    /// SpeakerAutoVolumeDatabaseService object to help get data transfer object from database, and map to domain's model.
    /// </summary>
    public class SpeakerAutoVolumeDatabaseService
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ISpeakerAutoVolumeDatabaseAccess speakerAutoVolumeDatabaseAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerAutoVolumeDatabaseService"/> class.
        /// </summary>
        /// <param name="speakerAutoVolumeDatabaseAccess"> ISpeakerAutoVolumeDatabaseAccess implementation.</param>
        public SpeakerAutoVolumeDatabaseService(ISpeakerAutoVolumeDatabaseAccess speakerAutoVolumeDatabaseAccess)
        {
            this.speakerAutoVolumeDatabaseAccess = speakerAutoVolumeDatabaseAccess;
            SpeakerAutoVolumeAutomapper.Initialize();
        }

        /// <summary>
        /// Select all schedules.
        /// </summary>
        /// <returns>List of schedule.</returns>
        public List<ScheduleModel> SelectAllSchedules()
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return null;
            }

            var output = this.speakerAutoVolumeDatabaseAccess.SelectAllSchedules();
            List<ScheduleModel> listSchedules = Mapper.Map<List<ScheduleDto>, List<ScheduleModel>>(output);
            return listSchedules;
        }

        /// <summary>
        /// Select all schedule details.
        /// </summary>
        /// <returns>List of schedule detail.</returns>
        public List<ScheduleDetailModel> SelectAllScheduleDetails()
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return null;
            }

            var output = this.speakerAutoVolumeDatabaseAccess.SelectAllScheduleDetails();
            List<ScheduleDetailModel> listScheduleDetails = Mapper.Map<List<ScheduleDetailDto>, List<ScheduleDetailModel>>(output);
            return listScheduleDetails;
        }

        /// <summary>
        /// Save schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be saved.</param>
        /// <returns>Affected rows.</returns>
        public int InsertSchedule(ScheduleModel schedule)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return 0;
            }

            if (!this.PrecheckScheduleExisted(schedule))
            {
                return 0;
            }
            else
            {
                ScheduleDto scheduleDto = Mapper.Map<ScheduleModel, ScheduleDto>(schedule);
                var output = this.speakerAutoVolumeDatabaseAccess.InsertSchedule(scheduleDto);
                return output;
            }
        }

        /// <summary>
        /// Save schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be saved.</param>
        /// <returns>Affected rows.</returns>
        public int InsertScheduleDetail(ScheduleDetailModel scheduleDetail)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return 0;
            }

            if (!this.PrecheckScheduleDetailExisted(scheduleDetail))
            {
                return 0;
            }
            else
            {
                ScheduleDetailDto scheduleDetailDto = Mapper.Map<ScheduleDetailModel, ScheduleDetailDto>(scheduleDetail);
                var output = this.speakerAutoVolumeDatabaseAccess.InsertScheduleDetail(scheduleDetailDto);
                return output;
            }
        }

        /// <summary>
        /// Select if a schedule detail exists.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be checked.</param>
        /// <returns>List of interger, if row exist return list of 1 element and value is 1.</returns>
        public List<int> SelectScheduleDetailExist(ScheduleDetailModel scheduleDetail)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return null;
            }

            if (!this.PrecheckScheduleDetailExisted(scheduleDetail))
            {
                return null;
            }
            else
            {
                ScheduleDetailDto scheduleDetailDto = Mapper.Map<ScheduleDetailModel, ScheduleDetailDto>(scheduleDetail);
                var output = this.speakerAutoVolumeDatabaseAccess.SelectScheduleDetailExist(scheduleDetailDto);
                return output;
            }
        }

        /// <summary>
        /// Delete Schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be deleted.</param>
        /// <returns>Affected rows.</returns>
        public int DeleteSchedule(ScheduleModel schedule)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return 0;
            }

            if (!this.PrecheckScheduleExisted(schedule))
            {
                return 0;
            }
            else
            {
                ScheduleDto scheduleDto = Mapper.Map<ScheduleModel, ScheduleDto>(schedule);
                var output = this.speakerAutoVolumeDatabaseAccess.DeleteSchedule(scheduleDto);
                return output;
            }
        }

        /// <summary>
        /// Delete a schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be deleted.</param>
        /// <returns>List of affected row.</returns>
        public int DeleteScheduleDetail(ScheduleDetailModel scheduleDetail)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return 0;
            }

            if (!this.PrecheckScheduleDetailExisted(scheduleDetail))
            {
                return 0;
            }
            else
            {
                ScheduleDetailDto scheduleDetailDto = Mapper.Map<ScheduleDetailModel, ScheduleDetailDto>(scheduleDetail);
                var output = this.speakerAutoVolumeDatabaseAccess.DeleteScheduleDetail(scheduleDetailDto);
                return output;
            }
        }

        /// <summary>
        /// Delete all schedule details based on schedule name.
        /// </summary>
        /// <param name="schedule"> Schedule details to be deleted.</param>
        /// <returns>List of affected row.</returns>
        public int DeleteScheduleDetails(ScheduleModel schedule)
        {
            if (!this.PrecheckInterfaceExisted())
            {
                return 0;
            }

            if (!this.PrecheckScheduleExisted(schedule))
            {
                return 0;
            }
            else
            {
                ScheduleDto scheduleDto = Mapper.Map<ScheduleModel, ScheduleDto>(schedule);
                var output = this.speakerAutoVolumeDatabaseAccess.DeleteScheduleDetails(scheduleDto);
                return output;
            }
        }

        private bool PrecheckInterfaceExisted()
        {
            if (this.speakerAutoVolumeDatabaseAccess == null)
            {
                NLogger.Info("SpeakerAutoVolumeDatabaseAccess is null.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool PrecheckScheduleExisted(ScheduleModel schedule)
        {
            if (schedule == null)
            {
                NLogger.Info("ScheduleModel is null.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool PrecheckScheduleDetailExisted(ScheduleDetailModel scheduleDetail)
        {
            if (scheduleDetail == null)
            {
                NLogger.Info("ScheduleDetailModel is null.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
