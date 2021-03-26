using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookLibrary
{  
   
    public class PhoneLibrary
    {
        static ReaderWriterLock locker = new ReaderWriterLock();

        

        public List<Entry> Entries { get; set; }

        public PhoneLibrary(string sort = "")
        {
            
            string fileName = "data.bin";
            if (File.Exists(fileName))
            {
                try
                {
                    locker.AcquireReaderLock(int.MaxValue);
                    using (FileStream fs1 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        BinaryReader br = new BinaryReader(fs1);
                        Entries = new List<Entry>();
                        while (br.BaseStream.Length > br.BaseStream.Position)
                        {
                            try
                            {
                                Entry entry = new Entry
                                {
                                    ID = br.ReadString(),
                                    FirstName = br.ReadString(),
                                    LastName = br.ReadString(),
                                    Type = br.ReadString(),
                                    Number = br.ReadString()
                                };
                                Entries.Add(entry);
                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, "Loading entries");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sort))
                    {
                        if (sort == "FirstName")
                        {
                            Entries = Entries.OrderBy(x => x.FirstName).ToList();
                        }
                        else if (sort == "LastName")
                        {
                            Entries = Entries.OrderBy(x => x.LastName).ToList();
                        }
                    }                    
                }
                catch(Exception ex)
                {
                    Log.Error(ex, "Reading file");                   
                }
                finally
                {
                    locker.ReleaseReaderLock();
                }               
                
            }
            else // If file does not exist, create it.
            {
                BinaryWriter bwStream = new BinaryWriter(new FileStream(fileName, FileMode.Create));

                Encoding ascii = Encoding.ASCII;
                BinaryWriter bwEncoder = new BinaryWriter(new FileStream(fileName, FileMode.Create), ascii);
            }
        }

        public string AddEntry(Entry _entry)
        {
            _entry.ID = "";           


            while (string.IsNullOrEmpty(_entry.ID) || Entries.Where(x => x.ID == _entry.ID).Count() > 0)
            {
                _entry.ID = Guid.NewGuid().ToString();
            }

            Entries.Add(_entry);
            var result = SaveChanges();
            return result;
        }

        public string Edit(Entry _entry)
        {
            var originalEntry = Entries.Where(x => x.ID == _entry.ID).FirstOrDefault();
            if(originalEntry != null)
            {
                originalEntry = _entry;
                var result = SaveChanges();
                return "Entry edited";                
            }
            else
            {
                return "Entry not found";
            }           
        }

        public string DeleteEntry(string id)
        {
            Entry entry = Entries.Where(x => x.ID == id).FirstOrDefault();
            if (entry != null)
            {
                Entries.Remove(entry);
                var result = SaveChanges();
                return result;
            }
            else
            {
                return "Not Found";
            }
           
        }



        public string SaveChanges()
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);
                using (FileStream fs = new FileStream("data.bin", FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        foreach (var entry in Entries)
                        {
                            bw.Write(entry.ID);
                            bw.Write(entry.FirstName);
                            bw.Write(entry.LastName);
                            bw.Write(entry.Type);
                            bw.Write(entry.Number);
                        }
                    }
                }
                return "Successful";
            }
            catch(Exception ex)
            {
                Log.Error(ex, "During writing file");
                return "Error while saving: " + ex.Message + Environment.NewLine + " - Check Logs.txt for more detail";
            }
            finally
            {
                locker.ReleaseWriterLock();
            }

        }
    }
}
