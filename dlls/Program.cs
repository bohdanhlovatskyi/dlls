using System;

namespace dlls
{

    using LinkedList;
    class Program
    {
        static void Main(string[] args)
        {

            var ll = new LinkedList<int>();
            ll.PushFront(10);
            ll.PushFront(14);
            ll.PushFront(12);
            Console.WriteLine(ll);
        }
    }
}
