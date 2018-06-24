using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgrammingSamples
{
    //Task hierarchy programming
    //when a sentence is given, we will reverse each word and then finally the sentence.
    class ReverseaSentence
    {
        //Taks of each word reversing is given to new task.
        //this list stores the child tasks which returns the string.
        private static List<Task<string>> childTasks = new List<Task<string>>();

        //method which reverse a string
        private static string ReverseString(string word)
        {
            //Making process to sleep for one second before reversing a word
            //So for a 10 sec word the process takes 10 sec
            Thread.Sleep(1000);

            StringBuilder sb = new StringBuilder();
            for (int i = word.Length-1; i >= 0 ; i--)
            {
                sb.Append(word[i]);
            }
            return sb.ToString();
        }


        //Sentense processing
        private static void SentenceProcessing(string sen)
        {
            foreach (string word in sen.Split())
            {
                //Craeting a child task using the create option attachedtoparent
                //child tasks are created in a new thread using longrunning create option, instead of creating in the thread pool
                //AS we are running all task individually the parent will be completed in one seccond instead of 10 sec
                childTasks.Add(Task.Factory.StartNew(
                    ()=> ReverseString(word) + " ", 
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning));
            }
        }

        public static void Main()
        {
            string sentence = "this is just the sample of my task programming learning";

            //
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // we make our process stop until our parent task is completed using Wait.
            Task.Factory.StartNew(()=> SentenceProcessing(sentence)).Wait();
            sw.Stop();
            Console.WriteLine("Total runtime: {0}ms", sw.ElapsedMilliseconds);

            //If we want to know the state of a task whether it is completed or not 
            //then we have to use .IsCompleted property

            //Writing the output
            Console.Write("Result: ");
            foreach (Task<string>child in childTasks)
            {
                Console.Write(child.Result);
            }

        }

    }
}
