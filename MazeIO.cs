using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_level_Maze
{
    internal class MazeIO
    {
        private int[][][] maze;
        private int levels;
        private int x;
        private int y;

        public MazeIO()
        {
            maze = new int[][][] { }; ;
            levels = 0;
            x = 0;
            y = 0;
        }
        public MazeIO(int _levels, int _x, int _y) {
            levels = _levels;
            x = _x;
            y = _y;
            maze = new int[levels][][];
            for (var i = 0; i < levels; i++)
            {
                maze[i] = new int[x][];
                for (var j = 0; j< x; j++)
                {
                    maze[i][j] = new int[y];
                    for (var k = 0; k < y; k++)
                    {
                        maze[i][j][k] = 0;
                    }
                }
            }
            
        }
        public int intReader(bool isMaze, string msg, int lower=0)
        {
            int value;
            Console.WriteLine(msg);
            do
            {

                int.TryParse(Console.ReadLine(), out value);
                if( (isMaze && (value ==0 || value == -1)) ||
                    (!isMaze && value>=lower) )
                    break;
                Console.WriteLine("!! wrong input !!");
            }
            while ( !((isMaze && (value == 0 || value == -1)) ||
                      (!isMaze && value >= lower)) );

            return value;
        }
        public int[][][] readMaze()
        {
            for(int i = 0; i < levels; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    for (int k = 0; k < y; k++) {
                        maze[i][j][k] = intReader(true, "maze["+i+"]["+j+"]["+k+"] = ");
                    }
                }
            }

            return maze;
        }
        public void printMaze()
        {
            for( var i = 0;i < maze.GetLength(0); i++)
            {
                for(var j=0;j < maze[0].GetLength(0); j++)
                {
                    for(var k = 0;k < maze[0][0].GetLength(0); k++)
                    {
                        Console.Write(maze[i][j][k].ToString().PadLeft(2, ' '));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
