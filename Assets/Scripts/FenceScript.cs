using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour
{
    
    private BoxCollider2D boxcol;
    // Start is called before the first frame update
    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") < 0){
			boxcol.enabled = false;
		}
		else {
			boxcol.enabled = true;
		}
    }
}
