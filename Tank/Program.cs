using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car(10, 40);
            car.Info();
            car.Control();
        }            
    }
}

    

