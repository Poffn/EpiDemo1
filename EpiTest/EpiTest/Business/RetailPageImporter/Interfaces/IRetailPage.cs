using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiTest.Business.RetailPageImporter.Interfaces
{
    public interface IRetailPageData
    {
        string orgNr { get; set; }
        string name { get; set; }
        string address { get; set; }
    }
}