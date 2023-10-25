using UnityEngine;
using UnityEngine.Rendering;

public class SimplePlayerController : MonoBehaviour 
{
    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    [SerializeField] private LayerMask surfaceLayer;
    [SerializeField] private GameOverScript gameOverScreen;
    [SerializeField] private GameOverScript menu;

    [SerializeField] private GameObject kite;

    //[SerializeField]private TMP_Text gameOverTxt; 
    private Rigidbody2D rb;
    private Animator anim;
    private SortingGroup myRenderer;
    private int ropeLength = 5;
    private BoxCollider2D coll;
    Vector3 movement;
    private int direction = 1;
    private Vector3 moveDirection = Vector3.zero;
    bool isJumping = false;
    private bool alive = true;
    public bool isFalling = false;
    private bool isHandling = false;
    private Vector3 handlePosition = Vector3.zero;
    private static readonly int Handling = Animator.StringToHash("Handling");
    private static readonly int Hurt1 = Animator.StringToHash("hurt");

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SortingGroup>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("Character");
    }

    private void Update()
    {
        Restart();

        if (alive)
        {
            Fall();
            Hurt();
            Die();
            Attack();
            Jump();
            //if (! isHandling){
            Run();
            //}
            //Handle();
        }
    }

    private void Fall()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            isFalling = true;
            SetHandle();
        }
        //if (Input.GetAxisRaw("Vertical") == 0){
        //	gameObject.layer = LayerMask.NameToLayer("Character");
        //	isFalling = false;
        //}	
    }

    private void SetHandle(bool arg = false)
    {
        isHandling = arg;
        anim.SetBool(Handling, arg);
        kite.SetActive(arg);
    }

    private void Handle()
    {
        // if (isHandling)
        // {
        //     transform.position = new Vector3(transform.position.x, handlePosition.y, transform.position.z);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFalling && !IsGrounded())
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            return;
        }

        if (IsGrounded())
        {
            isFalling = false;
        }

        if (!isFalling && other.gameObject.CompareTag("Transport"))
        {
            
            Debug.Log("Handling");
            anim.SetBool("isJump", true);
            SetHandle(true);
            handlePosition = transform.position;
            kite.transform.position = handlePosition + Vector3.up*ropeLength;
            
        }

        gameObject.layer = LayerMask.NameToLayer("Character");
        anim.SetBool("isJump", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
     
        if (other.gameObject.CompareTag("item") && !IsGrounded())
        {
            isFalling = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
            SetHandle();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Windows") )
        {
            Debug.Log("IsHandling" + isHandling);
            SetHandle(false);
            myRenderer.sortingLayerName = "back";
            myRenderer.sortingOrder = 0;
            menu.Setup();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.layer = LayerMask.NameToLayer("Water");
            GameOver();
        }
    }


    private bool IsGrounded()
    {
        //return false;
        //Debug.Log(coll.IsTouchingLayers(surfaceLayer));
        return coll.IsTouchingLayers(surfaceLayer);
    }

    void Run()
    {
        if (anim.GetBool("isJump") == false && isHandling == false)
        {
            moveDirection = Vector3.zero;
        }

        if (isHandling)
        {
            kite.transform.position = transform.position + Vector3.up*ropeLength;
            kite.transform.localScale = new Vector3(direction, 1, 1);
        }

        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveDirection = Vector3.left;
            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveDirection = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        transform.position += moveDirection * (movePower * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonUp("Jump"))
        {
        }

        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump"))
        {
            isJumping = true;
            SetHandle();
            anim.SetBool("isJump", true);
            anim.SetBool("isJump", true);
        }

        if (isJumping == false)
        {
            return;
        }

        //rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.velocity += jumpVelocity;

        isJumping = false;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }
    }

    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger(Hurt1);
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }

    void GameOver()
    {
        Debug.Log("Die");
        anim.SetTrigger("die");
        alive = false;
        gameOverScreen.Setup();
    }
}