using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject board;

    private BuildArea buildArea;

    private static int StartBlockAmount = 200;

    private int BlockAmount = StartBlockAmount;

    private void Start()
    {
        buildArea = board.GetComponent<BuildArea>();
        buildArea.starting(StartBlockAmount);
    }

    public int GetAmount() 
    {
        return BlockAmount;
    }

    public void ReturnBlocksInFormationToCounter(int blockCount)
    {
        this.BlockAmount += blockCount;
        buildArea.spawnVisualBlocks(blockCount);
    }

    public void RemoveCreatedFormationBlocks(int formationBlockSize)
    {
        this.BlockAmount = Mathf.Max(this.BlockAmount - formationBlockSize, 0);
        buildArea.removeVisualBlocks(formationBlockSize);
    }
}
