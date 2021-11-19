using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SupportBank
{
    class AllTransactions: List<Transaction>
    {
        public void ExportCSV(string filename)
        {
            string path = @"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\";
            string finalpath = Path.Combine(path, filename + ".csv");
            if (!File.Exists(finalpath))
            {
                var file = File.Create(finalpath);
                file.Close();
                StreamWriter sw = new StreamWriter(finalpath,true);
                sw.WriteLine("{0},{1},{2},{3},{4}", "Date", "FromAccount", "ToAccount", "Narrative", "Amount");
                foreach (Transaction t in this)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4}", t.Date, t.FromAccount, t.ToAccount, t.Narrative, t.Amount.ToString());
                }
                sw.Close();
            }
            else
            {
                throw (new Exception("FileFound"));
            }

        }
        public void ExportJSON(string filename)
        {
            string path = @"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\";
            string finalpath = Path.Combine(path, filename + ".json");
            if (!File.Exists(finalpath))
            {
                var file = File.Create(finalpath);
                file.Close();
                using (StreamWriter sw = new StreamWriter(finalpath, true))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(sw, this);
                }
            }
            else
            {
                throw (new Exception("FileFound"));
            }
        }
        public void ExportXML(string filename)
        {
            string path = @"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\";
            string finalpath = Path.Combine(path, filename + ".xml");
            if (!File.Exists(finalpath))
            {
                var file = File.Create(finalpath);
                file.Close();
                foreach(Transaction t in this)
                {
                    t.p = new Pearent();
                    t.p.FromAccount = t.FromAccount;
                    t.p.ToAccount = t.ToAccount;
                }
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Transaction>));
                TextWriter Filestream = new StreamWriter(finalpath);
                serialiser.Serialize(Filestream, this);
                Filestream.Close();
            }
            else
            {
                throw (new Exception("FileFound"));
            }
        }
    }
}
