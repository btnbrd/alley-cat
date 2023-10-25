using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterwalk : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;
    private bool invoked = false;
    private Vector3 moveDirection = Vector3.left;

    private Vector3 startposition;
	private Vector3 startScale;
    [SerializeField] private int moveVelocity = 20;
    // [SerializeField] private int moveVelocity = 10;
    void Start()
    {
        startposition = transform.position;
		startScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("MoveToBorder", 4.0f);
        
    }

    void MoveToBorder()
    {
        invoked = true;
        float randomTime = Random.Range(8, 13);
        if (Random.Range(0, 2)==1)
        {
            // Debug.Log(-1);
            moveDirection.x = -1;
        }
        else
        {
            // Debug.Log(1);
            moveDirection.x = +1;
        }
        transform.localScale = new Vector3(-startScale.x*moveDirection.x, startScale.y, startScale.z);

        transform.position = new Vector3(-startposition.x * moveDirection.x, startposition.y, startposition.z);
        // Debug.Log(transform.position);
        // Debug.Log(moveDirection.x);
        Invoke("MoveToBorder", randomTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (invoked &&(transform.position.x < 34 || transform.position.x > -34))
        {
            transform.position += moveVelocity * moveDirection * Time.deltaTime;
        }
    }
}
