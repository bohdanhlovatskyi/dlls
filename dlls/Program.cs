﻿using System;
using System.Collections.Generic;

namespace dlls
{

    using MyLinkedList;
    using MorseCode;
    class Program
    {
        static void Main(string[] args)
        {

            // print out of the inserted values, one-by-one, in alhabetical order
            // (characteer and corresponding morese code to it)
            var mc  =  MorseCode.getMorseCode();
            Console.WriteLine(mc);

            // delete those: F, R, B, Z, A, M, G, R, C, Q, Y, C, N
            char[] to_delete = new[] {'F', 'R', 'B', 'Z', 'A', 'M', 'G', 'R', 'C', 'Q', 'Y',  'C', 'N'};

            foreach(var ch in to_delete) {
                try {
                    mc.RemoveIf(node => node.data.Item1 == Char.ToLower(ch));
                } catch {
                    // if you could not find the node, do nothing
                    continue;
                }
            }
            Console.WriteLine(mc);

            // sort the list by its morse data
            mc.SortInplace(Comparer<(char, int)>.Create((c1, c2) => c1.Item2.CompareTo(c2.Item2)));
            Console.WriteLine(mc);
        }

        static void example() {
            var ll = new MyLinkedList<int>();
            ll.PushFront(10);
            ll.PushFront(14);
            ll.PushFront(12);

            ll.Insert(9, 1);
            ll.Remove(2);

            Console.WriteLine(ll);

            Console.WriteLine(ll.Sorted(x => x.data));

            ll.SortInplace(Comparer<int>.Default);
            Console.WriteLine(ll);
        }
    }
}
