using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("moveDown", 0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void moveDown()
    {
            transform.position += new Vector3(0, -1);
            if (!validMove())
            {
                transform.position -= new Vector3(0, -1);
            }
    }

    bool validMove()
    {

        var freeToMoveDown = true;
        for (int i = 0; i < transform.parent.childCount -1; i++)
        {
            if (transform.parent.GetChild(i).transform.position.y == transform.position.y -1)
            {
                freeToMoveDown = false;
            }
        }

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);

        if (roundedY < 0 || !freeToMoveDown)
        {
            return false;
        }
        return true;
    }
}
