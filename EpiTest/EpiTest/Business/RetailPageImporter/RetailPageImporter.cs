using EPiServer.Core;
using EPiServer.ServiceLocation;
using EpiTest.Business.RetailPageImporter.Interfaces;
using EpiTest.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiTest.Business.RetailPageImporter
{
    public class RetailPageImportHandler
    {
        Injected<IRetailPageService> RetailPageService;
        Injected<IRetailPageImporter> RetailPageImporter;
        Injected<IRetailPageContainerLocator> RetailPageContainerLocator;

        public bool ImportPages(out IEnumerable<string> errors )
        {
            var errorList = new List<string>();

            IContent containerPage = RetailPageContainerLocator.Service.GetContainerPage();
            IEnumerable<IRetailPageData> pages = RetailPageService.Service.GetRetailPageData();

            RetailPageImporter.Service.TryImportPages(pages, containerPage, out errors);
                        
            return (errorList.Count <= 0);
        }
    }
}