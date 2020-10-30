using EPiServer;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.DataAbstraction;
using EPiServer.Find;
using EPiServer.Find.Blocks;
using EPiServer.Find.Framework;
using EPiServer.ServiceLocation;
using EpiTest.Business.RetailPageImporter.Interfaces;
using EpiTest.Models.Pages;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace EpiTest.Business.RetailPageImporter.Handlers
{
    public class RetailPageEpiImporter : IRetailPageImporter
    {
        Injected<IContentLoader> ContentLoader;
        Injected<IContentRepository> ContentRepository;
        public bool TryImportPage(IRetailPageData data, IContent parent, out string error)
        {
            error = string.Empty;

            if (PageExists(data, out var contentData))
            {
                if (contentData.ParentLink != parent.ContentLink)
                {
                    //EXISTS AND ON wrong place;
                    ContentRepository.Service.Move(contentData.ContentLink, parent.ContentLink);
                }
            }


            return false;
        }

        public bool TryImportPages(IEnumerable<IRetailPageData> data, IContent parent, out IEnumerable<string> errors)
        {
            errors = new List<string>();

            var EpiPages = GetEpiPages();

            var EpiPageOrgIds = EpiPages.Select(x => x.orgNr);
            var OrgIds = data.Select(x => x.orgNr);


            foreach(var page in EpiPages)
            {
                try
                {
                    if (OrgIds.Contains(page.orgNr))
                    {
                        RetailPage clone = page.CreateWritableClone() as RetailPage;
                        //Page Should exist
                        var currentData = data.First(x => x.orgNr == page.orgNr);


                        clone.name = currentData.name;
                        clone.address = currentData.address;
                        var reference = ContentRepository.Service.Save(clone, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);

                        var newPage = ContentRepository.Service.Get<RetailPage>(reference);

                        if (newPage.ParentLink != parent.ContentLink)
                        {
                            //Page on wrong place, Move it.
                            ContentRepository.Service.Move(page.ContentLink, parent.ContentLink, EPiServer.Security.AccessLevel.NoAccess, EPiServer.Security.AccessLevel.NoAccess);
                        }

                    }
                    else
                    {
                        //Missing in JOSN, page must be deleted;
                        ContentRepository.Service.Delete(page.ContentLink, true);

                    }
                }
                catch (Exception ex)
                {
                    errors.Append(ex.Message);
                }
            }

            foreach(var page in data.Where(x=>!EpiPageOrgIds.Contains(x.orgNr)))
            {
                try
                {
                    var newPage = ContentRepository.Service.GetDefault<RetailPage>(parent.ContentLink);
                    newPage.Name = $"{page.name} {page.orgNr}";
                    newPage.name = page.name;
                    newPage.orgNr = page.orgNr;
                    newPage.address = page.address;

                    ContentRepository.Service.Save(newPage, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);
                }
                catch (Exception ex)
                {

                    errors.Append(ex.Message);
                }

            }

            return (errors.Count() > 0);

        }

        private SearchResults<RetailPage> GetEpiPages()
        {
            return SearchClient.Instance.Search<RetailPage>().GetResult();
            
        }

        private bool PageExists(IRetailPageData data, out IContent page) {

            var result = SearchClient.Instance.Search<RetailPage>().For(data.orgNr).InField(x=>x.orgNr).GetResult();
            var match = result.FirstOrDefault(x => x.orgNr == data.orgNr);

            if(match != null)
            {
                page = match;
                return true;
            }
            page = null;
            return false;

        }


    }
}