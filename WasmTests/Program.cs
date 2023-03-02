using System;
using WebAssembly;
using WebAssembly.Instructions;
using WebAssembly.Runtime;

namespace WasmTests
{
    public unsafe abstract class Sample
    {
        public abstract int Main(int a);
    }
    internal unsafe class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var module = new Module();
            var wasmCode = File.OpenRead("./Test.wasm");
            using var instance = Compile.FromBinary<Sample>(wasmCode)(new ImportDictionary());
            var test = new Task(() => instance.Exports.Main(0));
            try
            {
                test.Start();
                test.Wait();
            }
            catch 
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("WALA");


        }
    }
}