using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace ConsoleApplication77
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many elements you want to sort??");
           int numofelements=Convert.ToInt16(Console.ReadLine());
            //Array declaration
            string[] str = new string[numofelements];
            //Array input
            for (int i = 0; i < numofelements; i++)
            {
                Console.Write("[{0}]: ",i+1);
                str[i] = Console.ReadLine();
            }
            //Catching the sorted elements returned from smoothsort method
            string[] sorted=SmoothSort(str, str.Length);
          //Printing sorted elements
            Console.WriteLine("Sorted Array:");
            for (int k = 0; k < sorted.Length; k++)
            {
                Console.Write("[{0}]: ",k+1);
                Console.WriteLine(sorted[k]);
            }

            
        }
       
        static public string[] con(int[] array)
        {
            //Storing length of unsorted array
            int a = array.Length;
            //Storing elements in a new array
            string[] n = new string[a];
            for (int x = 0; x < n.Length;x++ )
            {
                n[x] = Convert.ToString(array[x]);
            }
            return n;
        }
        
         
        private static bool Ascending(string first, string second)
        {
            
         //Comapring the strings
            return (StringType.StrCmp(first, second, false) <= 0);
        }

        private static void UP(ref int IA, ref int IB, ref int a)
        {
            a= IA;
            IA += IB + 1;
            IB = a;
        }

        private static void DOWN(ref int IA, ref int IB, ref int b)
        {
            b= IB;
            IB = IA - IB - 1;
            IA = b;
        }

        private static int q, r, p, b, c, r1, b1, c1;
        private static string[] A;

        private static void Sift()
        {
            int r0, r2, d = 0;
            string t;
            r0 = r1;
            t = A[r0];

            while (b1 >= 3)
            {
                r2 = r1 - b1 + c1;

                if (!Ascending(A[r1 - 1], A[r2]))
                {
                    r2 = r1 - 1;
                    DOWN(ref b1, ref c1, ref d);
                }

                if (Ascending(A[r2], t))
                {
                    b1 = 1;
                }
                else
                {
                    A[r1] = A[r2];
                    r1 = r2;
                    DOWN(ref b1, ref c1, ref d);
                }
            }

            if (Convert.ToBoolean(r1 - r0))
                A[r1] = t;
        }

        private static void Trinkle()
        {
            int a1, r2, r3, r0, d1 = 0;
            string t;
            a1= p;
            b1 = b;
            c1 = c;
            r0 = r1;
            t = A[r0];

            while (a1 > 0)
            {
                while ((a1 & 1) == 0)
                {
                    a1 >>= 1;
                    UP(ref b1, ref c1, ref d1);
                }

                r3 = r1 - b1;

                if ((a1 == 1) || Ascending(A[r3], t))
                {
                    a1 = 0;
                }
                else
                {
                    --a1;

                    if (b1 == 1)
                    {
                        A[r1] = A[r3];
                        r1 = r3;
                    }
                    else
                    {
                        if (b1 >= 3)
                        {
                            r2 = r1 - b1 + c1;

                            if (!Ascending(A[r1 - 1], A[r2]))
                            {
                                r2 = r1 - 1;
                                DOWN(ref b1, ref c1, ref d1);
                                 
                            }
                            if (Ascending(A[r2], A[r3]))
                            {
                                A[r1] = A[r3]; r1 = r3;
                            }
                            else
                            {
                                A[r1] = A[r2];
                                r1 = r2;
                                DOWN(ref b1, ref c1, ref d1);
                                a1 = 0;
                            }
                        }
                    }
                }
            }

            if (Convert.ToBoolean(r0 - r1))
                A[r1] = t;

            Sift();
        }

        private static void SemiTrinkle()
        {
            string T;
            r1 = r - c;

            if (!Ascending(A[r1], A[r]))
            {
                T = A[r];
                A[r] = A[r1];
                A[r1] = T;
                Trinkle();
            }
        }

        public static string[] SmoothSort(string[] array, int N)
        {
            int e = 0;
            A = array;
            q = 1;
            r = 0;
            p = 1;
            b = 1;
            c = 1;

            while (q < N)
            {
                r1 = r;
                if ((p & 7) == 3)
                {
                    b1 = b;
                    c1 = c;
                    Sift();
                    p = (p + 1) >> 2;
                    UP(ref b, ref c, ref e);
                    UP(ref b, ref c, ref e);
                }
                else if ((p & 3) == 1)
                {
                    if (q + c < N)
                    {
                        b1 = b;
                        c1 = c;
                        Sift();
                    }
                    else
                    {
                        Trinkle();
                    }

                    DOWN(ref b, ref c, ref e);
                    p <<= 1;

                    while (b > 1)
                    {
                        DOWN(ref b, ref c, ref e);
                        p <<= 1;
                    }

                    ++p;
                }

                ++q;
                ++r;
            }

            r1 = r;
            Trinkle();

            while (q > 1)
            {
                --q;

                if (b == 1)
                {
                    --r;
                    --p;

                    while ((p & 1) == 0)
                    {
                        p >>= 1;
                        UP(ref b, ref c, ref e);
                    }
                }
                else
                {
                    if (b >= 3)
                    {
                        --p;
                        r = r - b + c;
                        if (p > 0)
                            SemiTrinkle();

                        DOWN(ref b, ref c, ref e);
                        p = (p << 1) + 1;
                        r = r + c;
                        SemiTrinkle();
                        DOWN(ref b, ref c, ref e);
                        p = (p << 1) + 1;
                    }
                }
            }
            return array;
        }
        }
    }