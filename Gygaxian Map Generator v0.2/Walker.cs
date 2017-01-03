using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygaxian_Map_Generator_v0._2
{
    class Walker
    {
        Cell Current;
        Cell Next;
        public bool Started;
        Stack<Cell> Stack;

        public Walker()
        {
            Stack = new Stack<Cell>();
        }

        public void Start(Cell Start)
        {
            Current = Start;
            Started = true;
            Current.RollInitial();
        }

        public void Update(Cell[,] Cells, int Cols, int Rows)
        {
            if (Current != null)
            {
                Current.Processed = true;
                Current.Update();
                Next = Current.checkNeighbours(Cells, Cols, Rows);
                if (Next != null)
                {
                    Next.Processed = true;
                    Stack.Push(Current);

                    Next.Terrain = Current.RollNext();

                    Current = Next;
                }
                else if (Stack.Count > 0)
                {
                    Current = Stack.Pop();
                }
            }
        }
    }
}
