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
    public class RetailPageFileService : IRetailPageService
    {
        Injected<IContentLoader> ContentLoader;
        public IEnumerable<IRetailPageData> GetRetailPageData()
        {

            var startpage = (EPiServer.Core.ContentReference.StartPage as IContent);
            var fileReference = (startpage as StartPage)?.RetailPageData;
            var jsonFile = ContentLoader.Service.Get<JsonFile>(fileReference);

            IEnumerable<IRetailPageData> retailPageData;

            using (var stream = jsonFile.BinaryData.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    using(var jsonReader = new JsonTextReader(reader))
                    {
                        var serializer = new JsonSerializer();
                        retailPageData = serializer.Deserialize<IEnumerable<IRetailPageData>>(jsonReader);
                    }
                }
            }
            return retailPageData;

        }
    }
}