
namespace MorseCode {
    using System.Collections.Generic;
    using MyLinkedList;


    public class MorseCode {
        public static Dictionary<char, int> map = new Dictionary<char, int>() {
            {'a', 12}, {'b', 2111}, {'c', 2121},
            {'d', 211}, {'e', 1}, {'f', 1121},
            {'g', 221}, {'h', 1111}, {'i', 11},
            {'j', 1222}, {'k', 212}, {'l', 1211},
            {'m', 22}, {'n', 21}, {'o', 111},
            {'p', 1221}, {'q', 2212}, {'r', 121},
            {'s', 111}, {'t', 2}, {'u', 112},
            {'v', 1112}, {'w', 122}, {'x', 2112},
            {'y', 2122}, {'z', 2211}
        };

        public static MyLinkedList<(char, int)> getMorseCode() {
            var data  = new MyLinkedList<(char, int)>();

            foreach (var pair in map) {
                data.PushBack((pair.Key,  pair.Value));
            }

            return data;
        }
    }
}