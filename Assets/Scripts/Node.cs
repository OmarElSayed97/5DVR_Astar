using UnityEngine;


namespace AStar5DVR
{
    public class Node 
    {
        #region Variables
        public Node Parent;
        public Vector2 Position;
        public bool Walkable;
        public float H;
        public float G;
        public float F
        {
            get
            {
                if (H != -1 && G != -1)
                    return H + G;
                else
                    return -1;
            }
        }
        #endregion

        #region Constructor
        public Node(Vector2 pos, bool walkable)
        {
            Parent = null;
            Position = pos;
            H = -1;
            G = 1;

            Walkable = walkable;
        }
        #endregion

    }
}

