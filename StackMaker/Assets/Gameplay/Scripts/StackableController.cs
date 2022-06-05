using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableController : StackController
{
    public GameObject blockMesh;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            Destroy(blockMesh);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            gameObject.tag = Constant.WALKABLE_BLOCK_TAG;
        }
    }
}
