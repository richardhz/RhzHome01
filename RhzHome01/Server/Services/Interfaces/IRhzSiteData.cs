using RhzHome01.Shared;
using System.Threading.Tasks;

namespace RhzHome01.Server.Services.Interfaces
{
    public interface IRhzSiteData
    {
        Task<BasicContentViewModel> GetHeroViewModel();
        Task<BasicContentViewModel> GetAboutViewModel();
        Task<BasicContentViewModel> GetDocumentsViewModel();
        Task<BasicContentViewModel> GetDocument(string blobname);
        Task SendMessage(ContactModel message);
    }
}
