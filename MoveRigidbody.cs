using System.Transactions;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRigidbody : MonoBehaviour
{
    [Header("Move Horizontal")]
    [SerializeField]
    private float moveSpeed = 8;    //�̵��ӵ�

    [Header("Move Vertical (Jump)")]
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float lowGravity = 2;   //����Ű�� ���������� ������ ����
    [SerializeField]
    private float highGravity = 3;  //�Ϲ����� �߷�
    [SerializeField]
    private int maxJumpCount = 2;   //�ִ� ���� Ƚ��
    private int currentJumpCount = 2;   //���� �����ִ� ���� Ƚ��

    [Header("Collision")]
    [SerializeField]
    private LayerMask  groundLayer; //�ٴ� �浹 üũ�� ���� ���̾�

    private bool isGrounded;        //�ٴ� üũ
    private Vector2 footPosition;   //�ٴ�üũ������ �� ��ġ
    private Vector2 footArea;       //�ٴ� üũ�� ���� �� �ν�

    public Rigidbody2D rigid2D;    //�ӷ���� ���� rigid
    private new Collider2D collider2D; //���� ������Ʈ�� �浹����

    public bool IsLongJump { set;  get; } = false;
    public float MoveSpeed { set => moveSpeed = Mathf.Max(0, value); }

    private void Awake()
    {
        rigid2D      = GetComponent<Rigidbody2D>();
        collider2D   = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        //�÷��̾� ������Ʈ�� Collider2D min
        Bounds bounds = collider2D.bounds;
        //�÷��̾� �� ��ġ ����
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //�÷��̾��� �� �ν� ���� ����
        footArea = new Vector2((bounds.max.x - bounds.min.x) * 0.5f, 0.1f);
        //�÷��̾��� �� ��ġ�� �ڽ��� �����ϰ�, �ڽ��� �ٴڰ� ��������� isGrounded = true;
        isGrounded = Physics2D.OverlapBox(footPosition, footArea, 0, groundLayer);

        //�÷��̾��� ���� ���� ����ְ� y�� �ӷ��� 0�����̸� ���� Ƚ�� �ʱ�ȭ
        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        //�������� �������� ������ ���� �߷°��
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
    //x �̵����� ���� (�ܺ� Ŭ�������� ȣ��)
    public void MoveTo(float x)
    {
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    //����
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
