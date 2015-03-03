using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Study.BingMap.Query
{


    public class BingMapQuery
    {

        private const string _RootUrl = @"http://spatial.virtualearth.net/REST/v1/data";
        private string _BingMapKey;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        public BingMapQuery(string key)
        {
            _BingMapKey = key;
        }




        /// <summary>
        /// Query bing maps by area
        /// </summary>
        /// <param name="accessID"></param>
        /// <param name="dataSourceName"></param>
        /// <param name="entityTypeName"></param>
        /// <param name="spatialFilter"></param>
        /// <param name="queryOption1"></param>
        /// <param name="queryOption2"></param>
        /// <returns></returns>
        public XmlDocument SearchByArea(string accessID, string dataSourceName, string entityTypeName, string spatialFilter, string queryOption1, string queryOption2)
        {
            // make the query url
            string url = @"{0}/{1}/{2}/{3}?spatialFilter={4}&{5}&{6}&{7}";

            return GetXmlResponse(url);
        }







        // Geocode an address and return a latitude and longitude
        public XmlDocument Geocode(string addressQuery)
        {
            //Create REST Services geocode request using Locations API
            string geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations/" + addressQuery + "?o=xml&key=" + _BingMapKey;


            //Make the request and get the response
            XmlDocument geocodeResponse = GetXmlResponse(geocodeRequest);

            return (geocodeResponse);
        }



        // Submit a REST Services or Spatial Data Services request and return the response
        private XmlDocument GetXmlResponse(string requestUrl)
        {
            System.Diagnostics.Trace.WriteLine("Request URL (XML): " + requestUrl);
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return xmlDoc;
            }
        }

    }
}
