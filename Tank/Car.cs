using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Car
{
    internal class Car
    {
        static readonly int MAX_SPEED_LOW_LIMIT = 100;
        static readonly int MAX_SPEED_HIGHT_LIMIT = 400;
        public int MAX_SPEED { get; }
        Engeen engeen;
        Tank tank;
        bool driver_inside;

        struct Threads
        {
            public Thread PanelThread { get; set; }
        }
        Threads threads;
        public Car(double consumption, int volume, int max_speed = 250)
        {
            engeen = new Engeen(consumption);
            tank = new Tank(volume);
            driver_inside = false;
            threads = new Threads();
            if (max_speed < MAX_SPEED_LOW_LIMIT) max_speed = MAX_SPEED_LOW_LIMIT;
            if (max_speed < MAX_SPEED_HIGHT_LIMIT) max_speed = MAX_SPEED_HIGHT_LIMIT;
            this.MAX_SPEED = max_speed;
        }
        public void GetIn()
        {
            driver_inside = true;
            threads.PanelThread = new Thread(Panel);
            threads.PanelThread.Start();
        }
        public void GetOut()
        {
            driver_inside = false;
            threads.PanelThread.Join();
            Console.Clear();
            Console.WriteLine("You are out of the car");
        }
        public void Control()
        {
            Console.WriteLine("Your car is ready, press Enter to get info");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (driver_inside) GetOut();
                        else GetIn();
                        break;
                    case ConsoleKey.Escape:
                        GetOut();
                        break;

                    case ConsoleKey.F:
                        if (!driver_inside)
                        {
                            Console.Write("Введите объём топлива: ");
                            double amount = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
                            tank.Fill(amount);
                        }
                        else
                        {
                            Console.WriteLine("Get out of the car");
                        }
                        break;
                    case ConsoleKey.E:
                        if (engeen.Started)
                        {
                            engeen.Stop();
                            Console.WriteLine("Двигатель заглушен");
                        }
                        else
                        {
                            engeen.Start();
                            Console.WriteLine("Двигатель заведён");
                        }
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }        
        void Panel()
        {
            while (driver_inside)
            {
                Console.Clear();
                Console.WriteLine($"Fuel level: {tank.FuelLevel} liters");
                Console.WriteLine($"Engeen is {(engeen.Started ? "started" : "stopped")}");
                Console.WriteLine($"Speed {MAX_SPEED}");
                if (tank.FuelLevel > 0 && engeen.Started)
                {
                    if (tank.FuelLevel <= 5)
                    {
                        Console.WriteLine("LOW FUEL");
                    }
                    tank.Vibros(engeen.ConsumptionPerSecond);
                    Thread.Sleep(100);
                }
                else
                {
                    engeen.Stop();
                    Console.WriteLine("Двигатель заглушен из-за нехватки топлива");
                }
                Thread.Sleep(100);
            }
        }
        //void Go()
        //{
        //    if (tank.FuelLevel > 0 && engeen.Started)
        //    {
        //        if (tank.FuelLevel <= 5)
        //        {
        //            Console.WriteLine("LOW FUEL");
        //        }
        //        tank.Fill(1);
        //        Thread.Sleep(100);
        //    }
        //    else
        //    {
        //        engeen.Stop();
        //        Console.WriteLine("Двигатель заглушен из-за нехватки топлива");                
        //    }
        //}
        public void Info()
        {
            engeen.Info();
            tank.Info();
            Console.WriteLine($"Max speed: {MAX_SPEED}km/h");
        }
    }
}
