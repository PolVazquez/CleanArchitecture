namespace BusinessComponent
{
    public interface IRepository
    {
        void Add(string beer);

        string Get();
    }
}