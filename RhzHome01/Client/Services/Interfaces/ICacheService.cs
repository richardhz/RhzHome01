namespace RhzHome01.Client.Services.Interfaces
{
    public interface ICacheService
    {
        void Add<T>(string key, T item, int maxAge = 60) where T : class;
        T Get<T>(string key) where T : class;
        bool Peek(string key);
    }
}