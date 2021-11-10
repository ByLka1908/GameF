using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardF
{
    struct Map
    {
        int size;
        int[,] map;

        /// <summary>
        /// Создаем игровое поле
        /// </summary>
        /// <param name="size"></param>
        public Map(int size)
        {
            this.size = size;
            map = new int[size, size];
        }

        /// <summary>
        /// Задать карту
        /// </summary>
        /// <param name="xy"></param>
        /// <param name="value"></param>
        public void Set(Coord xy, int value)
        {
            if (xy.OnBoard(size))
            {
                map[xy.x, xy.y] = value;
            }
        }

        /// <summary>
        /// Получить карту
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        public int Get(Coord xy)
        {
            if (xy.OnBoard(size))
                return map[xy.x, xy.y];
            return 0;
        }

        public void Copy(Coord from, Coord to)
        {
            Set(to, Get(from));
        }
    }
}