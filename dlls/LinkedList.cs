using System;
using System.IO; 
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MyLinkedList {

    public class Node<T> {
        public Node<T> next { get; set; }
        public Node<T> prev { get; set; }
        public T data { get; set; }

        public Node(T val) {
            next = null;
            prev = null;
            data = val;
        }

        public override string ToString()
        {
            return $"{{{data}}}";
        }
    }

    public class MyLinkedList<T> : IEnumerable<Node<T>> {    
        private Node<T> head;
        private Node<T> tail;

        private Node<T> current;

        private int itemCount = 0;

        public int Length {
            get {
                return itemCount;
            }
        }

        public Node<T> Head {
            get {
                return head;
            }
        }

        public Node<T> Tail {
            get {
                return tail;
            }
        }

        public MyLinkedList() {
            head = null;
            tail = null;
        }

        public IEnumerator<Node<T>> GetEnumerator() {
            current = head;
            while (current != null) {
                yield return current;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // this will invoke the public generic
            // version, so there is no recursion
            return this.GetEnumerator();
        }

        public void PushFront(T val) {
            var node = new Node<T>(val);
            if (head == null) {
                head = node;
                tail = node;
            } else {
                head.prev = node;
                node.next = head;
                head = node;
            }
            itemCount++;
        }

        public void PushBack(T val) {
            var node = new Node<T>(val);
            if (head == null) {
                head = node;
            }

            if (tail != null) {
                tail.next = node;
            }

            node.next = null;
            node.prev = tail;
            tail = node;

            itemCount++;
        }

        public void Insert(T val, int pos) {
            var nth = this.Skip(pos - 1).FirstOrDefault();
            
            InsertAfter(val, nth);
        }


        internal void InsertAfter(T val, Node<T> prev) {
            if (prev == null) {
                // throw new NullReferenceException("given prev is null ref");
                return;
            }

            var node = new Node<T>(val);
            node.next = prev.next;
            prev.next = node;
            node.prev = prev;
            if (node.next != null) {
                node.next.prev = node;
            }

            itemCount++;
        } 

        public Node<T> RemoveByKey(T val) {
            var first = this.First<Node<T>>(node => node.data.Equals(val));
            return RemoveNode(first);
        }

        public Node<T> RemoveIf(Func<Node<T>, bool> f) {
            var first = this.First<Node<T>>(f);
            return RemoveNode(first);
        }

        public Node<T> Remove(int pos) {
            var nth = this.Skip(pos).FirstOrDefault();
            return RemoveNode(nth);
        }

        internal Node<T> RemoveNode(Node<T> node) {
            if (node == null) {
                return null;
            }

            if (node.next != null) {
                node.next.prev = node.prev;
            }

            if (node.prev != null) {
                node.prev.next = node.next;
            }

            itemCount--;

            return node;
        }


        public Node<T> findByKey(T key) {
            return this.First<Node<T>>(node => node.data.Equals(key));
        }

        public MyLinkedList<T> Sorted(Func<Node<T>, T> comparator) {

            var temp = new MyLinkedList<T>();
            var ordered = this.OrderBy(comparator).AsEnumerable();

            foreach(var node in ordered) {
                temp.PushBack(node.data);
            }

            return temp;
        }

        public override string ToString()
        {
            var stream = new StringWriter();
            foreach (var node in this) {
                stream.Write(node.ToString() + " ");
            }

            return stream.ToString();
        }
    }

    // Note this is not really necessary, though I wanted to practive a little bit
    public static class SortingExtension {
        public static void SortInplace<T>(this MyLinkedList<T> ll, IComparer<T> comparer) {
            if (ll == null || ll.Length <=  1) {
                return;
            }

            if (ll.Head  == null || ll.Tail == null) {
                throw new NullReferenceException("Bad linked list passed");
            }

            Helper<T>(ll.Head, ll.Tail,  comparer);
        }

        internal static void Helper<T>(Node<T> head, Node<T> tail, IComparer<T> comparer) {
            if (head == tail) {
                return;
            }

            // note that here we could not improve this easily 
            // via randomisation, as it won't be O(1) to get
            // some node without additional memory
            var pivot = tail;
            var cur = head;
            while (cur.next != pivot) {
                if (comparer.Compare(cur.data, pivot.data) > 0) {
                    Swap(pivot.prev, pivot);
                    Swap(pivot, cur);
                    pivot = pivot.prev;
                } else {
                    cur = cur.next;
                }
            }

            if (comparer.Compare(cur.data, pivot.data) > 0) {
                Swap(cur,  pivot);
                pivot = cur;
            }

            if (head != pivot) {
                Helper(head, pivot.prev, comparer);
            }

            if (tail != pivot) {
                Helper(pivot.next,  tail, comparer);
            }
        }

        internal static void Swap<T>(Node<T> a, Node<T> b) {
            var tmp = a.data;
            a.data = b.data;
            b.data = tmp;
        }
    }
}
