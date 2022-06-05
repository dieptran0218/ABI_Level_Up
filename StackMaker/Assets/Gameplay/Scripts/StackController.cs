using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class StackController : MonoBehaviour
{
    public abstract void OnTriggerExit(Collider other);
}
