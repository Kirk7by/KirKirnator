using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//иницилизация нейрона
namespace NeuralCore
{
    // Класс - слой нейросети
    public class LayerNW
    {
        // Тут у нас хранятся веса
        double[,] Weights;
        // Число входов и выходов в слое
        int cX, cY;

        // Заполняем веса случайными числами
        public void GenerateWeights()
        {

        }

        // Выделяет память под веса
        protected void GiveMemory()
        {

        }

        // Конструктор с параметрами. передается количество входных и выходных нейронов
        public LayerNW(int countX, int countY)
        {

        }

        // Возвращаем или устанавливаем число входов
        public int countX() { return 0; }
        // Возвращаем или устанавливаем число выходов 
        public int countY() { return 0; }
        // Возвращаем или устанавливаем вес в заданной связи
    }
}

  