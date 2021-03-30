using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Module3_Task5
{
    public class Starter
    {
        public async void Run()
        {
            var result = await ThirdMethodAsync();
            Console.WriteLine(result);
        }

        private async Task<string> FirstMethodAsync()
        {
            return await Task.Run(async () =>
            {
                string filePath = "File.txt";
                string text = await File.ReadAllTextAsync(filePath);
                return text;
            });
        }

        private async Task<string> SecondMethodAsync()
        {
            return await Task.FromResult("World");
        }

        private async Task<string> ThirdMethodAsync()
        {
            var taskList = new List<Task<string>>();
            var resultsList = new List<string>();

            /*taskList.Add(Task.Run(() =>
            {
                var res = FirstMethodAsync();
                return res;
            }));

            taskList.Add(Task.Run(() =>
            {
                var res = SecondMethodAsync();
                return res;
            }));*/

            taskList.Add(FirstMethodAsync());

            taskList.Add(SecondMethodAsync());

            await Task.WhenAll(taskList);

            Console.WriteLine(taskList);

            foreach (var t in taskList)
            {
                var res = t.Result;
                resultsList.Add(res);
            }

            var result = resultsList.Aggregate((i, j) => i + " " + j);

            return result;
        }
    }
}