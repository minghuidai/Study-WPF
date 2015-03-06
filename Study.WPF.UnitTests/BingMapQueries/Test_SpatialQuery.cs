using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Study.BingMap.Query;
using System.Diagnostics;
using Microsoft.Maps.MapControl.WPF;

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

            //string QueryUrl1 = "http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary('King County',1,'AdminDivision2',0,0,'en','US')&$format=json&key=Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";
            string QueryUrl1 = "http://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary('GA 30024',1,'AdminDivision2',0,0,'en','US')&$format=json&key=Ai6zQ5AwxFAZKY3DtRmKAPJHZVlK4h_e01jNbblWGbagsXzwH0nf5vYrTEr13kBd";

            GeoDataSchema.Response response = Helper.GetJsonResponse(QueryUrl1);                    

            Assert.IsTrue(response.ResultSet != null);
        }


        [TestMethod]
        public void Test_ParseShapeValue()
        {
            string sampleShapeValue = "ph11p3pzkQ0x11Lsoy9B72neixg4D5guqBhgiUl30Ep1qNyq2D-oiEwl2D1p4V64jxD8lxOuimrCzugc2l7Bs97E39pbt6yhFikjSnzqY7g4O_14diutQ_-_e219xBgyjmEs0jelk2nC-_hrB3rjEsq60Ch21Vy5nzB9kkkBg_6elu3rFswjqBlsvgB1x4Gj64CnsivEww1rBx5j6E_q99El4wIo5uao2zLzgqnDp-lcy4uiEmw4O72_qBx02zDp_zLx-wxG-xoiDp0hJ-u7Q386P44gGqq7UtjiQwwklCs11lCm8vZ5myhBltjRvh_Vv5uVovi5CgvmlBxv_Ol84Ihqm7BsvlGzy3I3xgfktxUlxggBgw5No0uFk8sBkyjHvwokBr1za9uikBvtvT2qq4ExjiuCritQo-5Nv_sxCtzgwBq-iOrg3K20sYvnoS113qBk1lI8i_qLntysE3vuxHx5qjBpt8Nuy6pB2861Co1wD-60YnjyD4wovBz1zGgxjJ06mO_oiDk0rfo-zkCm6wS7vpHg5xT5uran46W24gzClgseuznYjxlpCnkmpBj98H0kqvBvx6bhz7I20hOiguU5k0UvsiyB1t0J1s6mB9j38B7x9nHt8xhB3uxoBgnkE5q_jB3i_Tzh9F_8xgNgtqhDqtsN_xkjD2r0L_9rI8qmyBg2loD1nulBzhvJ5v5Hv85Hw7s8Cn_hyBpjy2BnxwCqmoYsnxPxu7couyM1j8C0u9S3gwOgi6L1k5oBjqnN2wt4F2t3ExolDyntYkr5E3i0Hwk6Qgv16B3y7GixuE0ktDowsHwniEjwptCor_Bgj5E9v-O6_1U243Nn4jKz6usBu96lF77lmD2vzlD0uswBpq_4Bsm8Ey84PxxuGvvjPsz5O33xaz90L_pnH45hN7ziT-t22Mlqk_Cms8uB0vyzJvk7iB0hmG-_9FsrwN7trE1wxfzon-C5i8G8u2xBkngIgyxjC1yucthlhB5j2c1om8CswlIz4u_B5-hG60zalr75Bs74uC-kkI7z5Ivh__B8ymX0v9lCirjY51xHjh-sFi3jjBj26Fj31Dptw9Cs93G_nw_CniisBpulrB306nCprkoBhp8yBmzgJk1wErl5VqlyiBpp2hBhx_kCn9ljFukkT244Mt_vG8wzjBnsiI885kB_qwD1s4vGji9Lkx4vwD8mUsszo2oB-h6Csygvamiw24xC45rprKx9sr43Dw3_rM0txvHy-rLzqwqBv9-yF4llwIi-hiFh17jBhk-2QmwgqBvghkDih8gMi0ugBk4l0Ds94vGtpx9Fzmj0U3-pyO18htDpvpFvlmoHwyptCkgluDnlq9Czn70B7gkhB47imCz_w2zBjj7hE24_qFlx9hNi29qD-yh7Kzh_gBgwrayyy4F5lqsB2--yQk5wwB7h6Mx2zoEkixsJoni-D0ll-Ev5hrI346oEs0m1Kvs9nIk3v14R_l1V7ulx6VpjiE1h8c96mLvn3GygrhB09xnF__1F9hpU83yBw5_b7koO8l9Fw54MmonXmpvkBovtrB16lkB5m9Iuth2Bq-tLmk_G9q1rBkxoJxmyFo9xnCzpvvB-4ggHzjoepquqBjsvrBwu-xB-2l_Bmp1uBt0gpB-9i_DzovNl9uXqvyIghrE9_owBmz0nD5ozyCl21lElqwPoliXmjlHuvoiD6swaq5mbytvlCvxrkBrjkkB2j7HmhtP2oyFns2E9nqyC2h6iCn2q0Clo1xHs16e4h2hBplsan78MkrnJomuKwijH1oiWg8jcu8qRy09pB5o9jDts2Hs8-Vsxq1D67ihBw17jB7p0Vl5jMgupa7hjQu79sHthqJ_soSn5hS8rxE6n2qBqolrHk4ioB_tjxBxxmwD6s1asm5gB4ojgCh06gFq7gzDku0vD75gmB7xq2BvsmxBm03lCl3gO8jt8B8vohBrutFx4sB0z0yBrtzC77vP636Cym6EullV1s0Zt71MlwsE4p6sBnv2vB4nlFzxoK41i6C1u2Ngvn5Bl_iGtslrC4_tjD4lu1Evylcg_joB6m0oCp9uL6igKtsvvBtvvV-rrIt7nNpghKgq8Evyg1BxivN9ppD16yeksuD3p8gBlzoTxgykBpkhqBvnpfzi3Sl_7xBi-zM";
            // parse shape value
            LocationCollection locs;
            //Helper.TryParseEncodedValue(response.ResultSet.Results[0].Primitives[0].Shape, out locs);
            Helper.TryParseEncodedValue(sampleShapeValue, out locs);
            Assert.IsTrue(locs.Count == 478);
        }


    }
}
