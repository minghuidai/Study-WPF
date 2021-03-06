﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Study.BingMap.Query
{
    public static class Helper
    {


        public static XmlDocument GetXmlResponse(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return (xmlDoc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }



        public static GeoDataSchema.Response GetJsonResponse(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                //if (response.StatusCode != HttpStatusCode.OK)
                //{
                //    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));                
                //}

                var receiveStream = response.GetResponseStream();

                var jsonSerializer = new DataContractJsonSerializer(typeof(GeoDataSchema.Response));

                //object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                GeoDataSchema.Response objResponse = (GeoDataSchema.Response)jsonSerializer.ReadObject(receiveStream);

                //BoundryQueryResponseDataContract jsonResponse = objResponse as BoundryQueryResponseDataContract;
                return objResponse;

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// Generate the spatial filter string from polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static string GetSpatialFilterString(MapPolygon polygon)
        {
            string filterFormat = @"spatialFilter=intersects('POLYGON (({0}))')";
            //string filterFormat = @"spatialFilter='POLYGON({0})";

            //spatialFilter=intersects('POLYGON ((-112 42,-112 41,-123 41,-123 42,-112 42))')
            string prefix = "";
            var sb = new StringBuilder();

            foreach (var loc in polygon.Locations)
            {
                sb.Append(prefix);
                prefix = ",";
                sb.Append(string.Format("{0} {1}", loc.Latitude, loc.Longitude));
            }

            return string.Format(filterFormat, sb.ToString());
        }




        /// <summary>
        /// Display each "entry" in the Bing Spatial Data Services Atom (XML) response.
        /// </summary>
        /// <param name="entryElements"></param>
        public static List<string> ProcessEntityElements(XmlDocument response)
        {
            var result = new List<string>();

            XmlNodeList entryElements = response.GetElementsByTagName("entry");
            for (int i = 0; i <= entryElements.Count - 1; i++)
            {
                XmlElement element = (XmlElement)entryElements[i];
                XmlElement contentElement = (XmlElement)element.GetElementsByTagName("content")[0];
                XmlElement propElement = (XmlElement)
                  contentElement.GetElementsByTagName("m:properties")[0];
                XmlNode nameElement = propElement.GetElementsByTagName("d:Name")[0];
                if (nameElement == null)
                    throw new Exception("Name not found");

                XmlNode latElement = propElement.GetElementsByTagName("d:Latitude")[0];
                if (latElement == null)
                    throw new Exception("Latitude not found");

                XmlNode longElement = propElement.GetElementsByTagName("d:Longitude")
                  [0];
                if (longElement == null)
                    throw new Exception("Longitude not found");

                string name = nameElement.InnerText;
                double latitude = 0;
                Double.TryParse(latElement.InnerText, out latitude);
                double longitude = 0;
                Double.TryParse(longElement.InnerText, out longitude);

                result.Add(string.Format("Coordinates of '{0}': {1}, {2}", name, latitude, longitude));
            }

            return result;
        }




        /// <summary>
        /// Parse the compressed shape value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parsedValue"></param>
        /// <returns></returns>
        public static bool TryParseEncodedValue(string value, out LocationCollection parsedValue)
        {
            

            string safeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-";
            parsedValue = null; 
            var list = new LocationCollection();
            int index = 0;
            int xsum = 0, ysum = 0;
            while (index < value.Length)
            {
                long n = 0;
                int k = 0;
                while (true)
                {
                    if (index >= value.Length)
                        return false;
                    int b = safeCharacters.IndexOf(value[index++]);
                    if (b == -1)
                        return false;
                    n |= ((long)b & 31) << k;
                    k += 5;
                    if (b < 32) break;
                }
                int diagonal = (int)((Math.Sqrt(8 * n + 5) - 1) / 2);
                n -= diagonal * (diagonal + 1L) / 2;
                int ny = (int)n;
                int nx = diagonal - ny;
                nx = (nx >> 1) ^ -(nx & 1);
                ny = (ny >> 1) ^ -(ny & 1);
                xsum += nx;
                ysum += ny;
                double lat = ysum * 0.00001;
                double lon = xsum * 0.00001;
                //Trim latlong values to supported ranges
                lat = Math.Max(-85, Math.Min(85, lat));
                lon = Math.Max(-180, Math.Min(180, lon));
                list.Add(new Location(lat, lon));
            }
            parsedValue = list;
            return true;
        }

    }
}
