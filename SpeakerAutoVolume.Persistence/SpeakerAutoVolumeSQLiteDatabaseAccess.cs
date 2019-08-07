// <copyright file="SpeakerAutoVolumeSQLiteDatabaseAccess.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;
    using Dapper;
    using SpeakerAutoVolume.Domain.DTOs;
    using SpeakerAutoVolume.Domain.Interfaces;

    /// <summary>
    /// Implementation of SpeakerAutoVolumeSQLiteDatabaseAccess.
    /// </summary>
    public class SpeakerAutoVolumeSQLiteDatabaseAccess : ISpeakerAutoVolumeDatabaseAccess, IDisposable
    {
        private static readonly NLog.Logger NLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly SQLiteConnection sQLiteConnection;
        private string sqliteCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerAutoVolumeSQLiteDatabaseAccess"/> class.
        /// </summary>
        /// <param name="sQLiteConnection"> Instance of SQLiteConnection.</param>
        public SpeakerAutoVolumeSQLiteDatabaseAccess(SQLiteConnection sQLiteConnection)
        {
            this.sQLiteConnection = sQLiteConnection;
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
        }

        /// <summary>
        /// Select all schedules.
        /// </summary>
        /// <returns>List of schedule.</returns>
        public List<ScheduleDto> SelectAllSchedules()
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    var output = this.sQLiteConnection.Query<ScheduleDto>(Properties.Resources.SelectAllSchedule, new DynamicParameters());

                    NLogger.Info($"Executed SQL: {Properties.Resources.SelectAllSchedule}");
                    NLogger.Info($"Output: {output}");

                    return output.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return null;
            }
        }

        /// <summary>
        /// Select all schedule details.
        /// </summary>
        /// <returns>List of schedule detail.</returns>
        public List<ScheduleDetailDto> SelectAllScheduleDetails()
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    var output = this.sQLiteConnection.Query<ScheduleDetailDto>(Properties.Resources.SelectAllScheduleDetail, new DynamicParameters());

                    NLogger.Info($"Executed SQL: {Properties.Resources.SelectAllScheduleDetail}");

                    return output.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return null;
            }
        }

        /// <summary>
        /// Save schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be saved.</param>
        /// <returns>Affected rows.</returns>
        public int InsertSchedule(ScheduleDto schedule)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(Properties.Resources.InsertSchedule, schedule.Name);
                    var output = this.sQLiteConnection.Execute(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output;
                }
                else
                {
                    return Constants.ErrorCode;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        /// <summary>
        /// Save schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be saved.</param>
        /// <returns>Affected rows.</returns>
        public int InsertScheduleDetail(ScheduleDetailDto scheduleDetail)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(
                        Properties.Resources.InsertScheduleDetail,
                        scheduleDetail.StartHour,
                        scheduleDetail.StartMinute,
                        scheduleDetail.EndHour,
                        scheduleDetail.EndMinute,
                        scheduleDetail.Volume,
                        scheduleDetail.ScheduleName);
                    var output = this.sQLiteConnection.Execute(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output;
                }
                else
                {
                    return Constants.ErrorCode;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        /// <summary>
        /// Select if a schedule detail exists.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be checked.</param>
        /// <returns>List of interger, if row exist return list of 1 element and value is 1.</returns>
        public List<int> SelectScheduleDetailExist(ScheduleDetailDto scheduleDetail)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(
                       Properties.Resources.SelectScheduleDetailExist,
                       scheduleDetail.ScheduleName,
                       scheduleDetail.StartHour,
                       scheduleDetail.StartMinute,
                       scheduleDetail.EndHour,
                       scheduleDetail.EndMinute,
                       scheduleDetail.Volume);
                    var output = this.sQLiteConnection.Query<int>(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return null;
            }
        }

        /// <summary>
        /// Delete Schedule.
        /// </summary>
        /// <param name="schedule"> Schedule to be deleted.</param>
        /// <returns>Affected rows.</returns>
        public int DeleteSchedule(ScheduleDto schedule)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(Properties.Resources.DeleteSchedule, schedule.Name);
                    var output = this.sQLiteConnection.Execute(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output;
                }
                else
                {
                    return Constants.ErrorCode;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        /// <summary>
        /// Delete a schedule detail.
        /// </summary>
        /// <param name="scheduleDetail"> Schedule detail to be deleted.</param>
        /// <returns>List of affected row.</returns>
        public int DeleteScheduleDetail(ScheduleDetailDto scheduleDetail)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(
                       Properties.Resources.DeleteScheduleDetail,
                       scheduleDetail.ScheduleName,
                       scheduleDetail.StartHour,
                       scheduleDetail.StartMinute,
                       scheduleDetail.EndHour,
                       scheduleDetail.EndMinute,
                       scheduleDetail.Volume);
                    var output = this.sQLiteConnection.Execute(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output;
                }
                else
                {
                    return Constants.ErrorCode;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        /// <summary>
        /// Delete all schedule details based on schedule name.
        /// </summary>
        /// <param name="schedule"> Schedule details to be deleted.</param>
        /// <returns>List of affected row.</returns>
        public int DeleteScheduleDetails(ScheduleDto schedule)
        {
            try
            {
                if (this.PrecheckSQLiteConnectionExisted())
                {
                    this.sqliteCommand = string.Format(
                    Properties.Resources.DeleteScheduleDetails,
                    schedule.Name);

                    var output = this.sQLiteConnection.Execute(this.sqliteCommand);

                    NLogger.Info($"Executed SQL: {this.sqliteCommand}");

                    return output;
                }
                else
                {
                    return Constants.ErrorCode;
                }
            }
            catch (Exception ex)
            {
                NLogger.Error($"Error while trying to execute SQL: {ex.Message}.");
                return Constants.ErrorCode;
            }
        }

        private bool PrecheckSQLiteConnectionExisted()
        {
            if (this.sQLiteConnection == null)
            {
                NLogger.Info("SQLiteConnection is null.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
