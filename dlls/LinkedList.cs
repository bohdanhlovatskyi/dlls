using System;
using System.IO; 
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList {

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

    public class LinkedList<T> : IEnumerable<Node<T>> {    
        private Node<T> head;
        private Node<T> tail;

        private Node<T> current;

        private int itemCount = 0;

        public int Length {
            get {
                return itemCount;
            }
        }

        public LinkedList() {
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

        public LinkedList<T> Sorted(Func<Node<T>, T> comparator) {

            var temp = new LinkedList<T>();
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
}
