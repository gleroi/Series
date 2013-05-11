using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Core.TvShows;
using Series.Core.TvShows.Episodes;

namespace Series.Renamer
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("usage : Serie.Renamer.exe <folder> <serie>");
                return 1;
            }

            string serieFolder = args[0];
            string serieName = args[1];
            if (!Directory.Exists(serieFolder))
            {
                Console.WriteLine("Folder '" + serieFolder + "' does not exist");
                return 1;
            }

            FilenameInfosExtractor extractor = new FilenameInfosExtractor();
            foreach (var file in Directory.GetFiles(serieFolder))
            {
                Episode ep = extractor.Extract(file);
                if (ep != null)
                {
                    FileInfo info = new FileInfo(file);
                    Console.WriteLine("Rename " + file);

                    string newFilename = String.Format("{0}.s{1:00}e{2:00}{3}",
                        serieName, ep.Season, ep.Opus, info.Extension);
                    Console.WriteLine("\t" + newFilename);
                    info.MoveTo(Path.Combine(info.DirectoryName, newFilename));
                }
                else
                {
                    Console.WriteLine("No rename for " + file);
                }
            }
            return 0;
        }
    }
}
