using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EpiTest.Business.Rendering;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EpiTest.Business.RetailPageImporter.Interfaces;
using EPiServer.Core;
using EPiServer;
using EpiTest.Models.Pages;
using EPiServer.Core.Internal;
using EpiTest.Business.RetailPageImporter;

namespace EpiTest.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class PublishEventInitializer : IInitializableModule
    {
        Injected<IContentLoader> ContentLoader;
        Injected<IRetailPageImporter> RetailPageImporter;
        public void Initialize(InitializationEngine context)
        {
            var events = context.Locate.ContentEvents();
            events.PublishedContent += EventsPublishedContent;
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
            var events = context.Locate.ContentEvents();
            events.PublishedContent -= EventsPublishedContent;
        }


        public void EventsPublishedContent(object sender, ContentEventArgs eventArgs)
        {
            if((eventArgs.Content as StartPage) != null)
            {
                var previousPagereference = eventArgs.ContentLink.ToReferenceWithoutVersion();
                var previousPage  = ContentLoader.Service.Get<StartPage>(previousPagereference);
                
                

                if(previousPage.RetailPageData.WorkID != (eventArgs.Content as StartPage).RetailPageData.WorkID)
                {
                    var importHandler = new RetailPageImportHandler();
                    importHandler.ImportPages(out _);

                }

            }



        }
    }
}
