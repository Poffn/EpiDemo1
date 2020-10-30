using EPiServer;
using EPiServer.Core.Internal;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using EpiTest.Business.RetailPageImporter.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebGrease.Css.Ast.Selectors;

namespace EpiTest.Models.Pages
{
    [SiteContentType(
           GUID = "6bad1a67-0a96-4632-8c05-1125745e138e")]
    [AvailableContentTypes(Include = new[] { typeof(RetailPage) })]
    public class RetailContainerPage : StandardPage
    {
        Injected<IContentLoader> ContentLoader;


        [Ignore]
        public IEnumerable<RetailPage> RetailPages => ContentLoader.Service.GetChildren<RetailPage>(this.ContentLink);
            }
        }
    }
}