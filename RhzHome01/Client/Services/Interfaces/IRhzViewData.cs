using RhzHome01.Shared;
using System.Threading.Tasks;

namespace RhzHome01.Client.Services.Interfaces
{
    public interface IRhzViewData
    {
        Task<IndexData> GetIndexViewModel();
        Task<AboutData> GetAboutViewModel();
        Task<DocumentListData> GetDocumentsViewModel();
        Task<string> GetDocument(string blobname);
        Task SendMessage(ContactModel message);
    }
}
