using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    class Program
    {
        public string ProcessData(string dataName)
        {
            
            Thread.Sleep(3000);
            return $"Обработка {dataName} завершена за 3 секунды";
        }

        public async Task<string> ProcessDataAsync(string dataName)
        {
            await Task.Delay(3000);
            return $"Обработка {dataName} завершена за 3 секунды";
        }

        static async Task Main(string[] args)
        {
            Program program= new Program();

            Console.WriteLine("Синхронная обработка");

            var start=DateTime.Now;

            string res1 = program.ProcessData("Файл1");
            Console.WriteLine(res1);

            string res2 = program.ProcessData("Файл2");
            Console.WriteLine(res2);

            string res3 = program.ProcessData("Файл3");
            Console.WriteLine(res3);

            var resTime = DateTime.Now - start;
            Console.WriteLine($"Синхронная обработка заняла {resTime}");

            Console.WriteLine("Асинхронная обработка");

            var startAsync = DateTime.Now;

            Task<string> resAsync1 = program.ProcessDataAsync("Файл1");
            Task<string> resAsync2 = program.ProcessDataAsync("Файл2");
            Task<string> resAsync3 = program.ProcessDataAsync("Файл3");

            await Task.WhenAll(resAsync1, resAsync2, resAsync3);

            Console.WriteLine(resAsync1.Result);
            Console.WriteLine(resAsync2.Result);
            Console.WriteLine(resAsync3.Result);

            var resTimeAsync = DateTime.Now - startAsync;
            Console.WriteLine($"Асинхронная обработка заняла {resTimeAsync}");




        }
    }
}