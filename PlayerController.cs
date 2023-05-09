using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private GameController gameController;

    private MoveRigidbody movement;
    private PlayerHP playerHP;

    SpriteRenderer spriteRenderer;
    Animator anim;
    


    private void Awake()
    {
        movement = GetComponent<MoveRigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerHP = GetComponent<PlayerHP>();
        
    }
    private void Update()
    {
        if(gameController.IsGamePlay == false) return;

        UpdateMove();
        UpdateJump();
      
    }
    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(movement.rigid2D.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        
        if(rayHit.collider != null)
        {
            if(rayHit.distance < 0.3f)
                anim.SetBool("isJump", false);
        }
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        //좌우이동
        movement.MoveTo(x);

        //방향전환
        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //애니메이션
        if (movement.rigid2D.velocity.normalized.x == 0)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }

    }

    private void UpdateJump()
    {
        if(Input.GetKeyDown(jumpKey))
        {
            movement.JumpTo();
            anim.SetBool("isJump", true);
        }
        else if (Input.GetKey(jumpKey))
        {
            movement.IsLongJump = true;
            anim.SetBool("isJump", true);
        }
        else if (Input.GetKeyUp(jumpKey))
        {
            movement.IsLongJump = false;
            anim.SetBool("isJump", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            bool isDie = playerHP.TakeDamage();
            if(isDie == true)
            {
                GetComponent<Collider2D>().enabled = false;
                gameController.GameOver();
            }
        }
        else if(collision.CompareTag("HPPotion"))
        {
            collision.gameObject.SetActive(false);
            playerHP.RecoveryHP();
        }
    }
}
