using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise_Escape
{
    public class D
    {
	    public struct vertex
	    {
		    public int value;
		    public int dist;
		    public Point location;
		    public Point prev;
	    };

	    int Width;
	    int Height;

        List<Point> _path = new List<Point>();
	    List<List<vertex>> Graph = new List<List<vertex>>();
	    const int INTSIZE = 2147483647;

        public D(List<List<int>> nodes)
        {
            Width = nodes.Count;
            Height = nodes[0].Count;

            for (int i = 0; i < Height; i++)
            {
                Graph.Add(new List<vertex>());
                for (int j = 0; j < Width; j++)
                {
                    vertex temp = new vertex();
                    temp.dist = INTSIZE;
                    temp.value = nodes[i][j];
                    temp.location = new Point(i, j);
                    Graph[i].Add(temp);
                }
            }
        }

        public int shortPath(Point startPosition)
        {
            int i = startPosition.X;
            int j = startPosition.Y;
            Queue<vertex> d = new Queue<vertex>();

            vertex here = Graph[startPosition.X][startPosition.Y];
            here.dist = 0;
            Graph[i][j] = here;
            d.Enqueue(here);
            

            while (d.Count > 0)
            {
                i = d.Peek().location.X;
                j = d.Peek().location.Y;

                here = Graph[i][j];

                bool leftEdge = (j == 0) ? true : false;
                bool rightEdge = (j == Width - 1) ? true : false;
                bool topEdge = (i == 0) ? true : false;
                bool bottomEdge = (i == Height - 1) ? true : false;

                if (!leftEdge)
                {
                    vertex left = Graph[i][j - 1];
                    if ((here.dist + left.value) < left.dist)
                    {
                        left.dist = here.dist + left.value;
                        left.prev = here.location;
                        Graph[i][j - 1] = left;
                        d.Enqueue(left);
                    }
                }
                if (!rightEdge)
                {
                    vertex right = Graph[i][j + 1];
                    if ((here.dist + right.value) < right.dist)
                    {
                        right.dist = here.dist + right.value;
                        right.prev = here.location;
                        Graph[i][j + 1] = right;
                        d.Enqueue(right);
                    }
                }
                if (!topEdge)
                {
                    vertex up = Graph[i - 1][j];
                    if ((here.dist + up.value) < up.dist)
                    {
                        up.dist = here.dist + up.value;
                        up.prev = here.location;
                        Graph[i - 1][j] = up;
                        d.Enqueue(up);
                    }
                }
                if (!bottomEdge)
                {
                    vertex down = Graph[i+1][j];
                    if ((here.dist + down.value) < down.dist)
                    {
                        down.dist = here.dist + down.value;
                        down.prev = here.location;
                        Graph[i + 1][j] = down;
                        d.Enqueue(down);
                    }
                }
                d.Dequeue();
            }
            
            Point goalPosition = new Point(0,0);
            int min = INTSIZE;
            for(int x = 0; x < Height; x++)
                for (int y = 0; y < Width; y++)
                {
                    if (x == 0 || x == Height - 1 || y == 0 || y == Width - 1)
                    {
                        if (Graph[x][y].dist < min)
                        {
                            min = Graph[x][y].dist;
                            goalPosition.X = x;
                            goalPosition.Y = y;
                        }
                    }
                }

            here = Graph[goalPosition.X][goalPosition.Y];
            int retVal = here.dist;
            while (here.location != startPosition)
            {
                i = here.location.X;
                j = here.location.Y;

                _path.Add(new Point(i,j));

                here = Graph[Graph[i][j].prev.X][Graph[i][j].prev.Y];
            }
            _path.Add(new Point(startPosition.X, startPosition.Y));

            return retVal;
        }

        public List<Point> path
        {
            get { return _path; }
        }

        public List<List<int>> dist
        {
            get 
            {
                List<List<int>> ret = new List<List<int>>();
                for (int i = 0; i < Height; i++)
                {
                    ret.Add(new List<int>());

                    for (int j = 0; j < Width; j++)
                        ret[i].Add(Graph[i][j].dist);
                }
                return ret;
            }
        }

        public int maxDist
        {
            get
            {
                int max = 0;
                for (int i = 0; i < Height; i++)
                    for (int j = 0; j < Width; j++)
                    {
                        if (dist[i][j] > max)
                            max = dist[i][j];
                    }

                return max;
            }
        }
    }
}
