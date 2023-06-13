using System;

namespace redblacktree;

class Program
{
    static void Main(string[] args)
    {
        RBTree<int> rbtree = new RBTree<int>();
        int[] initArray = { 9, 7, 5, 3, 1, 0, 8, 6, 4, 2 };

        for (int i = 0; i < initArray.Length; i++)
        {
            if (rbtree.add(initArray[i]))
                Console.WriteLine($"{initArray[i]} добавилось, это хорошо!");
            else
                Console.WriteLine($"{initArray[i]} НЕ добавилось, это плохо!");
        }
        Console.WriteLine();

        for (int i = 0; i < initArray.Length; i++)
        {
            if (rbtree.contains(initArray[i]))
                Console.WriteLine($"{initArray[i]} в RBTree есть, это хорошо!");
            else
                Console.WriteLine($"{initArray[i]} в RBTree нет, это плохо!");
        }
    }
}
