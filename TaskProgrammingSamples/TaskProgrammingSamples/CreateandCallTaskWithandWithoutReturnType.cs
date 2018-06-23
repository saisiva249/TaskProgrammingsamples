using System;
using System.Threading;
using System.Threading.Tasks;


namespace TaskProgrammingSamples
{
    class CreateandCallTaskWithandWithoutReturnType
    {
        public static void Main()
        {
            //Task instance Creation
            var task_NoParameter_NoReturn = Task.Factory.StartNew(DoTask);
            var task_MessageobjectParameter_NoReturn = Task.Factory.StartNew(DoTaskParameter, "Method Returns"); // receiving parameter should be of object type or else throws error
            var task_MessagestringParameter_NoReturn = Task.Factory.StartNew(()=> { DoTaskParameterString("Method Returns"); });
            var task_NoParameter_ReturnsString = Task<string>.Factory.StartNew(DoTaskandReturn);
            var timeTakingTask_NoParameter_NoReturn = Task.Factory.StartNew(TimeTakingTask, TaskCreationOptions.LongRunning);

            //Main Thread Work- which will be executed for sure
            Console.WriteLine("This is From Main Thread");
            
            //Making the background threads wait until our tasks are completed
            task_NoParameter_NoReturn.Wait();
            task_MessageobjectParameter_NoReturn.Wait();
            task_MessagestringParameter_NoReturn.Wait();
            Console.WriteLine(task_NoParameter_ReturnsString.Result);
            timeTakingTask_NoParameter_NoReturn.Wait();
        }

        //Task  which dont return and dont accept any parameter
        static void DoTask()
        {
            Console.WriteLine("From DoTask");
        }

        //Task  which dont return which accepts one parameter
        //Parameter should be object type
        static void DoTaskParameter(object message)
        {
            Console.WriteLine("From DoTask "+ message);
        }

        //Task  which dont return which accepts one parameter
        //Parameter can be of any type
        static void DoTaskParameterString(string message)
        {
            Console.WriteLine("From DoTask with string " + message);
        }

        static string DoTaskandReturn()
        {
            Thread.Sleep(2000);
            return "Venkata";
        }

        //if a task takes more time to execute then it is preferd not to run on a threaad from threadpool
        static void TimeTakingTask()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Time taking Task");
        }

    }
}

//Here we have Three tasks 
//DoTask with no parameters,
//DoTaskParameter which accepts one parameter 
//DoTaskandReturn which return result without accepting parameter
//As these tasks work in background thread, these tasks will not be executed 
//with out Wait or Result methods.

