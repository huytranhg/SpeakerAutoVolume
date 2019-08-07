// <copyright file="SpeakerAutoVolumeAutomapper.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Domain
{
    using AutoMapper;
    using SpeakerAutoVolume.Domain.DTOs;

    /// <summary>
    /// Static class SpeakerAutoVolumeAutomapper that initializes config for Automapper to map objects.
    /// </summary>
    public static class SpeakerAutoVolumeAutomapper
    {
        /// <summary>
        /// Initializes config for Automapper to map objects.
        /// </summary>
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ScheduleDetailDto, ScheduleDetailModel>();
                cfg.CreateMap<ScheduleDto, ScheduleModel>();
            });
        }
    }
}
