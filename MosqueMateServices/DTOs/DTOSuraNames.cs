using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.DTOs
{
    public class DTOSuraNames
    {
        public List<Surah> surahs { get; set; }
    }
    public class Surah
    {
        public int index { get; set; }
        public int pageIndex { get; set; }
        public string name { get; set; }
    }
}
