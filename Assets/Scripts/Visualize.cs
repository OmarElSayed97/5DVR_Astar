using AStar5DVR;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class Visualize : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Transform Agent;
    [SerializeField]
    Transform GridParent;
    [SerializeField]
    GameObject GridTilePrefab;
    [SerializeField]
    Material StartMaterial, EndMaterial;


    private Algorithm _AlgorithmInstance;
    private Transform[,] WorldGrid;
    #endregion



    #region Methods
    void Start()
    {
        _AlgorithmInstance = Algorithm._instance;
       
        AStarGrid grid = new AStarGrid(4);
        WorldGrid = new Transform[grid.GridCols,grid.GridRows];
        Debug.Log(grid.GridCols);
        Stack<Node> path = _AlgorithmInstance.SolveGrid(new Vector2(0, 0), new Vector2(2, 2), grid);
        BuildWorld(new Vector2(0, 0), new Vector2(2, 2), grid);
        Agent.position = WorldGrid[0, 0].position;
        StartCoroutine(MoveAgent(path));
       
    }

    void BuildWorld(Vector2 startingPoint, Vector2 endingPoint, AStarGrid _grid)
    {
        GameObject tile;
        for (int i = 0; i < _grid.GridCols; i++)
        {
            
            for (int j = 0; j < _grid.GridRows; j++)
            {
                
                tile = Instantiate(GridTilePrefab, GridParent);
                Vector3 tilePos = new Vector3(_grid.Grid[i][j].Position.x , 0, _grid.Grid[i][j].Position.y );
                tile.transform.localPosition = tilePos;
                if (_grid.Grid[i][j].Position.x == startingPoint.x && _grid.Grid[i][j].Position.y == startingPoint.y)
                {
                    tile.GetComponent<MeshRenderer>().material = StartMaterial;
                }
                else if (_grid.Grid[i][j].Position.x == endingPoint.x && _grid.Grid[i][j].Position.y == endingPoint.y)
                {
                    tile.GetComponent<MeshRenderer>().material = EndMaterial;
                }
                else if (!_grid.Grid[i][j].Walkable)
                {
                    tile.transform.GetChild(0).localScale = new Vector3(0.8f, 20, 0.8f);
                }
                WorldGrid[i, j] = tile.transform;
            }
           

        }
    }

    IEnumerator MoveAgent(Stack<Node> path)
    {
        while (path.Count != 0)
        {
            Debug.Log(path.Pop().Position);
            Vector2 destination = path.Pop().Position;
            float t = 0;
            Vector3 endPos = WorldGrid[(int)destination.x,(int)destination.y].position;
            while (t <= 1.0)
            {
                t += Time.deltaTime / 0.5f;
                Agent.localPosition = Vector3.Lerp(Agent.position, endPos, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
            Agent.localPosition = endPos;
            yield return new WaitForSeconds(0.2f);

        }
           
    }


   
    #endregion


}
