using System.Threading.Tasks;

namespace GenericConstraints
{
    public interface IHttpClientService
    {
        Task<TResponseModel> GetRequestAsync<TResponseModel>(string requestUrl)
            where TResponseModel : class;
    }
}