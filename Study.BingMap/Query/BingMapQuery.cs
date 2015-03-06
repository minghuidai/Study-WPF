using Microsoft.Maps.MapControl.WPF;
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
        public string _DataSourceID { get; set; }
        public string _DataSourceName { get; set; }
        public string _DataEntityName { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        public BingMapQuery(string key)
        {
            _BingMapKey = key;

            _DataSourceID = "f22876ec257b474b82fe2ffcb8393150";
            _DataSourceName = "NavteqNA";
            _DataEntityName = "NavteqPOIs";

            // sample query url
            //http://spatial.virtualearth.net/REST/v1/data/f22876ec257b474b82fe2ffcb8393150/NavteqNA/NavteqPOIs?spatialFilter=nearby(40.83274904439099,-74.3163299560546935,5)&$filter=EntityTypeID%20eq%20'6000'&$select=EntityID,DisplayName,Latitude,Longitude,__Distance&$top=3&key=anyBingMapsKey

        }




        // make a sample query
        public IEnumerable<LocationEntity> SampleQuery()
        {
            var xmlResponse = GetXmlResponse(@"http://spatial.virtualearth.net/REST/v1/data/f22876ec257b474b82fe2ffcb8393150/NavteqNA/NavteqPOIs?spatialFilter=nearby(40.83274904439099,-74.3163299560546935,5)&$filter=EntityTypeID%20eq%20'6000'&$select=EntityID,DisplayName,Latitude,Longitude,__Distance&$top=3&key=" + _BingMapKey);
            return GetQueryResults(xmlResponse);
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
        public XmlDocument SearchByArea(string accessID, string dataSourceName, string entityTypeName, MapPolygon polygon, string queryOption1, string queryOption2)
        {
            // URL format
            const string urlFormat = @"{0}/{1}/{2}/{3}?{4}&{5}&{6}&{7}";

            // make the query url
            string url = string.Format(urlFormat, _RootUrl, accessID, dataSourceName, entityTypeName, Helper.GetSpatialFilterString(polygon));

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






        //Search for POI near a point
        private IEnumerable<LocationEntity> GetQueryResults(XmlDocument xmlDoc)
        {
            //Get location information from geocode response 

            //Create namespace manager
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");

            //Get all locations in the response and then extract the coordinates for the top location
            XmlNodeList locationElements = xmlDoc.SelectNodes("//rest:Location", nsmgr);

            if (locationElements.Count == 0) return null;

            //Get the geocode points that are used for display (UsageType=Display)
            XmlNodeList displayGeocodePoints =
                    locationElements[0].SelectNodes(".//rest:GeocodePoint/rest:UsageType[.='Display']/parent::node()", nsmgr);


            var result = new List<LocationEntity>();

            foreach (XmlNode locElement in locationElements)
            {
                var entity = new LocationEntity();
                entity.EntityTypeID = locElement.InnerXml;

                result.Add(entity);
            }

            //string latitude = displayGeocodePoints[0].SelectSingleNode(".//rest:Latitude", nsmgr).InnerText;
            //string longitude = displayGeocodePoints[0].SelectSingleNode(".//rest:Longitude", nsmgr).InnerText;
            //ComboBoxItem entityTypeID = (ComboBoxItem)EntityType.SelectedItem;
            //ComboBoxItem distance = (ComboBoxItem)Distance.SelectedItem;

            ////Create the Bing Spatial Data Services request to get nearby POI
            //string findNearbyPOIRequest = "http://spatial.virtualearth.net/REST/v1/data/f22876ec257b474b82fe2ffcb8393150/NavteqNA/NavteqPOIs?spatialfilter=nearby("
            //+ latitude + "," + longitude + "," + distance.Content + ")"
            //+ "&$filter=EntityTypeID%20EQ%20'" + entityTypeID.Tag + "'&$select=EntityID,DisplayName,__Distance,Latitude,Longitude,AddressLine,Locality,AdminDistrict,PostalCode&$top=10"
            //+ "&key=" + Shared.BingMapKey;

            ////Submit the Bing Spatial Data Services request and retrieve the response
            //XmlDocument nearbyPOI = GetXmlResponse(findNearbyPOIRequest);

            ////Center the map at the geocoded location and display the results
            //myMap.Center = new Location(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            //myMap.ZoomLevel = 12;
            //DisplayResults(nearbyPOI);

            return result;
        }




        /// <summary>
        /// Get bountries from the address by passing a description of the place
        /// </summary>
        /// <param name="address"></param>
        /// <param name="level">CountryRegion, AdminDivision2</param>
        public LocationCollection GetBoundries(string address, string level = "AdminDivision2")
        {


            // Original 
            //string urlFormat = "http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary('GA 30024',1,'AdminDivision2',0,0,'en','US')&$format=json&key=Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";

            string urlFormat = "http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary('{0}',1,'{2}',0,0,'en','US')&$format=json&key={1}";

            string url = string.Format(urlFormat, address, _BingMapKey, level);

            GeoDataSchema.Response response = Helper.GetJsonResponse(url);

            if (response == null || response.ResultSet == null || response.ResultSet.Results.Count() == 0) return null;

            var result = response.ResultSet.Results[0];

            var primitive = result.Primitives[0];

            // parse shape value to get location collection
            LocationCollection locs;
            if (Helper.TryParseEncodedValue(primitive.Shape.Substring(2), out locs))
                return locs;
            else
                return null;

        }

    }
}
