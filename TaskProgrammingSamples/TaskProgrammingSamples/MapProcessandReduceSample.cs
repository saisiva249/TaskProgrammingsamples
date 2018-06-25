using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgrammingSamples
{
    //Sample to show MapReduce procedure using the continuation task
    class MapProcessandReduceSample
    {
        private static string ReverseString(string word)
        {
            Thread.Sleep(1000);
            StringBuilder sb = new StringBuilder();
            for(int i = word.Length; i >= 0; i--)
            {
                sb.Append(word[i]);
            }
            return sb.ToString();
        }


        //Map: splitting the sentence to words
        //Process: reversing the each word splitted using parallel tasks
        //Reduce : aggregat the result in process


        //MapReduce step1: Map
        private static string[] Map(string sentece)
        {
            return sentece.Split();
        }

        //MapReduce step2: Process
        private static string[] Process(string[] words)
        {
            for (int i = 0; i< words.Length; i++)
            {
                int index = i;
                Task.Factory.StartNew(
                    ()=> words[index] = ReverseString(words[index]),
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            return words;
        }

        //MapReduce step3: Reduce
        private static string Reduce(string[] words)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                sb.Append(word);
                sb.Append(' ');
            }
            return sb.ToString();
        }

        public static void Main()
        {
            string sentence = "this is just the sample of my task programming learning";

            //initializing the map-reduce process
            //lamda expression in the each continuation can access the result of previous task.
            var task = Task<string[]>.Factory.StartNew(()=> Map(sentence))
                        .ContinueWith<string[]>(t=>Process(t.Result))
                        .ContinueWith<string>(t=>Reduce(t.Result));

            //display output
            Console.WriteLine("Result : {0} ", task.Result);

        }
    }
}
