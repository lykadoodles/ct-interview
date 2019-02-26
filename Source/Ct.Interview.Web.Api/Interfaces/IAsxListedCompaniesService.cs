using System.Threading.Tasks;

namespace Ct.Interview.Web.Api.Interfaces
{
    public interface IAsxListedCompaniesService
    {
        Task<AsxListedCompany> GetByAsxCode(string asxCode);
    }

}
