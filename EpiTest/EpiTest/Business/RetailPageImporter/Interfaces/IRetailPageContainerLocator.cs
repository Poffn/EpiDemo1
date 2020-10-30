using EPiServer.Core;
using EpiTest.Business.RetailPageImporter.Interfaces;
using System.Collections.Generic;

namespace EpiTest.Business.RetailPageImporter.Interfaces
{
    public interface IRetailPageContainerLocator
    {
        IContent GetContainerPage();
    }
}