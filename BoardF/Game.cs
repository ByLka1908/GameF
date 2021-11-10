using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardF
{
    public class Game
    {
        int size;
        public int moves { get; private set; }

        Map map;
        Coord space;

        public Game(int size)
        {
            this.size = size;
            map = new Map(size);
        }

        /// <summary>
        /// Начинаем
        /// </summary>
        /// <param name="seed"></param>
        public void Start(int seed = 0)
        {
            int digit = 0;
            foreach(Coord xy in new Coord().YeldCoord(size))
            {
                map.Set(xy, ++digit);
            }
            space = new Coord(size);
            if(seed > 0)
            {
                Shufle(seed);
                moves = 0;
            }
        } 

        /// <summary>
        /// Перемешиваем плашки
        /// </summary>
        /// <param name="seed"></param>
        void Shufle(int seed)
        {
            Random random = new Random(seed);
            for(int j = 0; j < seed; j++)
            {
                PressAt(random.Next(size), random.Next(size));
            }
        }

        public int PressAt(int x, int y)  
        {
            return PressAt(new Coord(x, y));
        }

        /// <summary>
        /// Нажатие на плашку
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        int PressAt(Coord xy)
        {
            if (space.Equals(xy))//Если нажали на пустое пространство
            {
                return 0;
            }
            if(xy.x != space.x && xy.y != space.y)//Пустое пространство по диагонали
            {
                return 0;
            }

            int steps = Math.Abs(xy.x - space.x) +
                        Math.Abs(xy.y - space.y);

            while (xy.x != space.x)
            {
                Shift(Math.Sign(xy.x - space.x), 0);
            }
            while (xy.y != space.y)
            {
                Shift(0, Math.Sign(xy.y - space.y));
            }
            moves += steps;
            return steps;
        }

        /// <summary>
        /// Перемещение 
        /// </summary>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        void Shift(int sx, int sy)
        {
            Coord next = space.Add(sx, sy);
            map.Copy(next, space); //map[space] := map[next]
            space = next;
        }

        public int GetDigitAt(int x, int y)
        {
           return GetDigitAt(new Coord(x, y));
        }

        /// <summary>
        /// Получение игрового поля
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        int GetDigitAt(Coord xy)
        {
            if (space.Equals(xy))
            {
                return 0;
            }
            return map.Get(xy);
        }

        /// <summary>
        /// Закончена ли игра
        /// </summary>
        /// <returns></returns>
        public bool IsSolved()
        {
            if (!space.Equals(new Coord(size)))
            {
                return false;
            }
            int digit = 0;
            foreach(Coord xy in new Coord().YeldCoord(size))
            {
                if (map.Get(xy) != ++digit)
                {
                    return space.Equals(xy);
                }
            }
            return true;
        }
    }
}
