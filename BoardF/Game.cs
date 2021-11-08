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

        void Shufle(int seed)//Перемешивание
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

        int PressAt(Coord xy)//Нажатие на плашки
        {
            if (space.Equals(xy))
            {
                return 0;
            }
            if(xy.x != space.x && xy.y != space.y)//Нажали по диагонали
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

        void Shift(int sx, int sy)//Перемещение
        {
            Coord next = space.Add(sx, sy);
            map.Copy(next, space); //map[space] := map[next]
            space = next;
        }

        public int GetDigitAt(int x, int y)
        {
           return GetDigitAt(new Coord(x, y));
        }

        int GetDigitAt(Coord xy)
        {
            if (space.Equals(xy))
            {
                return 0;
            }
            return map.Get(xy);
        }

        public bool IsSolved()//Все ли собранно
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
