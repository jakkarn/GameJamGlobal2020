using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockContainer : MonoBehaviour
{
    [SerializeField]
    private Sprite frozenSprite;

    [SerializeField]
    private GameObject staticBlock;

    [SerializeField]
    private GameObject destroyLine;

    public static int boardHeight = 24;
    public static int boardWidth = 14;

    public static Transform[,] staticGrid = new Transform[boardWidth, boardHeight];

    private List<Transform> instances = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addBlock(Transform transform)
    {

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);

        staticGrid[roundedX, roundedY] = transform;
        SpriteRenderer childRenderer = transform.GetComponent<SpriteRenderer>();
        childRenderer.sprite = frozenSprite;
        checkLines();
    }

    public bool isPositionFree(int x, int y)
    {
        if (x > boardWidth -1 || x < 0 || y < 0)
        {
            return false;
        }
        if (y > boardHeight)
        {
            return true;
        }

        if (staticGrid[x, y] != null)
        {
            return false;
        }

          return true;
    }

    private void checkLines()
    {
        for (int i = 0; i < boardHeight - 1; i++)
        {
            if (hasLine(i))
            {
                deleteLine(i);
                rowDown(i);

            }
        }
    }

    private bool hasLine(int index)
    {
        for (int j = 0; j < boardWidth; j++)
        {
            if (staticGrid[j, index] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void deleteLine(int index)
    {
        for (int j = 0; j < boardWidth; j++)
        {
            if (j == Mathf.Round(boardWidth / 2))
            {
                Instantiate(destroyLine, staticGrid[j, index].transform.position, Quaternion.identity);
            }
            Destroy(staticGrid[j, index].gameObject);
            staticGrid[index, j] = null;
        }
    }

    private void rowDown(int index)
    {
        for (int y = index; y < boardHeight; y++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if(staticGrid[j, y] != null)
                {
                    staticGrid[j, y - 1] = staticGrid[j, y];
                    staticGrid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    staticGrid[j, y] = null;
                }

            }

        }
    }
}

//    private void checkForFullLines()
//    {
//        for (int i = 0; i < boardGrid.GetLength(0) -1; i++)
//        {
//            var removeLine = true;
//            for (int j = 0; j < boardGrid.GetLength(1) - 1; j++)
//            {
//                if (boardGrid[i, j] == false)
//                {
//                    removeLine = false;
//                }
//            }

//            if (removeLine)
//            {
//                for (int k = 0; k < boardGrid.GetLength(1) - 1; k++)
//                {
//                    boardGrid[i, k] = false;
//                    var instance = instances.Find(x => x.position.x == k && x.position.y == i);
//                    instances.Remove(instance);
//                    Destroy(instance.gameObject);
//                }
//            }
//        }
//    }
//}
