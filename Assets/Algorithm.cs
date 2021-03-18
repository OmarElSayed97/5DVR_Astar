using AStar5DVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    #region Variables
    public static Algorithm _instance;
    private AStarGrid aStarGrid;
    #endregion
    #region MainMethods
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        aStarGrid = new AStarGrid(4);
        PrintPath(SolveGrid(new Vector2(0, 0), new Vector2(3, 3), aStarGrid));
    }
    #endregion
    #region Methods
    public Stack<Node> SolveGrid(Vector2 startPoint, Vector2 endPoint, AStarGrid grid)
    {
        Node start = new Node(new Vector2(startPoint.x, startPoint.x), true);
        Node end = new Node(new Vector2(endPoint.x, endPoint.x), true);

        Stack<Node> Path = new Stack<Node>();
        List<Node> UnvisitedNodes = new List<Node>();
        List<Node> ExploredNodes = new List<Node>();
        List<Node> adjacencies;
        Node current = start;
        UnvisitedNodes.Add(start);

        while (UnvisitedNodes.Count != 0 && !ExploredNodes.Exists(node => node.Position == end.Position))
        {
            current = UnvisitedNodes[0];
            UnvisitedNodes.Remove(current);
            ExploredNodes.Add(current);
            adjacencies = grid.GetAdjacentNodes(current);


            foreach (Node n in adjacencies)
            {
                if (!ExploredNodes.Contains(n) && n.Walkable)
                {
                    if (!UnvisitedNodes.Contains(n))
                    {
                        n.Parent = current;
                        n.H = Vector2.Distance(n.Position, endPoint);
                        n.G = Vector2.Distance(n.Position, startPoint);
                        UnvisitedNodes.Add(n);
                        UnvisitedNodes = UnvisitedNodes.OrderBy(node => node.F).ToList<Node>();
                    }
                }
            }
        }
        if (!ExploredNodes.Exists(x => x.Position == end.Position))
        {
            Debug.Log("NO PATH FOUND!");
            return null;
        }
        Node temp = ExploredNodes[ExploredNodes.IndexOf(current)];
        if (temp == null) return null;
        do
        {
            Path.Push(temp);
            temp = temp.Parent;
        } while (temp != start && temp != null);
        return Path;

    }


    void PrintPath(Stack<Node> path)
    {
        while (path.Count != 0)

            Debug.Log(path.Pop().Position);
    }
    #endregion




}