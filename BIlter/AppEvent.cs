using BIlter.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter
{
    public class AppEvent : IEventManager
    {
        private readonly IUIProvider _uIProvider;

        public AppEvent(IUIProvider uIProvider)
        {
            _uIProvider = uIProvider;
        }
        public void Subscribe()
        {
            _uIProvider.GetApplication().DocumentOpened += AppEvent_DocumentOpened;
            _uIProvider.GetApplication().DocumentClosed += AppEvent_DocumentClosed;
            _uIProvider.GetApplication().DocumentCreated += AppEvent_DocumentCreated;
        }

        public void Unsubscribe()
        {
            _uIProvider.GetApplication().DocumentOpened -= AppEvent_DocumentOpened;
            _uIProvider.GetApplication().DocumentClosed -= AppEvent_DocumentClosed;
            _uIProvider.GetApplication().DocumentCreated -= AppEvent_DocumentCreated;
        }

        private void AppEvent_DocumentCreated(object sender, Autodesk.Revit.DB.Events.DocumentCreatedEventArgs e)
        {
            /*System.Windows.MessageBox.Show("Created Project Succeeded");*/
        }

        private void AppEvent_DocumentClosed(object sender, Autodesk.Revit.DB.Events.DocumentClosedEventArgs e)
        {
            /*System.Windows.MessageBox.Show("Closed Project Succeeded");*/
        }

        private void AppEvent_DocumentOpened(object sender, Autodesk.Revit.DB.Events.DocumentOpenedEventArgs e)
        {
            /*System.Windows.MessageBox.Show("Opened Project Succeeded");*/
        }

        
    }
}
