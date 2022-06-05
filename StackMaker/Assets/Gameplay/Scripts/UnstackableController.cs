using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstackableController : StackController
{
    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            gameObject.tag = Constant.WALKABLE_BLOCK_TAG;
        }
    }
}
