using EpiTest.Business.RetailPageImporter.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpiTest.Models.Pages
{
    [SiteContentType(
           GUID = "c12b700b-8cb7-4ae2-b703-631a88b1f063")]
    public class RetailPage : StandardPage, IRetailPageData
    {
        [Editable(false)]
        public virtual string orgNr { get; set;}

        [Editable(false)]
        public virtual string name { get; set; }

        [Editable(false)]
        public virtual string address { get; set; }
    }
}