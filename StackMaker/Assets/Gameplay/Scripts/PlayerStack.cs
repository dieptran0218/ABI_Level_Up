using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    public Animator anim;
    private string ACTION_ANIM = "Action";

    public GameObject standedBrickPrefabs;

    public Transform stackContainer;
    public Transform PlayerRenderer;

    public bool IsWin { get; set; }

    private Stack<GameObject> _brickStack;

    private int animState = 0;

    private float brickHeight = 0.3f;

    public TreasureManager treasureManager;

    #region Singleton
    public static PlayerStack Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    void Start()
    {
        _brickStack = new Stack<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.STACKABLE_BLOCK_TAG))
        {
            PushToStack();
            SetAnimState(1);
            Invoke(nameof(SetIdleAnim), 0.1f);
        }

        if (other.CompareTag(Constant.UNSTACKABLE_BLOCK_TAG))
        {
            PopFromStack(other.transform.position + Vector3.down * brickHeight);
        }

        if (other.CompareTag(Constant.WIN_POSITION_TAG))
        {
            SetAnimState(2);
            IsWin = true;
            treasureManager.WaitToOpenTreasure(1f);
            treasureManager.StartFireworks();
        }
    }

    public void SetAnimState(int state)
    {
        animState = state;
        anim.SetInteger(ACTION_ANIM, animState);
    }

    public void SetIdleAnim()
    {
        SetAnimState(0);
    }


    private void PushToStack()
    {
        GameObject brick = Instantiate(standedBrickPrefabs, transform.position, Quaternion.identity, stackContainer);
        _brickStack.Push(brick);

        brick.transform.localPosition = new Vector3(0, 0, (_brickStack.Count - 1) * brickHeight);
        PlayerRenderer.localPosition = new Vector3(0, _brickStack.Count * brickHeight,0);
    }

    private void PopFromStack(Vector3 popedPosition)
    {
        if(_brickStack.Count > 0)
        {
            GameObject brick = _brickStack.Pop();
            brick.transform.parent = null;
            brick.transform.position = popedPosition + Vector3.up * 0.1f;

            PlayerRenderer.localPosition = new Vector3(0, _brickStack.Count * brickHeight , 0);
        }

    }
}
