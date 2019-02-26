using System;
using System.IO;
using System.Threading.Tasks;
using Ct.Interview.Web.Api.Interfaces;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Ct.Interview.Web.Api.Services
{
    public class GetListedCompanies : IAsxListedCompaniesService
    {

        public async Task<AsxListedCompany> GetByAsxCode(string asxCode)
        {
            var uri = System.Environment.GetEnvironmentVariable("ListedSecuritiesCsvUrl", EnvironmentVariableTarget.Process);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();


            StreamReader sr = new StreamReader(resp.GetResponseStream());

            AsxListedCompany asxCompany = new AsxListedCompany();

            for (int i = 1; i > 0; i++)
            {
                int skip = 3;
                if (i > skip)
                {
                    string companyLine = sr.ReadLine();
                    if (companyLine == null)
                    {
                        break;
                    }
                    string companyName = Regex.Unescape(companyLine.Split(',')[0]).Replace("\"", "");
                    string asx = Regex.Unescape(companyLine.Split(',')[1]).Replace("\"", "");
                    string gics = Regex.Unescape(companyLine.Split(',')[2]).Replace("\"", "");

                    asxCompany.CompanyName = companyName;
                    asxCompany.AsxCode = asx;
                    asxCompany.GicsIndustryGroup = gics;

                    if (asx.Equals(asxCode))
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(asxCompany));
                        return await Task.FromResult(asxCompany);
                    }
                }
                else
                {
                    //skip line
                    sr.ReadLine();
                }
            }
            sr.Close();
            return await Task.FromResult(asxCompany);
        }
    }
}
