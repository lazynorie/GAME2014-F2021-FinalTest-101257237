using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpicalPlatform : MonoBehaviour
{
    public float shrinkspeed = 1;
    private Vector3 temp;
    public float period;
    public float nextActionTime;
    public LayerMask playermask;
    public Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Expand();

        Shrink();
    }

    private void Shrink()
    {
        if (Input.GetKey(KeyCode.DownArrow) && temp.x > 0)
        {
            temp = transform.localScale;
            temp.x -= 0.1f * shrinkspeed;
            temp.y -= 0.1f * shrinkspeed;
            transform.localScale = temp;
        }
    }

    private void Expand()
    {
        if (Input.GetKey(KeyCode.UpArrow) && temp.x < 1)
        {
            nextActionTime += period;
            temp = transform.localScale;
            temp.x += 0.1f * shrinkspeed;
            temp.y += 0.1f * shrinkspeed;
            transform.localScale = temp;
        }
    }

    public void CheckCollider()
    {
        while (temp.x>0.2)
        {
            collider.enabled = !collider.enabled;
        }
    }
}
