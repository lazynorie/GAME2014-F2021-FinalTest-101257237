/// SpicalPlatform.cs
/// Jing Yuan Cheng 101257237
/// Special platform controller (sorry for the the missing "e" in the script name)
/// controls the shrink and expand of the platform 
/// also controls the floating behaviours of the platform
/// Last editedL: Dec 17 2021
/// Version 1.0
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpicalPlatform : MonoBehaviour
{
    [Header("Shrinking Platform")]
    public float shrinkspeed;
    public float expandspeed;
    private Vector3 temp;
    private bool isShrinkable;
    
    [Header("Audio")]
    public List<AudioClip> soundclips;
    public AudioSource audioSource;
    private bool expandcliplaying;
    private bool shrinclipplaying;

    [Header("Floating Platform")]
    public float floatSpeed;
    public float floatDistance;
    public float direction = 1;
    private float distance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isShrinkable = false;
        
        expandcliplaying = false;
        shrinclipplaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        floatingPlatform();
        ShrinkingPlatform();
        ExpandingPlatform();
    }

    private void floatingPlatform()
    {
        float yOffset = transform.position.y + direction * floatSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        distance += floatSpeed * Time.deltaTime;
        if (distance>=floatDistance)
        {
            distance = 0;
            direction = -direction;
        }
    }
    IEnumerator Shrink()
    {
        if (shrinclipplaying)
            {
                PlayClip(SoundClip.SHRINKING);
            }
           while (temp.x > 0)
           {
            temp = transform.localScale;
            temp.x -= 0.001f * shrinkspeed * Time.deltaTime;
            temp.y -= 0.001f * shrinkspeed * Time.deltaTime;
            transform.localScale = temp;
            yield return null;
           }
    }
    IEnumerator Expand()
    {
        yield return new WaitForSecondsRealtime(0.2f);
            if (expandcliplaying)
            {
                PlayClip(SoundClip.EXPANDING);
            }
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinkable = true;
            shrinclipplaying = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isShrinkable = false;
            expandcliplaying = true;
        }
      
    }
    private void PlayClip(SoundClip clip)
    {
        switch (clip)
        {
            case SoundClip.EXPANDING:
                expandcliplaying = true;
                shrinclipplaying = false;
                audioSource.clip = soundclips[0];
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    expandcliplaying = false;
                    shrinclipplaying = false;
                }
                break;
            case SoundClip.SHRINKING:
                expandcliplaying = false;
                shrinclipplaying = true;
                audioSource.clip = soundclips[1];
                if (!audioSource.isPlaying)
                { 
                    audioSource.Play();
                    expandcliplaying = false;
                    shrinclipplaying = false;
                }
                break;
        }
    }
}

public enum SoundClip
{
    EXPANDING,
    SHRINKING,
}