using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgrammingSamples
{
    class PlinqSample
    {
        public static void Main()
        {
            string senteence = "this is just the sample of my task programming learning";
            //Select in LInq is used for projecting one form of elements to another (simple executing for each element) 
            //new string(): as the reverse returns a Ienumerable of characters, and 
            //As the new string() constructor cannot handle the Ienumerable of characters
            //.ToArray takes Ienumerable of Characters and returns array of characters
            //which are converted to string using the string()
            //To make the process of function always in parallel we use .WithExecutionMode

            //Linq:
            //var words = senteence.Split()
            //                      .Select(x => new string(x.Reverse().ToArray()));

            //PLinq:
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var words = senteence.Split()
                                  .AsParallel()   
                                  .AsOrdered()
                                  .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                  .Select(x => new string(x.Reverse().ToArray()));



            //Join: this method concatenates the ienumerable strings.
            //which are delimited by spaces
            Console.WriteLine(string.Join(" ",words));

            sw.Stop();
            Console.WriteLine("Elapsed Time" + sw.ElapsedMilliseconds);
        }
    }
}

//our sample is basically works as a consequental(which means squential), but in squential the overhead of creating task network mapping the data,
//process and reduce, are making it slow, so i tried to force that to work as parallel.
//output when forced to work in parallel: "tsuj elpmas fo ym ksat gnimmargorp siht eht si gninrael" which is not  correct

// so to avoid the order mismatch we have a method where we ask the PLINQ parallism to preserve the order using the ASORDERED method
//A subsequent call to ASUNORDER restores the PLINQ ordereing behaviour.
