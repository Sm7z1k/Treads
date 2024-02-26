using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car
{
    internal class Engeen
    {
        static readonly double MIN_CONSUMPTION = 3;
        static readonly double MAX_CONSUMPTION = 30;
        double consumption;
        double consumption_per_second;
        public bool Started { get; private set; }
        public double Consumption
        {
            get => consumption;
            set
            {
                if (value < MIN_CONSUMPTION) value = MIN_CONSUMPTION;
                if (value > MAX_CONSUMPTION) value = MAX_CONSUMPTION;
                this.consumption = value;
                setConsuptionPerSecond();
            }
        }
        public double ConsumptionPerSecond
        {
            get => consumption_per_second;
        }
        void setConsuptionPerSecond()
        {
            consumption_per_second = consumption * 3e-5;
        }
        public void Start()
        {
            Started = true;
        }
        public void Stop()
        {
            Started = false;
        }
        
        public Engeen(double consumption)
        {
            Consumption = consumption;
            Started = false;
        }
        public void Info()
        {
            Console.WriteLine($"Consumption per second 100 km: {Consumption}");
            Console.WriteLine($"Consumption per second 1  sec: {ConsumptionPerSecond}");
        }
    }
}
