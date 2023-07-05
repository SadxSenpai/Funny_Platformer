using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAnimation : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (anim)
        {
            anim.SetBool("IsCollected", true);
        }
    }

    // Update is called once per frame
    private void DestroyFruit()
    {
        Destroy(gameObject);
    }
}
