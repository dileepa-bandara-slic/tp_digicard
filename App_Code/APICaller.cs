using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for APICaller
/// </summary>
public class APICaller
{
    public async Task<int> PostDoc(DocumentEntryDTO Entry)
    {
        bool returnObj = false;
        log lg = new log();
        try
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(Entry);
            HttpContent requestContent = new StringContent(json);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            HttpResponseMessage response = new HttpResponseMessage();

            //Live
            response = await client.PostAsync("http://192.168.101.20:10455/DocumentShareAPI/api/Document/RecordDocument", requestContent);
            //Test
            //response = await client.PostAsync("http://192.168.101.21:10155/DocumentShareAPI/api/Document/RecordDocument", requestContent);
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var json_str = await response.Content.ReadAsStringAsync();
                //returnObj = Convert.ToInt32(ret.Result.ToString());
                returnObj = JsonConvert.DeserializeObject<bool>(json_str);
            }
        }
        catch (Exception e)
        {
            lg.write_log("PostBulkDoc " + e.Message);
            Console.WriteLine(e);
        }

        return returnObj?1:0;

    }
}