using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace EpiTest.Models.Media
{
    [ContentType(GUID = "b599d856-77fa-4065-b3cf-1e5fdc832c36")]
    [MediaDescriptor(ExtensionString = "json")]
    public class JsonFile : ImageData
    {
    }
}
