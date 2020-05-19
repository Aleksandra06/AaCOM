using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace lab3_Transport_task
{
    //-1 - не проверен
    //-2 - проверен и исключен
    //другое положительное - учитывается (множитель)
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Номер примера (1-10): ");
            var num = Console.ReadLine();
            string path = @"matrix\" + num + ".txt";
            var table = Read(path);
            if (table == null) return;
            Print(table, true);
            table = Norm(table);
            Print(table, true);
            TransportTask(table);
            Answer(table);
        }

        private static int Answer(List<List<Tuple<int, int>>> table)
        {
            string str = "Z = ";
            int answer = 0;
            int count = 0;
            foreach (var tstr in table)
            {
                foreach (var tcl in tstr)
                {
                    if (tcl.Item2 >= 0)
                    {
                        str += " + " + tcl.Item1 + " * " + tcl.Item2;
                        answer += tcl.Item1 * tcl.Item2;
                        count++;
                    }
                }
            }
            str += " = " + answer;
            Console.WriteLine(str);
            str = (table.Count - 1) + " + " + (table[0].Count - 1) + " - " + 1;
            if (count == table.Count + table[0].Count - 1 - 2)
            {
                str += " == " + count + " => невырожденная";
            }
            else
            {
                str += " != " + count + " => вырожденная";
            }
            Console.WriteLine(str);
            return answer;
        }

        private static List<List<Tuple<int, int>>> Norm(List<List<Tuple<int, int>>> table)
        {
            int sumStr = 0, sumCol = 0;
            foreach (var t in table.LastOrDefault())
            {
                sumStr += t.Item1;
            }
            for (int i = 0; i < table.Count - 1; i++)
            {
                sumCol += table[i].LastOrDefault().Item1;
            }
            if (sumCol < sumStr)
            {
                List<Tuple<int, int>> newlist = new List<Tuple<int, int>>();
                for (int i = 0; i < table[0].Count - 1; i++)
                {
                    newlist.Add(new Tuple<int, int>(0, -1));
                }
                newlist.Add(new Tuple<int, int>(sumStr - sumCol, -1));
                table.Insert(table.Count - 1, newlist);
                Console.WriteLine("Открытая\n");
            }
            else
            {
                Console.WriteLine("Закрытая\n");
            }
            return table;
        }

        static void TransportTask(List<List<Tuple<int, int>>> table)
        {
            while (!Check(table))
            {
                Tuple<int, int> minId = SearchMinId(table);
                var min = Math.Min(table[minId.Item1].LastOrDefault().Item1, table[table.Count - 1][minId.Item2].Item1);
                if (table[minId.Item1].LastOrDefault().Item1 - min == 0) //строка
                {
                    for (int i = 0; i < table[minId.Item1].Count - 1; i++)
                    {
                        if (minId.Item2 == i)
                        {

                            table[minId.Item1][i] = new Tuple<int, int>(table[minId.Item1][i].Item1, min);
                            continue;
                        }
                        if (table[minId.Item1][i].Item2 != -1)
                        {
                            continue;
                        }
                        table[minId.Item1][i] = new Tuple<int, int>(table[minId.Item1][i].Item1, -2);
                    }
                }
                else //столбец
                {
                    for (int i = 0; i < table.Count - 1; i++)
                    {
                        if (minId.Item1 == i)
                        {
                            table[i][minId.Item2] = new Tuple<int, int>(table[i][minId.Item2].Item1, min);
                            continue;
                        }
                        if (table[i][minId.Item2].Item2 != -1)
                        {
                            continue;
                        }
                        table[i][minId.Item2] = new Tuple<int, int>(table[i][minId.Item2].Item1, -2);
                    }
                }
                table[minId.Item1][table[0].Count - 1] = new Tuple<int, int>(table[minId.Item1][table[0].Count - 1].Item1 - min, -1);
                table[table.Count - 1][minId.Item2] = new Tuple<int, int>(table[table.Count - 1][minId.Item2].Item1 - min, -1);

                Print(table, false);
            }
            Print(table, true);

        }

        private static Tuple<int, int> SearchMinId(List<List<Tuple<int, int>>> table)
        {
            int min = Int32.MaxValue;
            Tuple<int, int> minId = new Tuple<int, int>(-1, -1);
            for (int i = 0; i < table.Count - 1; i++)
            {
                for (int j = 0; j < table[i].Count - 1; j++)
                {
                    if (table[i][j].Item1 < min && table[i][j].Item1 != 0 && table[i][j].Item2 == -1)
                    {
                        minId = new Tuple<int, int>(i, j);
                        min = table[i][j].Item1;
                    }
                }
            }
            if (min == Int32.MaxValue)
            {
                for (int i = 0; i < table.Count - 1; i++)
                {
                    for (int j = 0; j < table[i].Count - 1; j++)
                    {
                        if (table[i][j].Item1 == 0 && table[i][j].Item2 == -1)
                        {
                            minId = new Tuple<int, int>(i, j);
                            min = table[i][j].Item1;
                            return minId;
                        }
                    }
                }
                return null;
            }
            return minId;
        }

        private static bool Check(List<List<Tuple<int, int>>> table)
        {
            foreach (var t in table.LastOrDefault())
            {
                if (t.Item1 != 0) return false;
            }
            for (int i = 0; i < table.Count - 1; i++)
            {
                if (table[i].LastOrDefault().Item1 != 0) return false;
            }
            return true;
        }

        static List<List<Tuple<int, int>>> Read(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                List<List<Tuple<int, int>>> num = new List<List<Tuple<int, int>>>();
                int i, j;
                for (i = 0; i < lines.Length; i++)
                {
                    List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();
                    num.Add(new List<Tuple<int, int>>());
                    string[] temp = lines[i].Split(' ');
                    for (j = 0; j < temp.Length; j++)
                        num.LastOrDefault().Add(new Tuple<int, int>(Convert.ToInt32(temp[j]), -1));
                }
                return num;
            }
            catch
            {
                Console.WriteLine("Проблемы с чтением, проверьте входные данные\n");
                return null;
            }
        }
        static void Print(List<List<Tuple<int, int>>> tuples, bool border)
        {
            string str = "";
            if (border)
            {
                str = "|\t|";
                for (int i = 0; i < tuples[0].Count - 1; i++)
                {
                    str += "B" + (i + 1) + "\t|";
                }
                str += "Запасы\t|";
                Console.WriteLine(str);
                Console.WriteLine("--------------------------------------------------------");
            }
            for (int i = 0; i < tuples.Count; i++)
            {
                if (border)
                {
                    if (i < tuples.Count - 1)
                        str = "|A" + (i + 1) + "\t|";
                    else
                        str = "|Потреб\t|";
                }
                else str = "";
                for (int j = 0; j < tuples[i].Count; j++)
                {
                    if (tuples[i][j].Item2 == -2)
                        str += "-\t|";
                    else if (tuples[i][j].Item2 == -1)
                        str += tuples[i][j].Item1 + "\t|";
                    else
                        str += tuples[i][j].Item1 + "(" + tuples[i][j].Item2 + ")\t|";

                }
                Console.WriteLine(str);
                Console.WriteLine("--------------------------------------------------------");
            }
            Console.WriteLine("\n");
        }
    }
}
