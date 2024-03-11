using System;
using System.Text;

namespace MosqueMateServices.Helper
{
    public class StringBuilderHelper : IDisposable
    {
        private readonly StringBuilder sbOneObj;
        public StringBuilderHelper()
        {
            sbOneObj = new StringBuilder();
        }

        public string GetAppendObj
        {
            get
            {
                return sbOneObj.ToString() != null ? sbOneObj.ToString() : string.Empty;
            }
            set
            {
            }
        }
        public string Append(int appendCount, params string[] appendlines)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < appendCount; i++)
            {
                sb.Append(appendlines[i]);
            }
            return sb.ToString();
        }
        public string AppendLine(int appendCount, params string[] appendlines)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < appendCount; i++)
            {
                sb.AppendLine(appendlines[i]);
            }
            return sb.ToString();
        }
        public string AppendInOneObject(int appendCount, params string[] appendlines)
        {
            for (int i = 0; i < appendCount; i++)
            {
                sbOneObj.Append(appendlines[i]);
            }
            return sbOneObj.ToString();
        }
        public string AppendLineInOneObject(int appendCount, params string[] appendlines)
        {
            for (int i = 0; i < appendCount; i++)
            {
                sbOneObj.AppendLine(appendlines[i]);
            }
            return sbOneObj.ToString();
        }
        public void RemoveInOneObject()
        {
            sbOneObj.Remove(0, sbOneObj.Length);
            GetAppendObj = string.Empty;
        }

        public void Dispose()
        {
            sbOneObj.Clear();
        }
    }
}
