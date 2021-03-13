using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AStar5DVR
{
    public class AStarGrid
    {
        #region Variables
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
                return Grid[0].Count;
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        #endregion

        #region Constructor
        public AStarGrid(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public AStarGrid(int gridSize)
        {
            List<List<Node>> grid = new List<List<Node>>();
            for (int i = 0; i < gridSize; i++)
            {
                List<Node> row = new List<Node>();
                grid.Add(row);
                for (int j = 0; j < gridSize; j++)
                {
                    bool walkable = GenerateObstacle();
                    Vector2 position = new Vector2(i, j);
                    Node node = new Node(position,walkable);
                    grid[i].Insert(j,node);
                }
            }
        }
        #endregion

        #region Methods
        static bool GenerateObstacle()
        {
            System.Random random = new System.Random();
            if (random.NextDouble() < 0.8)
                return false;
            else
            {
                return true;
            }
            
        }

        public List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> adjNodes = new List<Node>();

            int row = (int)n.Position.y;
            int col = (int)n.Position.x;

            if (row + 1 < GridRows)
            {
                adjNodes.Add(Grid[col][row + 1]);
            }
            if (row - 1 >= 0)
            {
                adjNodes.Add(Grid[col][row - 1]);
            }
            if (col - 1 >= 0)
            {
                adjNodes.Add(Grid[col - 1][row]);
            }
            if (col + 1 < GridCols)
            {
                adjNodes.Add(Grid[col + 1][row]);
            }

            return adjNodes;
        }
        #endregion

    }
}

