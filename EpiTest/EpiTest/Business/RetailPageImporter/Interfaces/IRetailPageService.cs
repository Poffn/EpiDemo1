using EpiTest.Business.RetailPageImporter.Interfaces;
using System.Collections.Generic;

namespace EpiTest.Business.RetailPageImporter.Interfaces
{
    public interface IRetailPageService
    {
        IEnumerable<IRetailPageData> GetRetailPageData();
    }
}