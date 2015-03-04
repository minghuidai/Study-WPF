using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Study.BingMap.Query
{
    public class SpatialService
    {
        string _BingMapsKey;
        string _DataSourceID = "20181f26d9e94c81acdf9496133d4f23";
        string _DataSourceName = "FourthCoffeeSample";
        string _DataEntityName = "FourthCoffeeShops";


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bingMapkey"></param>
        public SpatialService(string bingMapskey)
        {
            _BingMapsKey = bingMapskey;

            _DataSourceID = "f22876ec257b474b82fe2ffcb8393150";
            _DataSourceName = "NavteqNA";
            _DataEntityName = "NavteqPOIs";


        }



        #region Query By Area
        public List<string> ExampleFindByAreaRadius()
        {
            Console.WriteLine("\nExampleFindByAreaRadius");

            // Find all previously uploaded MyShops entities located within
            // a certain radius around a point.
            // Custom name of spatial data source created during upload
            //string dataSourceName = "FourthCoffeeSample";
            // Name of entities in the data source
            //string dataEntityName = "FourthCoffeeShops";
            // Unique access ID assigned to your data source by Bing Maps
            // e.g. f8986xxxxxxxc844b
            //string accessId = _DataSourceID;
            // Your Bing Maps Spatial Data Services query key.
            //string bingMapsKey = _BingMapsKey;
            // Coordinates of the point to search from.
            double SearchLatitude = 47.63674;
            double SearchLongitude = -122.30413;
            // Search radius
            double Radius = 3; // km
            // Setup REST request to query our uploaded customer data
            string requestUrl = string.Format("http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}" +
              "?spatialFilter=nearby({3},{4},{5})&key={6}", _DataSourceID, _DataSourceName, _DataEntityName, SearchLatitude, SearchLongitude, Radius, _BingMapsKey);
            // Send the request and get back an XML response.
            XmlDocument response = Helper.GetXmlResponse(requestUrl);
            // Display each entity's info.

            return Helper.ProcessEntityElements(response);
        }
        #endregion 



        #region Query by Property
        public List<string> ExampleFindByProperty()
        {
            //Console.WriteLine("\nExampleFindByProperty");
            // Find all previously uploaded MyShops entities that accept
            // online orders.
            // Custom name of spatial data source created during upload
            //string dataSourceName = "FourthCoffeeSample";
            // Name of entities in the data source
            //string dataEntityName = "FourthCoffeeShops";
            // Unique access ID assigned to your data source by Bing Maps
            // e.g. f8986xxxxxxxc844b
            //string accessId = _DataSourceID;
            // Your Bing Maps Spatial Data Services query key.
            //string bingMapsKey = _BingMapsKey;
            // Setup REST request to query our uploaded customer data
            string requestUrl = String.Format(
              "http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}" +
              "?$filter=AcceptsOnlineOrders Eq True&key={3}", _DataSourceID, _DataSourceName, _DataEntityName, _BingMapsKey);
            // Send the request and get back an XML response.
            XmlDocument response = Helper.GetXmlResponse(requestUrl);
            // Display each entity's info.
            return Helper.ProcessEntityElements(response);
        }
        #endregion 




        #region Query by Bounding Box
        public List<string> ExampleFindByBoundingBox()
        {
            Console.WriteLine("\nExampleFindByBoundingBox");
            // Find all previously uploaded MyShops entities located within
            // the specified bounding box.
            // Custom name of spatial data source created during upload
            //string dataSourceName = "FourthCoffeeSample";
            // Name of entities in the data source
            //string dataEntityName = "FourthCoffeeShops";
            // Coordinates of the bounding box's corners
            double lat1 = 47.61247675940658;
            double long1 = -122.3237670214032;
            double lat2 = 47.68239156080077;
            double long2 = -122.27996173131822;
            // Setup REST request to query our uploaded customer data
            string requestUrl = String.Format(
              "http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}" +
              "?spatialFilter=bbox({3},{4},{5},{6})&key={7}", _DataSourceID, _DataSourceName, _DataEntityName, lat1, long1, lat2, long2, _BingMapsKey);
            // Send the request and get back an XML response.
            XmlDocument response = Helper.GetXmlResponse(requestUrl);
            // Display each entity's info.
            return Helper.ProcessEntityElements(response);
        }
        #endregion 




        #region Query by Bounding Box
        public List<string> ExampleFindByPolygon()
        {

            // add locations into the polygon
            var locCollection = new LocationCollection();
            //locCollection.Add(new Location(47.6424, -122.3219));
            //locCollection.Add(new Location(47.8424, -122.1747));
            //locCollection.Add(new Location(47.5814, -122.1747));
            locCollection.Add(new Location(-112, 42));
            locCollection.Add(new Location(-112, 41));
            locCollection.Add(new Location(-123, 41));
            locCollection.Add(new Location(-123, 42));
            locCollection.Add(new Location(-112, 42));

            // define a polycy
            var p = new MapPolygon()
            {
                Locations = locCollection,
            };
            var filterString = Helper.GetSpatialFilterString(p);

            // Setup REST request to query our uploaded customer data
            string requestUrl = String.Format(
              "http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}" +
              "?{3}&key={4}", _DataSourceID, _DataSourceName, _DataEntityName, filterString, _BingMapsKey);
            // Send the request and get back an XML response.
            XmlDocument response = Helper.GetXmlResponse(requestUrl);
            // Display each entity's info.
            return Helper.ProcessEntityElements(response);
        }
        #endregion 




        #region Query By ID
        /// <summary>
        /// Query by ID using ATOM protocol
        /// </summary>
        public string ExampleQueryByIdAtom()
        {
            //Console.WriteLine("\nExampleQueryByIdAtom");
            // http://spatial.virtualearth.net/REST/v1/data/accessId/dataSourceName/entityTypeName('entityId')?key=queryKey
            // http://spatial.virtualearth.net/REST/v1/data/f8986xxxxxxxc844b/MyShopsSample/MyShops('1')?key=queryKey
            // Custom name of spatial data source created during upload
            //string dataSourceName = "FourthCoffeeSample";
            // Name of entities in the data source
            //string dataEntityName = "FourthCoffeeShops";
            // ID of the entity to search for
            int entityId = -22067;

            string requestUrl = string.Format("http://spatial.virtualearth.net" +
              "/REST/v1/data/{0}/{1}/{2}('{3}')?key={4}", _DataSourceID, _DataSourceName, _DataEntityName, entityId, _BingMapsKey);
            // By default, the Spatial Data API returns
            // data responses in Atom (xml) format.
            XmlDocument xmlResponse = Helper.GetXmlResponse(requestUrl);
            // Select the first shop data in the xml results.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlResponse.NameTable);
            nsmgr.AddNamespace("a", "http://www.w3.org/2005/Atom");
            nsmgr.AddNamespace("m",
              "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
            nsmgr.AddNamespace("d",
              "http://schemas.microsoft.com/ado/2007/08/dataservices");
            XmlNode firstShopNode = xmlResponse.SelectSingleNode(
              "//a:entry/a:content/m:properties", nsmgr);
            // Extract result data from the xml nodes.
            int retrievedEntityId = int.Parse(firstShopNode.SelectSingleNode(
              "d:EntityID", nsmgr).FirstChild.Value);
            string postalCode = firstShopNode.SelectSingleNode(
              "d:PostalCode", nsmgr).FirstChild.Value;
            double latitude = double.Parse(firstShopNode.SelectSingleNode(
              "d:Latitude", nsmgr).FirstChild.Value);
            double longitude = double.Parse(firstShopNode.SelectSingleNode(
              "d:Longitude", nsmgr).FirstChild.Value);

            return string.Format("Found EntityID {0} Postal Code: {1} Lat,Lon:  ({2},{3})", retrievedEntityId, postalCode, latitude, longitude);

        }

        #endregion;


        #region "Reverse Geo coding"

        // Geocode an address and return a latitude and longitude
        public List<string> ReverseGeoCode(double latitude, double longtitude)
        {
            //Create REST Services geocode request using Locations API
            string urlFormat = @"http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=json&key={2}";

            string url = string.Format(urlFormat, latitude, longtitude, _BingMapsKey);


            //Make the request and get the response
            XmlDocument geocodeResponse = Helper.GetXmlResponse(url);

            var result = Helper.ProcessEntityElements(geocodeResponse);

            return result;
        }


        #endregion



    }
}
