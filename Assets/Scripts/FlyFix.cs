using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFix : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
       rb= gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
      
        if(rb.velocity.y> 5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5f);
        }
}
}
