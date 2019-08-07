// <copyright file="Bootstrapper.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Windows;
    using Caliburn.Micro;
    using SpeakerAutoVolume.Presentation.Controls;

    /// <summary>
    /// Bootstrapper class.
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            this.Initialize();
        }

        /// <summary>
        /// On Startup.
        /// </summary>
        /// <param name="sender"> Sender object.</param>
        /// <param name="e"> Startup event arguments.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var startupTasks =
                this.GetAllInstances(typeof(StartupTask))
                .Cast<ExportedDelegate>()
                .Select(exportedDelegate => (StartupTask)exportedDelegate.CreateDelegate(typeof(StartupTask)));

            startupTasks.Apply(s => s());

            this.DisplayRootViewFor<IShell>();
        }

        /// <summary>
        /// Build Up.
        /// </summary>
        /// <param name="instance"> Instance.</param>
        protected override void BuildUp(object instance)
        {
            this.container.SatisfyImportsOnce(instance);
        }

        /// <summary>
        /// By default, we are configured to use MEF.
        /// </summary>
        protected override void Configure()
        {
            var catalog =
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());

            this.container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this.container);
            batch.AddExportedValue(catalog);

            this.container.Compose(batch);
        }

        /// <summary>
        /// Get All Instances.
        /// </summary>
        /// <param name="serviceType"> Service type.</param>
        /// <returns> IEnumerable of objects.</returns>
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        /// <summary>
        /// Get Instance.
        /// </summary>
        /// <param name="serviceType"> Service type.</param>
        /// <param name="key"> Key.</param>
        /// <returns> Object.</returns>
        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = this.container.GetExportedValues<object>(contract);

            if (exports.Any())
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }
    }
}