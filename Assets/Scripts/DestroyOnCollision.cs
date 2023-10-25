using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    //private CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<CircleCollider2D>();
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
		//Debug.Log(other.gameObject.layer);
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Character") ){
       		 Destroy(gameObject);
		}
    }
}