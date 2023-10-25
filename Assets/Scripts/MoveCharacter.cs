using UnityEngine;
using UnityEngine.Serialization;

public class MoveCharacter : MonoBehaviour
{
    public float walkSpeed = 10;
    [FormerlySerializedAs("walkSpeed_horizontal")] public float walkSpeedHorizontal = 10;
    private Animator _anim;
    private Rigidbody2D rb;

    private static readonly int WalkHash = Animator.StringToHash("walk");

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
                
        }
    }
            
}