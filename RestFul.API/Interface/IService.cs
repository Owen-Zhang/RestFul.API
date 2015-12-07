namespace RestFul.API.Interface
{
    public interface IService<T>
    {
        object Execute(T request);
    }
}
