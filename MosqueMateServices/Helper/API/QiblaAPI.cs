using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.Helper.API
{
    public class QiblaAPI
    {
        public int code { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double direction { get; set; }
    }
}
