namespace Blog.Interfaces.IRepository
{
    public interface IPostRepository
    {
        bool IsExist(string title);
    }
}