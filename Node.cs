using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_level_Maze
{
    internal class Node
    {
        public int x;
        public int y;
        public int level;
        public int g;
        public int h;
        public Node parent;
        public int f
        {
            get { return g + h; }
        }

        public Node(int x, int y, int level, Node parent)
        {
            this.x = x;
            this.y = y;
            this.level = level;
            this.g = (parent != null) ? parent.g : 0;
            this.parent = parent;
        }

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            return (other != null) && (x == other.x) && (y == other.y) && (level == other.level);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ level.GetHashCode();
        }

        public override string ToString()
        {
            return "("+x+","+y+","+level+")";
        }
    }
}
