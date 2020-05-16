using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace CusomtAsyncStateMachine
{
    public static class Program
    {
        public static async Task Main()
        {
            var testClass = new TestClass();

            var foos1 = await testClass.ReadFoosAsync1("data.json");
            Console.WriteLine(JsonSerializer.Serialize(foos1, new JsonSerializerOptions() { WriteIndented = true }));

            var foos2 = await testClass.ReadFoosAsync2("data.json");
            Console.WriteLine(JsonSerializer.Serialize(foos2, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}