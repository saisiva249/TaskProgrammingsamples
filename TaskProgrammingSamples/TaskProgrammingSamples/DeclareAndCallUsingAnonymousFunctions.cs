
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgrammingSamples
{
    //the other way of accessing and defining the tasks is using anonymous function
    class DeclareAndCallUsingAnonymousFunctions
    {
        public static void Main()
        {
            //from Mian thread
            Console.WriteLine("Main thread");
            string Message = "Initial Message";

            var task_NoParameters_NoReturn = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("task_NoParameters_NoReturn");
            });


            //initializing using lamda expression and using a initializing object
            //initializing object is stored in task.asyncstate, which helps in 
            //debugging
            var task_Parameters_NoReturn = Task.Factory.StartNew((state) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine(Message +" and " + state);
            },"MessageTask");

            task_Parameters_NoReturn.Wait();
            task_NoParameters_NoReturn.Wait();
            Console.WriteLine("Main thread ends");
        }
    }
}