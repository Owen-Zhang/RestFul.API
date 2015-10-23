using RestFul.API.Extensions;
namespace RestFul.API.Host
{
    public partial class Runtime
    {
        public bool ApplyRequestFilters(AspNetRequest req, AspNetResponse res, object requestDto)
        {
            req.ThrowIfNull("Request");
            req.ThrowIfNull("Response");

            if (res.IsClosed)
                return true;

            return ApplyRequestFiltersSingle(req, res, requestDto);
        }

        private bool ApplyRequestFiltersSingle(AspNetRequest req, AspNetResponse res, object requestDto)
        {
            return true;
        }
    }
}
