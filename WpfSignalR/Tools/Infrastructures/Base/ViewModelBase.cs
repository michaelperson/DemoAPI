using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSignalR.Tools.Infrastructures.Base
{
    public abstract class ViewModelBase : BindableBase
    {
        public ViewModelBase()
        {
            this.Container = Bootstrapper.serviceProvider.GetRequiredService<IContainerProvider>();
            this.RegionManager = Bootstrapper.serviceProvider.GetRequiredService<IRegionManager>();
            this.EventAggregator = Bootstrapper.serviceProvider.GetRequiredService <IEventAggregator >();
        }

        #region Properties

        private IContainerProvider _container;

        /// <summary>
        /// The container
        /// </summary>
        public IContainerProvider Container
        {
            get { return _container; }
            private set { this.SetProperty<IContainerProvider>(ref this._container, value); }
        }

        private IRegionManager regionManager;

        /// <summary>
        /// The region manager
        /// </summary>
        public IRegionManager RegionManager
        {
            get { return regionManager; }
            private set { this.SetProperty<IRegionManager>(ref this.regionManager, value); }
        }

        private IEventAggregator eventAggregator;

        /// <summary>
        /// The EventAggregator
        /// </summary>
        public IEventAggregator EventAggregator
        {
            get { return eventAggregator; }
            private set { this.SetProperty<IEventAggregator>(ref this.eventAggregator, value); }
        }

        #endregion Properties
    }
}
