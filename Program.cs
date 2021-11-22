using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            _ArrayList<int> intList = new _ArrayList<int>(5);
            intList.Add(203);
            intList.Add(19);
            intList.Add(1);
            intList.Add(6);
            intList.Add(8);
            intList.Add(10);
            intList.RemoveAt(2);

            foreach (var item in intList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            _ArrayList<string> stringList = new _ArrayList<string>(4);
            stringList.Add("a");
            stringList.Add("b");
            stringList.Add("c");

            foreach (var item in stringList)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
