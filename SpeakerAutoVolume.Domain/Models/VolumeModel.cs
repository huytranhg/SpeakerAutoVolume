// <copyright file="VolumeModel.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    /// <summary>
    /// Class Volume.
    /// </summary>
    public class VolumeModel
    {
        /// <summary>
        /// Gets or sets SpeakerVolume.
        /// </summary>
        public int SpeakerVolume { get; set; }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return this.SpeakerVolume.ToString();
        }
    }
}