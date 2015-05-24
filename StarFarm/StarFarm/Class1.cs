using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Media;

namespace StarFarm
{
    public class Class1
    {
        private Thread _thr;
        public Class1() 
        {
            _thr = new Thread(Run);
            _thr.Start();
        }
        static int file_index = 0;

        private void Run()
        {
            while (true)
            {
                try
                {
                    List<string> str = new List<string>(Directory.GetDirectories(Properties.Settings.Default.LastPath).ToList());
                    str.Sort();
                    string last_dir = str.ElementAt(str.Count - 1);
                    last_dir += "\\game.log";
                    //List<string> file = new List<string>(File.ReadAllLines(last_dir).ToList());
                    FileInfo fi = new FileInfo(last_dir);
                    //FileStream fs = File.OpenRead(last_dir);
                    FileStream fs = File.Open(last_dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    byte[] bytes = new byte[fi.Length];
                    int bytesRead = fs.Read(bytes, 0, bytes.Length);
                    //string temp_buff = File.ReadAllText(last_dir);
                    string temp_buff = System.Text.Encoding.UTF8.GetString(bytes);
                    int curr_length = temp_buff.Length;
                    if (file_index == curr_length) 
                    {
                        continue;
                    }
                    if (file_index > curr_length)
                    {
                        file_index = 0;
                        continue;
                    }
                    List<string> file = new List<string>(temp_buff.Substring(file_index).Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                    foreach (string str_c in file) 
                    {
                        if (str_c.Length > 0)
                        {
                            try
                            {
                                #region
                                //string date = str_c.Substring(0, 12);
                                //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                                //dtfi.LongTimePattern = "hh:mm:ss.fff";
                                //DateTime dt = DateTime.ParseExact(date, "HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                //Console.Write(date + "\r\n");
                                #endregion


                                if ((str_c.IndexOf("ADD_PLAYER") != -1) && (str_c.IndexOf(Properties.Settings.Default.LastNickName) == -1) && (str_c.IndexOf("status 4") != -1)) 
                                {
                                    Console.WriteLine("Bingo");
                                    Console.WriteLine("Row: \"" + str_c+ "\"");
                                    Console.WriteLine("Currunt file index: " + Convert.ToString(file_index));
                                    Console.WriteLine("Currunt file index: " + Convert.ToString(curr_length));
                                    
                                    SystemSounds.Asterisk.Play();
                                    break;
                                }
                            }
                            catch (ThreadAbortException abortEx)
                            {
                                Console.WriteLine(abortEx.Message);
                                throw abortEx;
                            }
                            catch (Exception ex) 
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                            }
                        }
                    }
                    file_index = curr_length;
                    Thread.Sleep(500);
                }
                catch (ThreadAbortException abortEx)
                {
                    Console.WriteLine(abortEx.Message);
                    throw abortEx;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Close()
        {
            _thr.Abort();
        }
    }
}
