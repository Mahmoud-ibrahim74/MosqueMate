using System.Threading.Tasks;

namespace MosqueMateServices.Interfaces
{
    public interface IFileHelper
    {
        public string resourcePath { get; set; }
        public void WriteLocalJson(string content,string path);
        public Task<bool> LoadFiles();
        public string ReadResourcesFile(byte[] content);
    }
}
