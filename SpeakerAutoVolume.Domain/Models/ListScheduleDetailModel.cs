// <copyright file="ListScheduleDetail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    using Caliburn.Micro;

    /// <summary>
    /// Class ListScheduleDetail.
    /// </summary>
    public class ListScheduleDetailModel : BindableCollection<ScheduleDetailModel>
    {
        /// <summary>
        /// Sort for items in ListScheduleDetail.
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < (this.Count - 1); i++)
            {
                for (int j = i + 1; j < this.Count; j++)
                {
                    if (int.Parse(
                        this.Items[i].StartHour.ToString("00")
                        + this.Items[i].StartMinute.ToString("00")) >
                        int.Parse(
                        this.Items[j].StartHour.ToString("00")
                        + this.Items[j].StartMinute.ToString("00")))
                    {
                        this.MoveItem(j, i);
                    }
                }
            }
        }
    }
}