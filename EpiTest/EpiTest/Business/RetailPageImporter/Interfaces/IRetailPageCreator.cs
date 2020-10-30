using EPiServer.Core;
using EpiTest.Business.RetailPageImporter.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace EpiTest.Business.RetailPageImporter.Interfaces
{
    public interface IRetailPageImporter
    {
        bool TryImportPage(IRetailPageData data, IContent parent, out string error);
        bool TryImportPages(IEnumerable<IRetailPageData> data, IContent parent, out IEnumerable<string> error);

    }
}