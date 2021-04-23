using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleMiner
{

    class PetShop
    {
        public KittyHouse[] KittyHouses { get; set; }
        public int KittyHouseCount { get; set; } = default;
        public decimal Income { get; private set; } = 0;
        public void AddKittyHouse(ref KittyHouse kittyh)
        {
            KittyHouse[] temp = new KittyHouse[++KittyHouseCount];
            if (KittyHouses != null)
            {
                KittyHouses.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = kittyh;
            KittyHouses = temp;
        }
        public void ShowHouses()
        {
            foreach (var houses in KittyHouses)
            {
                houses.ShowKitties();
            }
        }
        public void CalculateIncome()
        {
            if (KittyHouses != null)
            {
                foreach (var item in KittyHouses)
                {
                    Income += item.GetTotalIncome();
                }
            }
        }

    }
    class KittyHouse
    {
        public Kitty[] Kitties { get; set; }
        public int KittyCount { get; set; } = default;
        public void AddKitty(ref Kitty kitty)
        {
            Kitty[] temp = new Kitty[++KittyCount];
            if (Kitties != null)
            {
                Kitties.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = kitty;
            Kitties = temp;
        }
        private int Find(string name)
        {
            int index = -1;
            for (int k = 0; k < KittyCount; k++)
            {
                if (Kitties[k].Nickname == name)
                {
                    index = k;
                    break;
                }
            }
            return index;
        }
        public void DeleteKittyByNickname(string name)
        {
            Kitty[] destination = new Kitty[KittyCount - 1];
            int index = Find(name);

            if (Kitties != null && index != -1)
            {
                Array.Copy(Kitties, 0, destination, 0, index);
            }
            if (index < KittyCount - 1)
            {
                Array.Copy(Kitties, index + 1, destination, index, KittyCount - index - 1);
            }
            Kitties = destination;
        }

        public void ShowKitties()
        {
            Console.WriteLine("++++++++++++++HOUSE++++++++++++++");
            if (Kitties != null)
            {
                foreach (var item in Kitties)
                {
                    item.Show();
                }
            }
        }
        public decimal GetTotalIncome()
        {
            decimal total = 0;
            if (Kitties != null)
            {
                foreach (var kitties in Kitties)
                {
                    total += kitties.Price;
                }
            }
            return total;
        }
    }
    class Kitty
    {
        public string Nickname { get; set; }
        public int Age { get; set; }
        public int Energy { get; set; } = 100;
        public decimal Price { get; private set; } = default;

        public void Sleep()
        {
            Console.WriteLine($"{Nickname} is sleeping");
            Energy += 50;
        }
        public void Eat()
        {
            if (Energy > 0)
            {
                Price += 5;
                Energy += 1;
            }
            else
            {
                Energy = 0;
                Sleep();
            }
        }
        public void Play()
        {
            if (Energy > 0)
            {
                Energy -= 5;
            }
            else
            {
                Energy = 0;
                Sleep();
            }
        }
        public void Show()
        {
            Console.WriteLine("+++++++++++++++++KITTY+++++++++++++++++");
            Console.WriteLine();
            Console.WriteLine($"Nickname: {Nickname}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Energy: {Energy}");
            Console.WriteLine($"Price: {Price}");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Kitty w1 = new Kitty
            {
                Nickname = "Snow",
                Age = 2
            };
            Kitty w2 = new Kitty
            {
                Nickname = "Night",
                Age = 2
            };
            Kitty w3 = new Kitty
            {
                Nickname = "Sun",
                Age = 0
            };
            Kitty w4 = new Kitty
            {
                Nickname = "Moon",
                Age = 3
            };
            KittyHouse kh = new KittyHouse();
            kh.AddKitty(ref w1);
            kh.AddKitty(ref w3);

            KittyHouse kh1 = new KittyHouse();
            kh1.AddKitty(ref w2);
            kh1.AddKitty(ref w4);

            PetShop petShop = new PetShop();

            petShop.AddKittyHouse(ref kh);
            petShop.AddKittyHouse(ref kh1);

            for (int i = 0; i < 20; i++)
            {
                w2.Eat();
                w3.Eat();
                w2.Play();
                w3.Play();
            }

            petShop.CalculateIncome();
            Console.WriteLine(petShop.Income);

            //kh1.DeleteKittyByNickname("Night");

            //Console.WriteLine("After Delete");
            //Console.WriteLine();

            //kh1.ShowKitties();
        }
    }
}


