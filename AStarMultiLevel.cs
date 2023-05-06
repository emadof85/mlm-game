using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_level_Maze
{
    internal class AStarMultiLevel
    {
        private int[][][] map;
        private int[] start;
        private int[] end;
        private List<Node> openList;
        private List<Node> closedList;
        private int numLevels;

        public AStarMultiLevel(int[][][] map, int[] start, int[] end)
        {
            this.map = map;
            this.start = start;
            this.end = end;
            this.numLevels = map.Length;
            this.openList = new List<Node>();
            this.closedList = new List<Node>();
        }

        public List<int[]> FindPath()
        {
            Node startNode = new Node(start[0], start[1], start[2], null);
            Node endNode = new Node(end[0], end[1], end[2], null);
            startNode.h = CalculateH(startNode, endNode);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // Get the node with the lowest f value
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].f < currentNode.f)
                    {
                        currentNode = openList[i];
                    }
                }

                // Remove the current node from the open list and add it to the closed list
                openList.Remove(currentNode);
                closedList.Add(currentNode);
                Console.WriteLine("visited node{" + currentNode + "}");

                // Check if we've reached the end node
                if (currentNode.x == endNode.x && currentNode.y == endNode.y && currentNode.level == endNode.level)
                {
                    return GetPath(currentNode);
                }

                // Generate the neighbors of the current node
                List<Node> neighbors = GenerateNeighbors(currentNode, endNode);

                // Add the neighbors to the open list
                foreach (Node neighbor in neighbors)
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    int index = openList.IndexOf(neighbor);
                    if (index == -1)
                    {
                        neighbor.h = CalculateH(neighbor, endNode);
                        openList.Add(neighbor);
                    }
                    else if (neighbor.g < openList[index].g)
                    {
                        openList[index].g = neighbor.g;
                        openList[index].parent = currentNode;
                    }
                }
            }

            // No path found
            return null;
        }

        private int CalculateH(Node node, Node endNode)
        {
            int dx = Math.Abs(node.x - endNode.x);
            int dy = Math.Abs(node.y - endNode.y);
            int dz = Math.Abs(node.level - endNode.level);
            return dx + dy + dz;
        }

        private List<Node> GenerateNeighbors(Node node, Node endNode)
        {
            List<Node> neighbors = new List<Node>();

            // Generate the neighbors on the same level
            int[][] directions = new int[][] {
            new int[] {0, 1},
            new int[] {1, 0},
            new int[] {0, -1},
            new int[] {-1, 0}
        };
            foreach (int[] direction in directions)
            {
                int x = node.x + direction[0];
                int y = node.y + direction[1];
                if (x < 0 || x >= map[0].Length || y < 0 || y >= map[0][0].Length)
                {
                    continue;
                }
                if (map[node.level][x][y] == -1)
                {
                    continue;
                }
                Node neighbor = new Node(x, y, node.level, node);
                neighbor.g = node.g + map[node.level][x][y];
                neighbor.h = CalculateH(neighbor, endNode);
                neighbors.Add(neighbor);
            }

            // Generate the neighbors on adjacent levels
            if (map[node.level][node.x][node.y] == 0)
            {
                if (node.level > 0 && map[node.level - 1][node.x][node.y] >= 0)
                {
                    Node neighbor = new Node(node.x, node.y, node.level - 1, node);
                    neighbor.g = node.g + 1;
                    neighbor.h = CalculateH(neighbor, endNode);
                    neighbors.Add(neighbor);
                }
                if (node.level < numLevels - 1 && map[node.level + 1][node.x][node.y] >= 0)
                {
                    Node neighbor = new Node(node.x, node.y, node.level + 1, node);
                    neighbor.g = node.g + 1;
                    neighbor.h = CalculateH(neighbor, endNode);
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        private List<int[]> GetPath(Node node)
        {
            List<int[]> path = new List<int[]>();
            while (node != null)
            {
                int[] point = new int[] { node.x, node.y, node.level};
                path.Insert(0, point);
                node = node.parent;
            }
            return path;
        }
    }
}
