namespace Skeleton.Core.Result
{
    public interface IResult<T>
    {
        T Data { get; set; }
        int StatusCode { get; set; }
        bool IsSuccessful { get; set; }
    }
}
