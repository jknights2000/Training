using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Export
    {
        public Export(string answer, AllTransactions alltransactions)
        {
            
                if (alltransactions.Count <= 0)
                {
                    Console.WriteLine("nohting to export");

                }
                else
                {
                    int filenamestart = answer.ToLower().LastIndexOf("export") + 7;
                    string filename = answer.Substring(filenamestart);
                    Console.WriteLine($"what type of file do you want {filename} to be stored as?");
                    string filetype = Console.ReadLine();
                    switch (filetype.Trim().ToLower())
                    {
                        case ".csv":
                        case "csv":
                            try
                            {
                                alltransactions.ExportCSV(filename);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            break;
                        case ".json":
                        case "json":
                            try
                            {
                                alltransactions.ExportJSON(filename);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            break;
                        case ".xml":
                        case "xml":
                            try
                            {
                                alltransactions.ExportXML(filename);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                            break;
                        case "":
                            Console.WriteLine("no file type specified");
                          
                            break;
                        default:
                            Console.WriteLine("This file type is not supported");
                            
                            break;
                    }

                }
            
        }
    }
}
