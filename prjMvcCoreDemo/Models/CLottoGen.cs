using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projMauiDemo.Resources.Models
{
    public class CLottoGen
    {
        public string getNumbers()
        {
            Random rand = new Random();
            int[] numbers = new int[6];
            int count = 0;
            
            while (count < numbers.Length)
            {
                int temp = rand.Next(1, 50);
                if (!is亂數是否存在於陣列(temp,numbers))
                {
                    numbers[count] = temp;
                    count++;
                }
            }

            //由小到大做排序:泡沫演算法
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    int big = numbers[j];
                    if (numbers[j] > numbers[j + 1])
                    {
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = big;
                    }
                }
            }

            string s = "Your Numbers Are:\n";
            foreach (int i in numbers)
            {
                s += i.ToString()+"       ";
            }
            return s;
            //public string getNumbers()
            //{

            //    while (lottos.Count() < maxLength)
            //    {
            //        Random rand = new Random();
            //        int index = rand.Next(1, 50);
            //        if (!lottos.Contains(index))
            //        {
            //            lottos.Add(index);
            //        }
            //    }
            //    lottos.Sort();
            //    foreach (int i in lottos)
            //    {
            //        lblLotto.Text += i.ToString() + "\n";
            //    }
            //}
        }
        private bool is亂數是否存在於陣列(int temp, int[] numbers)
        {
            foreach (int i in numbers)
            {
                if(temp == i)
                {
                    return true;
                } 
            }
            return false;
        }
    }
}
