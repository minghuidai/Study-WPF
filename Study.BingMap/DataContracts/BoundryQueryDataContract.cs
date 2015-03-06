using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization.Json;


namespace Study.BingMap.DataContracts
{

    [DataContract]
    public class BoundryQueryResponseDataContract
    {
        public string d { get; set; }
    }





    [DataContract]
    public class d
    {
        [DataMember(Name = "copyright")]
        public string Copyright { get; set; }

        public Result[] results { get; set; }

    }



    [DataContract]
    public class Result
    {
        //public object EntityMetadata { get; set; }

        public string EntityID {get; set; }

        public NameData Name { get; set; }

        public Primitive[] Primitives { get; set; }
    }


    [DataContract]
    public class NameData
    {
        public string EntityName { get; set; }
        public string Culture { get; set; }
        public string SourceID { get; set; }
    }


    [DataContract]
    public class Primitive
    {
        public string PrimitiveID { get; set; }
        public string Shape { get; set; }
        public string NumPoints { get; set; }
        public string SourceID { get; set; }
    }


  
}
