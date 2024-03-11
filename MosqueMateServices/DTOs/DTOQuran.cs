using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.DTOs
{
    public class DTOQuran
    {
        public string index { get; set; }
        public string name { get; set; }
        public List<string> verse { get; set; }
        public int count { get; set; }
    }
    
}
