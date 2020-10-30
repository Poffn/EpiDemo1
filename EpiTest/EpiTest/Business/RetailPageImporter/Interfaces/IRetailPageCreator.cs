using EPiServer.Core;
using EpiTest.Business.RetailPageImporter.Interfaces;

namespace EpiTest.Business.RetailPageImporter.Interfaces
{
    public interface IRetailPageImporter
    {
        bool ImportPage(IRetailPageData data, IContent parent, out string Error);

    }
}