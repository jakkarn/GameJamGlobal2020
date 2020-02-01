using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject staticBlock;

    private bool[,] boardGrid = new bool[24, 14];

    private List<Transform> instances = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStaticBlock(Vector2 position)
    {
        boardGrid[(int)position.y, (int)position.x] = true;
        var instance = Instantiate(staticBlock, position, Quaternion.identity, transform);
        instances.Add(instance.transform);
        checkForFullLines();
    }

    private void checkForFullLines()
    {
        for (int i = 0; i < boardGrid.GetLength(0) -1; i++)
        {
            var removeLine = true;
            for (int j = 0; j < boardGrid.GetLength(1) - 1; j++)
            {
                if (boardGrid[i, j] == false)
                {
                    removeLine = false;
                }
            }

            if (removeLine)
            {
                for (int k = 0; k < boardGrid.GetLength(1) - 1; k++)
                {
                    boardGrid[i, k] = false;
                    var instance = instances.Find(x => x.position.x == k && x.position.y == i);
                    instances.Remove(instance);
                    Destroy(instance);
                }
            }
        }
    }
}
