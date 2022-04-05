using System;
using Xunit;

using System.Collections.Generic;

namespace tests
{
    using MyLinkedList;
    public class UnitTest1
    {
        [Fact]
        public void PushBackAndPopTest()
        {

            MyLinkedList<int> ll = new MyLinkedList<int>();

            ll.PushBack(1);
            ll.PushBack(2);
            
            Assert.Equal(2, ll.Length);

            var node = ll.Remove(1);
            Assert.Equal(1, ll.Length);
            Assert.Equal(2, node.data);

            node = ll.Remove(0);
            Assert.Equal(0, ll.Length);
            Assert.Equal(1, node.data);

            // to make sure that everything is ok with head and tail pointers after pushing  and removing
            ll.PushBack(1);
            Assert.Equal(1, ll.Length);
        }

        [Fact]
        public void InsertTest()
        {
            MyLinkedList<int> ll = new MyLinkedList<int>();

            // we could not insert into empty array by convention
            ll.PushBack(1);
            Assert.Equal(1, ll.Length);

            // insert as a second elm
            ll.Insert(2, 1);
            Assert.Equal(2, ll.Length);

            // trivial insert
            ll.Insert(3, 1);
            Assert.Equal(3, ll.Length);

            // insert at back
            ll.Insert(4, 3);
            Assert.Equal(4, ll.Length);
        }

        [Fact]
        public void SortCopyTest()
        {
            MyLinkedList<int> ll = new MyLinkedList<int>();

            // we could not insert into empty array by convention
            ll.PushBack(1);
            ll.PushBack(3);
            ll.PushBack(0);
            ll.PushBack(10);

            var sorted = ll.Sorted(x => x.data);
            Assert.Equal(0, sorted.Head.data);
            Assert.Equal(10, sorted.Tail.data);

            // make sure that copy is returned
            sorted.Head.data = 100;
            Assert.Equal(ll.Head.data, 1);
        }


        [Fact]
        public void SortInplaceTest()
        {
            MyLinkedList<int> ll = new MyLinkedList<int>();

            // we could not insert into empty array by convention
            ll.PushBack(1);
            ll.PushBack(3);
            ll.PushBack(0);
            ll.PushBack(10);

            ll.SortInplace(Comparer<int>.Default);
            Assert.Equal(0, ll.Head.data);
            Assert.Equal(10, ll.Tail.data);
        }
    }
}
