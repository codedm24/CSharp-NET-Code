using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace ConsoleAppParallelTask
{
    public class Program4
    {
        static void Main(string[] args) {
            var target = SetupPipeline();
            target.Post("F:\\Projects\\Windows\\ConsoleAppParallelTask\\ConsoleAppParallelTask");
            ReadLine();
        }

        public static IEnumerable<string> GetFileNames(string path) { 
            foreach(string fileName in Directory.EnumerateFiles(path, "*.cs")) {
                yield return fileName;
            }
        }

        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames) { 
            foreach (string fileName in fileNames)
            {
                using(FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> GetWords(IEnumerable<string> lines) {
            foreach(var line in lines)
            {
                string[] words = line.Split(' ',';','(',')','{','}','.',',');
                foreach(var word in words)
                {
                    if(!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }

        public static ITargetBlock<string> SetupPipeline()
        { 
            var fileNamesForPath = new TransformBlock<string,IEnumerable<string>>(path => { return GetFileNames(path); });

            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames => { 
                return LoadLines(fileNames);
            });

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines2 => { 
                return GetWords(lines2);
            });

            var display = new ActionBlock<IEnumerable<string>>(coll => { 
                foreach(var s in coll)
                    WriteLine(s);
            });

            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }
    }
}
