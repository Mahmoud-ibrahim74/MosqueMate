using MosqueMateServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.Interfaces
{
    public interface ISouraNames
    {
        public string GetSouraNameById(int id);
        public int GetSouraIdByName(string name);
        public List<string> GetAllSoura();
    }
}
