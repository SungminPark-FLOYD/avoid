using System.Transactions;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRigidbody : MonoBehaviour
{
    [Header("Move Horizontal")]
    [SerializeField]
    private float moveSpeed = 8;    //이동속도

    [Header("Move Vertical (Jump)")]
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float lowGravity = 2;   //점프키를 오래누르고 있을때 적용
    [SerializeField]
    private float highGravity = 3;  //일반적인 중력
    [SerializeField]
    private int maxJumpCount = 2;   //최대 점프 횟수
    private int currentJumpCount = 2;   //현재 남아있는 점프 횟수

    [Header("Collision")]
    [SerializeField]
    private LayerMask  groundLayer; //바닥 충돌 체크를 위한 레이어

    private bool isGrounded;        //바닥 체크
    private Vector2 footPosition;   //바닥체크를위한 발 위치
    private Vector2 footArea;       //바닥 체크를 위한 발 인식

    public Rigidbody2D rigid2D;    //속력제어를 위한 rigid
    private new Collider2D collider2D; //현재 오브젝트의 충돌범위

    public bool IsLongJump { set;  get; } = false;
    public float MoveSpeed { set => moveSpeed = Mathf.Max(0, value); }

    private void Awake()
    {
        rigid2D      = GetComponent<Rigidbody2D>();
        collider2D   = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        //플레이어 오브젝트의 Collider2D min
        Bounds bounds = collider2D.bounds;
        //플레이어 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //플레이어의 발 인식 범위 설정
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        //플레이어의 발 위치에 박스를 생성하고, 박스가 바닥과 닿아있으면 isGrounded = true;
        isGrounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);

        //플레이어의 발이 땅에 닿아있고 y축 속력이 0이하이면 점프 횟수 초기화
        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        //낮은점프 높은점프 구현을 위한 중력계수
        if(IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravity;
        }
        else
        {
            rigid2D.gravityScale = highGravity;
        }
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(transform.position.x , Contants.min.x, Contants.max.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    //x 이동방향 설정 (외부 클래스에서 호출)
    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    //점프
    public bool JumpTo()
    {
        if(currentJumpCount > 0)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
            currentJumpCount--;

            return true;
        }

        return false;
        
    }
}
