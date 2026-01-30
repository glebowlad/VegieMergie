using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip mergeSound;
    [SerializeField]
    private AudioClip dropSound;
    [SerializeField]
    private Drag drag;
    //[SerializeField]
    //private Merge merge;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        Subscribe();
        
    }

    private void Subscribe()
    {
        drag.OnDragFinished += PlayDropSound;
        //merge.Merged += PlayMergeSound;
    }

    private void PlayMergeSound()
    {
        source.PlayOneShot(mergeSound);
    }

    private void PlayDropSound()
    {
        source.clip = dropSound;
        source.Play();
    }
    private void OnDestroy()
    {
        drag.OnDragFinished -= PlayDropSound;
    }
}
