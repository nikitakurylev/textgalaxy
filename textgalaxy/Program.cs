using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceSim
{
    class Program
    {
        static int x = 50, y = 50;
        static int dx = x, dy = y;
        static string mes;
        static int power = 100, credits = 0, gold = 0;
        static int averagegold = 125, goldnum = 1;
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                if (dx == x && dy == y)
                {
                    mes += "\nWhat should I do?";
                    Update();
                    string s = Console.ReadLine();
                    switch (s.Split(' ')[0])
                    {
                        case "exit":
                            exit = true;
                            break;
                        case "mine":
                            if (getPos(x, y) == '#')
                            {
                                Random r = new Random();
                                int mined = r.Next(5);
                                gold += mined;
                                mes = "Mined " + mined + " gold";
                                power -= 10;
                            }
                            break;
                        case "sell":
                            if ((getPos(x, y) == '+'|| getPos(x, y) == 'o') && s.Length > 5)
                                switch (s.Split(' ')[1])
                                {
                                    case "gold":
                                        try
                                        {
                                            int div = 2;
                                            if (getPos(x, y) == '+')
                                                div = 1;
                                            int amount = Convert.ToInt32(s.Split(' ')[2]);
                                            if (amount <= gold)
                                            {
                                                Random r1 = new Random(x);
                                                Random r2 = new Random(y);
                                                int goldvalue = (r1.Next(25, 100) + r2.Next(25, 100));
                                                Console.WriteLine("That will be " + amount * goldvalue / div + "c. Are you sure? (yes/no)");
                                                string ss = Console.ReadLine().ToLower();
                                                if (ss == "yes" || ss == "y" || ss == "yeah")
                                                {
                                                    credits += amount * goldvalue / div;
                                                    gold -= amount;
                                                    mes = "Sold " + amount + " gold for " + amount * goldvalue / div + "c";
                                                }
                                            }
                                            else
                                                mes = "You don't have so much gold!";
                                        }
                                        catch
                                        {
                                            mes = "Use this command properly: sell <resource> <amount>";
                                        }
                                        break;
                                }
                            else
                                mes = "You can't sell anything here!";
                            break;
                        case "buy":
                            if ((getPos(x, y) == '+' || getPos(x, y) == 'o') && s.Length > 4)
                                switch (s.Split(' ')[1])
                                {
                                    case "gold":
                                        try
                                        {
                                            int div = 2;
                                            if (getPos(x, y) == '+')
                                                div = 1;
                                            Random r1 = new Random(x);
                                            Random r2 = new Random(y);
                                            int goldvalue = (r1.Next(25, 100) + r2.Next(25, 100));
                                            int amount = Convert.ToInt32(s.Split(' ')[2]);
                                            if (amount* goldvalue / div <= credits)
                                            {

                                                Console.WriteLine("That will be " + amount * goldvalue / div + "c. Are you sure? (yes/no)");
                                                string ss = Console.ReadLine().ToLower();
                                                if (ss == "yes" || ss == "y" || ss == "yeah")
                                                {
                                                    credits -= amount * goldvalue / div;
                                                    gold += amount;
                                                    mes = "Bought " + amount + " gold for " + amount * goldvalue / div + "c";
                                                }
                                            }
                                            else
                                                mes = "You don't have enought credits!";
                                        }
                                        catch
                                        {
                                            mes = "Use this command properly: sell <resource> <amount>";
                                        }
                                        break;
                                    case "power":
                                        try
                                        {
                                            int amount = Convert.ToInt32(s.Split(' ')[2]);
                                            if (amount * 15 <= credits)
                                            {

                                                Console.WriteLine("That will be " + amount * 15 + "c. Are you sure? (yes/no)");
                                                string ss = Console.ReadLine().ToLower();
                                                if (ss == "yes" || ss == "y" || ss == "yeah")
                                                {
                                                    credits -= amount * 15;
                                                    power += amount;
                                                    mes = "Bought " + amount + " power for " + amount * 15 + "c";
                                                }
                                            }
                                            else
                                                mes = "You don't have enought credits!";
                                        }
                                        catch
                                        {
                                            mes = "Use this command properly: sell <resource> <amount>";
                                        }
                                        break;
                                }
                            else
                                mes = "You can't buy anything here!";
                            break;
                        case "go":
                            if (s.Length > 3)
                                switch (s.Split(' ')[1])
                                {
                                    case "down":
                                        mes = "Went down";
                                        y++;
                                        dy++;
                                        power++;
                                        break;
                                    case "up":
                                        mes = "Went up";
                                        y--;
                                        dy--;
                                        power++;
                                        break;
                                    case "left":
                                        mes = "Went left";
                                        x--;
                                        dx--;
                                        power++;
                                        break;
                                    case "right":
                                        mes = "Went right";
                                        x++;
                                        dx++;
                                        power++;
                                        break;
                                    case "to":
                                        try
                                        {
                                            dx = Convert.ToInt32(s.Split(' ')[2]);
                                            dy = Convert.ToInt32(s.Split(' ')[3]);
                                        }
                                        catch
                                        {
                                            mes = "Use this command properly: go to <x> <y>";
                                        }
                                        break;
                                    case "random":
                                        Console.WriteLine("Please wait...");
                                        bool fin = true;
                                        while (fin)
                                        {
                                            Random r = new Random();
                                            int X, Y;
                                            X = r.Next(x - power, x + power);
                                            Y = r.Next(y - power, y + power);
                                            if (getPos(X, Y) == '+')
                                            {
                                                dx = X;
                                                dy = Y;
                                                fin = false;
                                            }
                                        }
                                        break;
                                }
                            break;
                    }

                }
                else if (power > 0)
                {
                    power--;
                    if (dx > x)
                        x++;
                    else if (dx < x)
                        x--;
                    if (dy > y)
                        y++;
                    else if (dy < y)
                        y--;
                    mes = "Travelling...";
                    Update();
                    System.Threading.Thread.Sleep(10);
                    if (power == 0)
                    {
                        dx = x;
                        dy = y;
                        mes = "Not enought power";
                    }
                }
                Console.Clear();
            }
        }
        static void Update()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("x:" + x + " y:" + y);
            for (int Y = 0; Y < 11; Y++)
                for (int X = 0; X < 11; X++)
                {
                    Console.SetCursorPosition(X * 3, 1 + Y);
                    if (X == 5 && Y == 5)
                        Console.Write("[" + getPos(x + X - 5, y + Y - 5) + "]");
                    else
                        Console.Write(" " + getPos(x + X - 5, y + Y - 5));
                }
            switch (getPos(x, y))
            {
                case 'o':
                    Random r1 = new Random(x);
                    Random r2 = new Random(y);
                    int goldvalue = (r1.Next(25, 100) + r2.Next(25, 100));
                    Console.SetCursorPosition(36, 1);
                    Console.Write("Planet '" + getName(x, y) + "'");
                    Console.SetCursorPosition(40, 2);
                    Console.Write("Buy");
                    Console.SetCursorPosition(41, 3);
                    Console.Write("Power - " + 10);
                    Console.SetCursorPosition(40, 4);
                    Console.Write("Buy/Sell");
                    Console.SetCursorPosition(41, 5);
                    Console.Write("Gold - " + goldvalue/2+"c");
                    Console.Write(" (" + (goldvalue / 2-averagegold/2/goldnum)+")");
                    Console.ResetColor();
                    averagegold += goldvalue;
                    goldnum++;
                    break;
                case '#':
                    Console.SetCursorPosition(36, 1);
                    Console.Write("Asteroid field");
                    break;
                case '+':
                    Random r11 = new Random(x);
                    Random r21 = new Random(y);
                    int goldvalue1 = (r11.Next(25, 100) + r21.Next(25, 100));
                    Console.SetCursorPosition(36, 1);
                    Console.Write("Space station '" + getName(x, y) + "'");
                    Console.SetCursorPosition(40, 2);
                    Console.Write("Buy");
                    Console.SetCursorPosition(41, 3);
                    Console.Write("Power - " + 10);
                    Console.SetCursorPosition(40, 4);
                    Console.Write("Buy/Sell");
                    Console.SetCursorPosition(41, 5);
                    Console.Write("Gold - " + goldvalue1);
                    Console.Write(" (" + (goldvalue1-averagegold/goldnum)+")");
                    Console.ResetColor();
                    //Console.SetCursorPosition(41, 4);
                    //Console.Write("Power - " + (r1.Next(1, 10) + r2.Next(1, 10)) + "c");
                    averagegold += goldvalue1;
                    goldnum++;
                    break;
            }
            Console.SetCursorPosition(0, 12);
            Console.Write("My Ship:\n    Power: " + power + "\n    Credits: " + credits + "\n    Gold: " + gold + " (~" + averagegold / goldnum * gold + "c)" + "\n" + mes + "\n>");
        }
        static char getPos(int X, int Y)
        {
            Random r1 = new Random(X);
            Random r2 = new Random(Y);
            int output = (r1.Next(1, 50) + r2.Next(1, 50)) / r1.Next(1, 50) % r2.Next(1, 50);

            if (output == r1.Next(10) + r2.Next(10))
                return 'o';
            else if (output == r1.Next(10) - r2.Next(10))
                return '#';
            else if (output == r1.Next(10) + r2.Next(10) + r1.Next(10) - r2.Next(10))
                return '+';
            else
                return '.';
        }
        static string getName(int X, int Y)
        {
            Random r1 = new Random(X);
            Random r2 = new Random(Y);
            string[] parts = new string[11] { "va", "ru", "si", "no", "ly", "fe", "ga", "bo", "ki", "hy", "oo" };
            string name = "";
            name += parts[r1.Next(5) + r2.Next(5)];
            name += parts[r1.Next(5, 10) + r2.Next(5, 10) - 10];
            name += parts[r1.Next(10, 15) + r2.Next(10, 15) - 20];
            name = name.ToUpper();
            return name;
        }
    }
}
