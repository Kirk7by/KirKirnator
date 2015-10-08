using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNW
{
    // Класс - нейронная сеть
    public class NeuralNW
    {
        // Вход 
        public class Input
        {
            // Связи с нейронами
            public Link[] OutgoingLinks;
        }

        // Связь входа с нейроном
        public class Link
        {
            // Нейрон
            public Neuron Neuron;
            // Вес связи
            public double Weight;
        }

        public class Neuron
        {
            //Все входы нейрона
            public Link[] IncomingLinks;
            // Накопленный нейроном заряд 
            public double Power { get; set; }
        }
    }
}
