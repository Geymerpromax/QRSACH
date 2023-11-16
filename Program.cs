using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        Hashtable hashTable = new Hashtable();
        string inCollection = "";
        char[] znaki = new char[] { '.', ',', '!', '&','?', ' ' };
        int i = 1;
        var array = new List<string>();
        Console.WriteLine("Составляем хеш-таблицу...");
        foreach (string line in ReadTxtFile())
        {
            string[] words = line.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
           
            foreach (string word in words)
            {
                int asd = word.IndexOfAny(znaki);
                string word2 = word;
                if (asd > 0)
                {
                    word2 = word.Remove(asd, 1).ToLower();
                }
                array.Add(word2);
                if (hashTable.ContainsKey(GetSHA256Hash(word2)))
                {                    
                    string a = hashTable[GetSHA256Hash(word2)].ToString();
                    inCollection += a;
                    inCollection += ", ";
                    inCollection += i;
                    hashTable.Remove(GetSHA256Hash(word2));
                    hashTable.Add(GetSHA256Hash(word2), inCollection);
                    inCollection = "";
                }
                else
                {
                    inCollection = i.ToString();
                    hashTable.Add(GetSHA256Hash(word2), inCollection);
                    inCollection = "";
                }
                i++;
                word2 = " ";
            }           
        }
        Console.WriteLine("Ведите слово для поиска хешированием:");
        string search = Console.ReadLine();
        if (hashTable.ContainsKey(GetSHA256Hash(search)))
        {
            string zxc = "";
            zxc = hashTable[GetSHA256Hash(search)].ToString();
            Console.WriteLine(zxc);           
        }
        else
        {
            Console.WriteLine("Слово не найдено");
        }

        Console.WriteLine("Ведите слово для поиска перебором:");
        search = Console.ReadLine();
        int count = 0;
        for (int k = 0; k < array.Count; k++)
        {
            if (array[k] == search)
            {
                Console.Write(k+1 + ", ");
                count++;
            }
        }
        if (count == 0)
        {
            Console.WriteLine("Слово не найдено");
        }

        Console.WriteLine("Создаём массив случайных чисел от 0 до 10...");
        var rnd = new Random();
        var arrNum = new List<string>();
        for (int h = 0; h < 10000000; h++)
        {
            arrNum.Add(rnd.Next(0, 1000).ToString());
           
        }
        Console.WriteLine("Готово");
        
        Console.WriteLine("Создаём хеш-таблицу для маасива чисел...");
        Hashtable hashTableNum = new Hashtable();

        hashTableNum = LoadJsonInHashTable();

        i = 1;
        //foreach (string num in arrNum)
        //{
        //    if (hashTableNum.ContainsKey(GetSHA256Hash(num)))
        //    {
        //        string a = hashTableNum[GetSHA256Hash(num)].ToString();
        //        inCollection += a;
        //        inCollection += ", ";
        //        inCollection += i;
        //        hashTableNum.Remove(GetSHA256Hash(num));
        //        hashTableNum.Add(GetSHA256Hash(num), inCollection);
        //        inCollection = "";
        //    }
        //    else
        //    {
        //        inCollection = i.ToString();
        //        hashTableNum.Add(GetSHA256Hash(num), inCollection);
        //        inCollection = "";
        //    }
        //    i++;            
        //    Console.WriteLine("Захешировано " + i + "из" + 10000000);
            
        //}
        Console.WriteLine("Готово");

        //SaveHashTableInJson(hashTableNum);
        //Console.WriteLine("Таблица сохранена");

        Console.WriteLine("Ведите цифру для поиска хешированием:");
        search = Console.ReadLine();
        if (hashTableNum.ContainsKey(GetSHA256Hash(search)))
        {
            string zxc = "";
            zxc = hashTableNum[GetSHA256Hash(search)].ToString();
            Console.WriteLine(zxc);
        }
        else
        {
            Console.WriteLine("Позиция цифры не найдено");
        }
        Console.WriteLine("Поиск хешированеим завершен\n\n");

        Console.WriteLine("Ведите цифру для поиска перебором:");
        search = Console.ReadLine();
        count = 0;
        string value2 = "";
        for (int k = 0; k < arrNum.Count; k++)
        {
            if (arrNum[k] == search)
            {
                value2 +=(k + 1 + ", ");
                count++;
            }
        }
        Console.WriteLine(value2);
        if (count == 0)
        {
            Console.WriteLine("Слово не найдено");
        }
        Console.WriteLine("Поиск перебором завершен");

    }

    public static string GetSHA256Hash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Преобразуем входную строку в массив байтов
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // Вычисляем хеш-код
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);

            // Преобразуем хеш-код в строку шестнадцатеричного представления
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    static List<string> ReadTxtFile(string fileName = "C:\\Users\\Руслан\\Desktop\\Программинг\\1.txt")
    {       
        List<string> words = new List<string>();
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] lineWords = line.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            words.AddRange(lineWords);
        }
       return words;
    }

    static void SaveHashTableInJson(Hashtable hashtable, string path = "C:\\Users\\Руслан\\Desktop\\Программинг\\hashNum.json")
    {
        string json = JsonConvert.SerializeObject(hashtable);

        // Записываем json в файл
        File.WriteAllText(path, json);
    }

    static Hashtable LoadJsonInHashTable(string path = "C:\\Users\\Руслан\\Desktop\\Программинг\\hashNum.json")
    {
        string json = File.ReadAllText(path);

        Hashtable hashtable = JsonConvert.DeserializeObject<Hashtable>(json);

        return hashtable;
    }

}