/* Exemplary file for Chapter 5 - Variants of Trees. */

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Trees
{
    class Program
    {
        const int numberOfItems = 1000;
        private const int COLUMN_WIDTH = 5;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Timing tObj = new Timing();
            
            BinarySearchTree<int> BST = new BinarySearchTree<int>();
            AVL<int> AVLTree = new AVL<int>();

            tObj.startTime();
            for (int i = 0; i < numberOfItems; i++)
                BST.Add(i);
            tObj.StopTime();
            Console.WriteLine("Time for BST add operation:" + tObj.Result());
            tObj.startTime();
            for (int i = 0; i < numberOfItems*2; i++)
                BST.Contains(i);
            tObj.StopTime();
            Console.WriteLine("Time for BST contains operation:" + tObj.Result());

            tObj.startTime();
            for (int i = 0; i < numberOfItems; i++)
                AVLTree.Add(i);
            tObj.StopTime();
            Console.WriteLine("Time for AVL add operation:" + tObj.Result());
            tObj.startTime();
            bool Contains_Result;
            for (int i = 0; i < numberOfItems*2; i++)
                Contains_Result = AVLTree.Contains(i);
            tObj.StopTime();
            Console.WriteLine("Time for AVL contains operation:" + tObj.Result());

            Console.WriteLine("The height of the AVL Tree before Delete operations is " + AVLTree.getHeight());

            for (int i = 0; i < numberOfItems-2; i+=1)
                AVLTree.Delete(i);
  
            //for (int i = 0; i < numberOfItems; i++)
            //    Console.WriteLine("AVL Tree Cotains: " + i + " ?" + AVLTree.Contains(i));

            Console.WriteLine("The height of the AVL Tree after Delete operations is " + AVLTree.getHeight());

           
        }

        public class Timing
        {
            TimeSpan startingTime;
            TimeSpan duration;
            public Timing()
            {
                startingTime = new TimeSpan(0);
                duration = new TimeSpan(0);
            }
            public void StopTime()
            {
                duration =
                Process.GetCurrentProcess().Threads[0].
                UserProcessorTime.Subtract(startingTime);

            }

            public void startTime()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                startingTime =
                Process.GetCurrentProcess().Threads[0].
                UserProcessorTime;
            }

            public TimeSpan Result()
            {
                return duration;
            }
        }
    }
}
