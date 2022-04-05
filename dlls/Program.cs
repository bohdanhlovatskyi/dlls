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

            ll.Insert(9, 1);
            ll.Remove(2);

            Console.WriteLine(ll);
        }
    }
}
