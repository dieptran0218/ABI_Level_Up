using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get => _canMove; }
    public GameObject rayCastPoint;
    private Vector3 targetPos;
    private float speed = 15f;
    private bool _canMove;
    private Transform _thisTransform;

    private Vector3 RayCastDir;

    #region Singleton
    public static PlayerController Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    private void Start()
    {
        rayCastPoint = !rayCastPoint ? rayCastPoint = gameObject : rayCastPoint;
        _thisTransform = transform;
        targetPos = _thisTransform.position;
        InputHandler.OnSwipe += FindTarget;
        RayCastDir = _thisTransform.forward;
    }

    private void Update()
    {
        DebugRaycast();
        if (_canMove)
        {
            if ((_thisTransform.position - targetPos).sqrMagnitude > 0.0001f)
            {
                _thisTransform.position = Vector3.MoveTowards(_thisTransform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                _canMove = false;
            }
        }
    }

    private void FindTarget(Direction direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                Raycasting(_thisTransform.position, Vector3.forward);
                break;

            case Direction.Back:
                Raycasting(_thisTransform.position, Vector3.back);
                break;

            case Direction.Left:
                Raycasting(_thisTransform.position, Vector3.left);
                break;

            case Direction.Right:
                Raycasting(_thisTransform.position, Vector3.right);
                break;
        }

        _canMove = true;
    }

    private void Raycasting(Vector3 startRay, Vector3 dirUnit)
    {
        RayCastDir = dirUnit;
        //Debug.Log(startRay);

        RaycastHit hit;
        if (Physics.Raycast(startRay, dirUnit, out hit, 1f))
        {
            //Debug.Log("Hit " + hit.transform.name);

            if (hit.transform.CompareTag(Constant.STACKABLE_BLOCK_TAG) ||
                hit.transform.CompareTag(Constant.UNSTACKABLE_BLOCK_TAG) ||
                hit.transform.CompareTag(Constant.WALKABLE_BLOCK_TAG))
            {
                targetPos = hit.transform.position;
                startRay += dirUnit;

                Raycasting(startRay, dirUnit);
            }
            else
            {
                //Debug.Log("Hit Unidentify object" + hit.transform.name);
            }
        }
        else
        {
            //Debug.Log("No hit" );
        }
    }

    private void DebugRaycast()
    {
        Vector3 forward = RayCastDir;
        Debug.DrawRay(rayCastPoint.transform.position, forward, Color.green, 1f);
    }
}
