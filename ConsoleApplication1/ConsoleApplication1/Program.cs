using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static List<node> Nodes = new List<node>();
        static Dictionary<string, int> NodesIndex = new Dictionary<string, int>();
        static List<edge> Edges = new List<edge>();
        static int SizeEdges = 0;
        static int SizeNodes = 0;
        static string StartNode;
        class node
        {
            public string name;
            public string pre;
            public int Dist;
            public bool vis;
            public node(string NodeName)
            {
                name = NodeName;
                Dist = -1;
                vis = false;
            }
        }
        static public void AddNode(string NodeName)
        {
            node temp = new node(NodeName);
            Nodes.Add(temp);
            NodesIndex.Add(NodeName, Nodes.Count() - 1);
        }
        class edge
        {
            public string N1;
            public string N2;
            public int Dist;
            public edge(string Node1, string Node2, int EdgeVal)
            {
                N1 = Node1;
                N2 = Node2;
                Dist = EdgeVal;
            }
        }
        static public void AddEdge(string Node1, string Node2, int EdgeVal)
        {
            edge temp = new edge(Node1, Node2, EdgeVal);
            Edges.Add(temp);
        }
        static void StartingNode(string name)
        {
            Nodes[NodesIndex[name]].Dist = 0;
            SizeNodes = Nodes.Count();
            SizeEdges = Edges.Count();
            StartNode = name;
        }

        static void AdjNode(string N, List<string> AdjNodes)
        {
            for (int i = 0; i < SizeEdges; ++i)
            {
                if (Edges[i].N1 == N && !(Nodes[(NodesIndex[Edges[i].N2])].vis))
                {
                    AdjNodes.Add(Edges[i].N2);
                }
                else if (Edges[i].N2 == N && !(Nodes[NodesIndex[Edges[i].N1]].vis))
                {
                    AdjNodes.Add(Edges[i].N1);
                }
            }
        }
        static bool EdgeNode(edge E, string N1, string N2)
        {
            return (E.N1 == N1 && E.N2 == N2 || E.N1 == N2 && E.N2 == N1);
        }
        static int GetDistance(string N1, string N2)
        {
            for (int i = 0; i < SizeEdges; ++i)
            {
                if (EdgeNode(Edges[i], N1, N2))
                {
                    return Edges[i].Dist;
                }
            }
            return -1;
        }
        static int NearNode()
        {
            int index = 0;
            int Dist = 0;
            for (int i = 0; i < SizeNodes; ++i)
            {
                if (!Nodes[i].vis && Nodes[i].Dist >= 0)
                {
                    Dist = Nodes[i].Dist;
                    index = i;

                }
            }
            for (int i = 0; i < SizeNodes; ++i)
            {
                if (Nodes[i].Dist < Dist && !Nodes[i].vis && Nodes[i].Dist >= 0)
                {
                    Dist = Nodes[i].Dist;
                    index = i;
                }
            }
            Nodes[index].vis = true;
            return index;
        }
        static void Dijkstras()
        {

            while (true)
            { 
                int visited = 0;
                for (int i = 0; i < SizeNodes; ++i)
                {
                    if (Nodes[i].vis)
                    {
                        ++visited;
                    }
                }
                if (visited >= SizeNodes)
                {
                    break;
                }

                node nazdiktarinNode = Nodes[NearNode()];
                List<string> nodhayemostaqim = new List<string>();
                AdjNode(nazdiktarinNode.name, nodhayemostaqim);
                int tedadeNodeHamsaye = nodhayemostaqim.Count();
                for (int i = 0; i < tedadeNodeHamsaye; ++i)
                {
                    node node = Nodes[NodesIndex[nodhayemostaqim[i]]];
                    int Distance = nazdiktarinNode.Dist + GetDistance(nazdiktarinNode.name, nodhayemostaqim[i]);
                    if (node.Dist >= 0)
                    {
                        if (Distance < node.Dist)
                        {
                            node.Dist = Distance;
                            node.pre = nazdiktarinNode.name;
                        }
                    }
                    else
                    {
                        node.Dist = Distance;
                        node.pre = nazdiktarinNode.name;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            AddNode("u");
            AddNode("x");
            AddNode("w");
            AddNode("v");
            AddNode("y");
            AddNode("z");
            AddEdge("u", "x", 5);
            AddEdge("u", "w", 3);
            AddEdge("u", "v", 7);
            AddEdge("x", "w", 4);
            AddEdge("x", "y", 7);
            AddEdge("x", "z", 9);
            AddEdge("w", "v", 3);
            AddEdge("w", "y", 8);
            AddEdge("v", "y", 4);
            AddEdge("y", "z", 2);
            string startnode = "u";
            StartingNode(startnode);

            Dijkstras();
            Console.WriteLine(startnode + " is start nod and Cost= 0;");
            Console.WriteLine("nodes" + " \t\t|"+"cost");
            string[] masirha = { "u be x", "u be w", "u be v", "x be w", "x be y", "x be z", "w be v", "w be y", "v be y", "y be z" };

            for (int i = 0; i < Nodes.Count(); i++)
            {
                if (Nodes[i].name == startnode)
                {
                    continue;
                }
                Console.WriteLine(Nodes[i].pre + " to " + Nodes[i].name + " \t\t| " + Nodes[i].Dist);

            }
            while (true)
            {
                ResetNodes();
        
                Console.Write("gere shoro ra vared konid:  ");
                startnode = Console.ReadLine();
                StartingNode(startnode);
                for (int i = 0; i <= 9; i++)
                {
                    Console.Write("Vazne " + masirha[i] + ": ");
                    ChangeDist(i, int.Parse(Console.ReadLine()));
                }
                Dijkstras();
                Console.Write("-----------\n\n");
                Console.WriteLine(startnode + " is start nod and Cost= 0;");
                           Console.WriteLine("nodes" + " \t\t|"+"cost");

                for (int i = 0; i < Nodes.Count(); i++)
                {
                    if (Nodes[i].name == startnode)
                    {
                        continue;
                    }
                    Console.WriteLine(Nodes[i].pre + " to " + Nodes[i].name + " \t\t| " + Nodes[i].Dist);
                }
            }
        }

        private static void ChangeDist(int index, int newDist)
        {
            Edges[index].Dist = newDist;
        }

        private static void ResetNodes()
        {
            for (int i = 0; i < Nodes.Count(); i++)
            {
                Nodes[i].Dist = -1;
                Nodes[i].vis = false;
                Nodes[i].pre = "";
            }
        }
    }
}
