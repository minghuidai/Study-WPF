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



        /// <summary>
        /// Get boundries from address
        /// </summary>
        /// <returns></returns>
        public List<string> GetBoundries()
        {

            // original url copied from bing website
            //  http://platform.bing.com/geo/spatial/v1/public/geodata?SpatialFilter=GetBoundary(address,levelOfDetail,entityType,getAllPolygons,getEntityMetadata,culture,userRegion)&$format=responseFormat&key=BingMapsKey


            string urlFormat = @"http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary({0}')&$format={1}&key={2}";

            //string address = "'8480 River Walk Landing, Johns Creek GA 30024'";
            string address = "'King  County',1,'AdminDivision2',0,0,'en','US'";
            
            string outFormat = "json";

            string url = string.Format(urlFormat, address, outFormat, _BingMapsKey);

            var response = Helper.GetXmlResponse(url);

            var result = Helper.ProcessEntityElements(response);

            return result;

        }



        //private void TileXYXoomToQuadkey(string tile)
        //{
        //    string quakKey;

            

        //    for (int i = 5; i > 0;  )

        //}



    }
}






//{"d":
//    {"Copyright":"\u00a9 2015 Microsoft and its suppliers.  This API and any results cannot be used or accessed without Microsoft's express written permission.",
//     "results":
//        [{"__metadata": {"uri":"https:\/\/platform.bing.com\/geo\/spatial\/v1\/public\/Geodata?$filter=GetEntity(10037780L,1,0,'en','US')"},

//          "EntityID":"10037780",
//          "Name":
//                {"EntityName":"King",
//                 "Culture":"en",
//                 "SourceID":"2"},
//          "Primitives":
//                [{"PrimitiveID":"-7889550576",
//                 "Shape":"1,ph11p3pzkQ0x11Lsoy9B72neixg4D5guqBhgiUl30Ep1qNyq2D-oiEwl2D1p4V64jxD8lxOuimrCzugc2l7Bs97E39pbt6yhFikjSnzqY7g4O_14diutQ_-_e219xBgyjmEs0jelk2nC-_hrB3rjEsq60Ch21Vy5nzB9kkkBg_6elu3rFswjqBlsvgB1x4Gj64CnsivEww1rBx5j6E_q99El4wIo5uao2zLzgqnDp-lcy4uiEmw4O72_qBx02zDp_zLx-wxG-xoiDp0hJ-u7Q386P44gGqq7UtjiQwwklCs11lCm8vZ5myhBltjRvh_Vv5uVovi5CgvmlBxv_Ol84Ihqm7BsvlGzy3I3xgfktxUlxggBgw5No0uFk8sBkyjHvwokBr1za9uikBvtvT2qq4ExjiuCritQo-5Nv_sxCtzgwBq-iOrg3K20sYvnoS113qBk1lI8i_qLntysE3vuxHx5qjBpt8Nuy6pB2861Co1wD-60YnjyD4wovBz1zGgxjJ06mO_oiDk0rfo-zkCm6wS7vpHg5xT5uran46W24gzClgseuznYjxlpCnkmpBj98H0kqvBvx6bhz7I20hOiguU5k0UvsiyB1t0J1s6mB9j38B7x9nHt8xhB3uxoBgnkE5q_jB3i_Tzh9F_8xgNgtqhDqtsN_xkjD2r0L_9rI8qmyBg2loD1nulBzhvJ5v5Hv85Hw7s8Cn_hyBpjy2BnxwCqmoYsnxPxu7couyM1j8C0u9S3gwOgi6L1k5oBjqnN2wt4F2t3ExolDyntYkr5E3i0Hwk6Qgv16B3y7GixuE0ktDowsHwniEjwptCor_Bgj5E9v-O6_1U243Nn4jKz6usBu96lF77lmD2vzlD0uswBpq_4Bsm8Ey84PxxuGvvjPsz5O33xaz90L_pnH45hN7ziT-t22Mlqk_Cms8uB0vyzJvk7iB0hmG-_9FsrwN7trE1wxfzon-C5i8G8u2xBkngIgyxjC1yucthlhB5j2c1om8CswlIz4u_B5-hG60zalr75Bs74uC-kkI7z5Ivh__B8ymX0v9lCirjY51xHjh-sFi3jjBj26Fj31Dptw9Cs93G_nw_CniisBpulrB306nCprkoBhp8yBmzgJk1wErl5VqlyiBpp2hBhx_kCn9ljFukkT244Mt_vG8wzjBnsiI885kB_qwD1s4vGji9Lkx4vwD8mUsszo2oB-h6Csygvamiw24xC45rprKx9sr43Dw3_rM0txvHy-rLzqwqBv9-yF4llwIi-hiFh17jBhk-2QmwgqBvghkDih8gMi0ugBk4l0Ds94vGtpx9Fzmj0U3-pyO18htDpvpFvlmoHwyptCkgluDnlq9Czn70B7gkhB47imCz_w2zBjj7hE24_qFlx9hNi29qD-yh7Kzh_gBgwrayyy4F5lqsB2--yQk5wwB7h6Mx2zoEkixsJoni-D0ll-Ev5hrI346oEs0m1Kvs9nIk3v14R_l1V7ulx6VpjiE1h8c96mLvn3GygrhB09xnF__1F9hpU83yBw5_b7koO8l9Fw54MmonXmpvkBovtrB16lkB5m9Iuth2Bq-tLmk_G9q1rBkxoJxmyFo9xnCzpvvB-4ggHzjoepquqBjsvrBwu-xB-2l_Bmp1uBt0gpB-9i_DzovNl9uXqvyIghrE9_owBmz0nD5ozyCl21lElqwPoliXmjlHuvoiD6swaq5mbytvlCvxrkBrjkkB2j7HmhtP2oyFns2E9nqyC2h6iCn2q0Clo1xHs16e4h2hBplsan78MkrnJomuKwijH1oiWg8jcu8qRy09pB5o9jDts2Hs8-Vsxq1D67ihBw17jB7p0Vl5jMgupa7hjQu79sHthqJ_soSn5hS8rxE6n2qBqolrHk4ioB_tjxBxxmwD6s1asm5gB4ojgCh06gFq7gzDku0vD75gmB7xq2BvsmxBm03lCl3gO8jt8B8vohBrutFx4sB0z0yBrtzC77vP636Cym6EullV1s0Zt71MlwsE4p6sBnv2vB4nlFzxoK41i6C1u2Ngvn5Bl_iGtslrC4_tjD4lu1Evylcg_joB6m0oCp9uL6igKtsvvBtvvV-rrIt7nNpghKgq8Evyg1BxivN9ppD16yeksuD3p8gBlzoTxgykBpkhqBvnpfzi3Sl_7xBi-zM",
//                 "NumPoints":"478",
//                 "SourceID":"1"}
//                ],
//          "Copyright":{
//                        "CopyrightURL":"http:\/\/windows.microsoft.com\/en-us\/windows-live\/about-bing-data-suppliers",
//                        "Sources":[{"SourceID":"1",
//                                    "SourceName":"NVT",
//                                    "Copyright":"\u00a9 2013 Nokia"}
//                                  ]
//                      }
//         }]
//    }
//}