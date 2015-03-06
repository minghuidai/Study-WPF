using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Study.BingMap.Query;
using System.Diagnostics;

namespace Study.WPF.UnitTests.BingMapQueries
{
    [TestClass]
    public class Test_SpatialQuery
    {

        private string _BingMapKey = "Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";


        [TestMethod]
        public void TestMethod1()
        {
            var ddd = new SpatialService(_BingMapKey);

            //var result1 = ddd.ExampleFindByAreaRadius();
            //var result2 = ddd.ExampleFindByProperty();
            //var result = ddd.ExampleFindByBoundingBox();
            //var result4 = ddd.ExampleQueryByIdAtom();

            //var result = ddd.ReverseGeoCode(33.75550, -84.3900);
            var result = ddd.GetBoundries();

            Assert.IsTrue(result.Count > 0);
            //Assert.IsTrue(result2.Count > 0);
            //Assert.IsTrue(result3.Count > 0);
            //Assert.IsTrue(result4 != null);
            
        }



        [TestMethod]
        public void Test_JsonParser()
        {

            string QueryUrl1 = "http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary('King County',1,'AdminDivision2',0,0,'en','US')&$format=json&key=Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";

            GeoDataSchema.Response response = Helper.GetJsonResponse(QueryUrl1);
            //BoundryQueryResponseDataContract response = Helper.GetJsonResponse(@"G:\Temp\json data.txt");
            

            Assert.IsTrue(response.ResultSet != null);

        }


    }
}
