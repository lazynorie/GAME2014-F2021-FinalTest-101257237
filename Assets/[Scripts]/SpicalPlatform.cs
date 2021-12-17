using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatformState
{
    EXPANDING,
    SHRINKING,
}
public class SpicalPlatform : MonoBehaviour
{
    public float shrinkspeed;
    public float expandspeed;
    private Vector3 temp;
    private Collider2D collider;
    private bool isShrinkable;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        isShrinkable = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShrinkingPlatform();
        ExpandingPlatform();
    }

    IEnumerator Shrink()
    {
        if (isShrinkable)
        {
            while (temp.x > 0)
            {
                temp = transform.localScale;
                temp.x -= 0.001f * shrinkspeed * Time.deltaTime;
                temp.y -= 0.001f * shrinkspeed * Time.deltaTime;
                transform.localScale = temp;
                yield return null;
            }
        }
    }

    IEnumerator Expand()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        while (temp.x < 1)
        {
            temp = transform.localScale;
            temp.x += 0.002f * expandspeed * Time.deltaTime;
            temp.y += 0.002f * expandspeed * Time.deltaTime;
            transform.localScale = temp;
            yield return null;
        }
    }

    private void ShrinkingPlatform()
    {
        if (isShrinkable)
        {
            StartCoroutine(Shrink());
        }
        
    }

    private void ExpandingPlatform()
    {
        if (!isShrinkable)
        {
            StartCoroutine(Expand());
        }
        
    }
    
    /*private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinkable = true;
            Debug.Log("Stay");
            ShrinkingPlatform();
        }
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinkable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinkable = false;
        }
      
    }
}
