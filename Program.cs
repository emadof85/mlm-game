using Multi_level_Maze;

MazeIO mio = new MazeIO();
int levels = mio.intReader(false, "Fill in the number of level > 0: ", 1);
Console.WriteLine("------------------\n");
int x = mio.intReader(false, "Fill in x slots count", 2);
int y = mio.intReader(false, "Fill in y slots count", 2);
mio = new MazeIO(levels, x, y);

int[][][] maze = mio.readMaze();

mio.printMaze();
/*int[][][] maze = new int[][][] {
    new int[][] {
        new int[] {0, -1, 0},
        new int[] {0, 0, -1},
        new int[] { 0, -1, 0},
        new int[] { 0, -1, 0}
    },
    new int[][] {
        new int[] {0, -1, 0},
        new int[] {0, 0, 0},
        new int[] {0, 0, -1},
        new int[] {0, -1, 0}
    },
    new int[][] {
        new int[] {0, 0, 0},
        new int[] {0, 0, -1},
        new int[] {0, -1, 0},
        new int[] {0, 0, 0}
    }
};*/
int[] start = new int[] { 0, 0, 0};
int[] end = new int[] { 3, 2, 1};
AStarMultiLevel aStar = new AStarMultiLevel(maze, start, end);
List<int[]> path = aStar.FindPath();
if (path != null)
{
    foreach (int[] point in path)
    {
        Console.WriteLine("(" + point[0] + ", " + point[1] + ", " + point[2] + ")");
    }
}
else
{
    Console.WriteLine("No path found");
}

