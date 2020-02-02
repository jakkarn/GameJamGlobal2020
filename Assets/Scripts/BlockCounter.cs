using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCounter : MonoBehaviour
{
    private static int StartBlockAmount = 50;

    private int BlockAmount = StartBlockAmount;

    public int GetAmount() 
    {
        return BlockAmount;
    }

    public void ReturnBlocksInFormationToCounter(int blockCount)
    {
        this.BlockAmount += blockCount;
    }

    public void RemoveCreatedFormationBlocks(int formationBlockSize)
    {
        this.BlockAmount = Mathf.Max(this.BlockAmount - formationBlockSize, 0);
    }
}
