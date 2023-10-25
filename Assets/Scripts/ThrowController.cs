using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<GameObject> windows;
    [SerializeField] private GameObject item;

    [SerializeField] private int throwForce;
    [SerializeField] private GameObject player;
    private GameObject openedWindow;
    private Animator window_anim;
    // private Rigidbody2D rb;

    void Awake()
    {
        Invoke("OpenWindow", 4.0f);
        // rb = item.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


    void OpenWindow()
    {
        int ind = Random.Range(0, windows.Count);
        openedWindow = windows[ind];
        openedWindow.GetComponent<BoxCollider2D>().enabled = true;
        window_anim = openedWindow.GetComponent<Animator>();
        window_anim.SetBool("openWindow", true);

        Invoke("ThrowItem", 1.0f);
    }

    void ThrowItem()
    {
        // Debug.Log("Me too");
        var instance = Instantiate(item, openedWindow.transform.position, Quaternion.identity);
        instance.tag = "item";

        //
        var direction = player.transform.position - openedWindow.transform.position;
        direction.Normalize();
        instance.GetComponent<Rigidbody2D>().AddForce(throwForce * direction, ForceMode2D.Impulse);

        Invoke("CloseWindow", 4.0f);
    }

    void CloseWindow()
    {
        openedWindow.GetComponent<BoxCollider2D>().enabled = false;
        window_anim.SetBool("openWindow", false);
        Invoke("OpenWindow", 4.0f);
    }
}