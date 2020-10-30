using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EpiTest.Business.RetailPageImporter.Interfaces;
using EpiTest.Models.Media;
using EpiTest.Models.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EpiTest.Business.RetailPageImporter.Handlers
{
    public class StartPageReferenceRetailPageContainerLocator : IRetailPageContainerLocator
    {
        Injected<IContentLoader> ContentLoader;

        public IContent GetContainerPage()
        {
            var startpage = ContentLoader.Service.Get<StartPage>(ContentReference.StartPage); 
            return startpage.RetailContainerPage as IContent;
        }
    }
}