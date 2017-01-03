using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygaxian_Map_Generator_v0._2
{
    class Cell
    {
        #region Properties
        private static List<string> Initial = new List<string> { "Plain", "Scrub", "Forest", "Rough", "Desert", "Hills", "Mountains", "Marsh" };
        private static string[] Terrains = new string[] {
        "Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Plain,Scrub,Forest,Rough,Desert,Hills,Mountains,Marsh",
        "Plain,Plain,Plain,Scrub,Scrub,Scrub,Scrub,Scrub,Scrub,Scrub,Scrub,Scrub,Scrub,Forest,Forest,Rough,Desert,Hills,Mountains,Marsh",
        "Plain,Scrub,Scrub,Scrub,Forest,Forest,Forest,Forest,Forest,Forest,Forest,Forest,Forest,Forest,Forest,Rough,Hills,Mountains,Marsh",
        "Plain,Plain,Scrub,Scrub,Rough,Rough,Rough,Rough,Rough,Desert,Desert,Hills,Hills,Hills,Hills,Mountains,Mountains,Marsh",
        "Plain,Plain,Plain,Scrub,Scrub,Rough,Rough,Rough,Desert,Desert,Desert,Desert,Desert,Desert,Desert,Hills,Mountains,Mountains,Marsh",
        "Plain,Scrub,Scrub,Forest,Forest,Rough,Rough,Desert,Hills,Hills,Hills,Hills,Hills,Hills,Hills,Hills,Mountains,Mountains,Marsh",
        "Plain,Scrub,Forest,Rough,Rough,Desert,Hills,Hills,Hills,Hills,Mountains,Mountains,Mountains,Mountains,Mountains,Mountains,Mountains,Mountains,Mountains,Mountains",
        "Plain,Plain,Scrub,Scrub,Forest,Forest,Rough,Hills,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh,Marsh"
        };


        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        Vector2 index;
        public Vector2 Index
        {
            get { return index; }
        }

        Rectangle rect;
        public Rectangle Rectangle
        {
            get { return rect; }
        }

        Color Color;

        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
        }

        public bool Processed;
        int size;
        public string Terrain;

        #endregion
        #region Constructor
        public Cell(Vector2 Pos, int cellSize, GraphicsDevice GFX)
        {
            this.position.X = Pos.X * cellSize;
            this.position.Y = Pos.Y * cellSize;
            this.index = Pos;
            this.Processed = false;
            this.size = cellSize;

            rect = new Rectangle(position.ToPoint(), new Point(cellSize));
            Color = Color.Black;
            texture = new Texture2D(GFX, cellSize, cellSize);
            SetColorTexture(Color, cellSize);
        }
        #endregion
        #region Functions
        public void Update()
        {
            if (this.Processed)
            {
                if (this.Terrain == "Plain")
                {
                    SetColorTexture(Color.LawnGreen, this.size);
                }
                else if (this.Terrain == "Scrub")
                {
                    SetColorTexture(Color.Green, this.size);
                }
                else if (this.Terrain == "Forest")
                {
                    SetColorTexture(Color.ForestGreen, this.size);
                }
                else if (this.Terrain == "Rough")
                {
                    SetColorTexture(Color.Purple, this.size);
                }
                else if (this.Terrain == "Desert")
                {
                    SetColorTexture(Color.SandyBrown, this.size);
                }
                else if (this.Terrain == "Hills")
                {
                    SetColorTexture(Color.DarkGray, this.size);
                }
                else if (this.Terrain == "Mountains")
                {
                    SetColorTexture(Color.Gray, this.size);
                }
                else if (this.Terrain == "Marsh")
                {
                    SetColorTexture(Color.SaddleBrown, this.size);
                }
                else
                {
                    Debug.Print(this.Terrain + " NOT SET");
                }
            }
        }

        public void SetColorTexture(Color color, int size)
        {
            this.Color = color;
            Color[] colorData = new Color[size * size];
            for (int i = 0; i < size * size; i++)
            {
                colorData[i] = Color;
            }
            this.texture.SetData<Color>(colorData);
        }

        public Cell checkNeighbours(Cell[,] Cells, int Cols, int Rows)
        {
            List<Cell> result = new List<Cell>();

            //Top
            if (Index.Y - 1 >= 0)
                if(!Cells[(int)Index.X, (int)Index.Y - 1].Processed)
                    result.Add(Cells[(int)Index.X, (int)Index.Y - 1]);
            //Left
            if (Index.X - 1 >= 0)
                if(!Cells[(int)Index.X - 1, (int)Index.Y].Processed)
                result.Add(Cells[(int)Index.X - 1, (int)Index.Y]);
            //Bottom
            if (Index.Y+1<Rows)
                if (!Cells[(int)Index.X, (int)Index.Y + 1].Processed)
                    result.Add(Cells[(int)Index.X, (int)Index.Y + 1]);
            //Right
            if(Index.X+1<Rows)
                if (!Cells[(int)Index.X + 1, (int)Index.Y].Processed)
                    result.Add(Cells[(int)Index.X + 1, (int)Index.Y]);
            ////TopLeft
            //if (Index.Y - 1 >= 0 && Index.X - 1 >= 0)
            //    if (!Cells[(int)Index.X-1, (int)Index.Y - 1].Processed)
            //        result.Add(Cells[(int)Index.X-1, (int)Index.Y - 1]);
            ////TopRight
            //if (Index.Y - 1 >= 0 && Index.X + 1 < Rows)
            //    if (!Cells[(int)Index.X + 1, (int)Index.Y - 1].Processed)
            //        result.Add(Cells[(int)Index.X+1, (int)Index.Y - 1]);
            ////BottomLeft
            //if(Index.Y + 1 < Rows && Index.X - 1 >= 0)
            //    if (!Cells[(int)Index.X-1, (int)Index.Y + 1].Processed)
            //    result.Add(Cells[(int)Index.X-1, (int)Index.Y + 1]);
            ////BottomRight
            //if (Index.Y + 1 < Rows && Index.X + 1 < Rows)
            //    if (!Cells[(int)Index.X + 1, (int)Index.Y + 1].Processed)
            //        result.Add(Cells[(int)Index.X+1, (int)Index.Y + 1]);

            if (result.Count == 0)
                return null;
            int i = StaticRandom.Instance.Next(0, result.Count());
            return result[i];
        }

        public void RollInitial()
        {
            Terrain = Initial[StaticRandom.Instance.Next(Initial.Count)];
            Terrain = RollNext();
        }

        public string RollNext()
        {
            string[] terrainType = Terrains[Initial.IndexOf(this.Terrain)].Split(',');
            this.Processed = true;
            return terrainType[StaticRandom.Instance.Next(terrainType.Length)];

        }
        #endregion
    }
}
