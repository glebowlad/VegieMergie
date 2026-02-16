using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject nextLevelItem;
    private Canvas canvas;
    private bool isMerging = false;
    private GameObject collidedItem;
    private PrefabPool pool;
    public static event Action<GameObject> Merged;

    //private static int mergedItemsCounter=0;
    //public int scoreCounter = 0;
    //private static int totalScore=0;
    private void Awake()
    {
        pool= new PrefabPool(nextLevelItem);
        canvas = GetComponentInParent<Canvas>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( isMerging)
            return;
        collidedItem = collision.gameObject;
        if (nextLevelItem != null)
        {
        if (gameObject.CompareTag(collidedItem.tag))
        {
            if (InitiateMerge())
            {
                    StartCoroutine(CreateNewItem());
                }
        }
        }
    }
    
    private bool InitiateMerge()
    {
        return transform.position.y < collidedItem.transform.position.y;
    }
    private IEnumerator CreateNewItem()
    {
        isMerging= true;
        yield return new WaitForSeconds(0.15f);
        collidedItem.GetComponent<Merge>().isMerging = true;
        GameObject newItem = pool.Get();
        Merged?.Invoke(newItem);
        newItem.transform.SetParent(transform.parent, false);
        newItem.transform.position = (transform.position+collidedItem.transform.position)/2f;

        pool.Release(collidedItem);
        pool.Release(gameObject);
        
        ParticleSystem newItemEffect=newItem.GetComponentInChildren<ParticleSystem>();
        newItemEffect.Play();
        Rigidbody2D newItemRB = newItem.GetComponent<Rigidbody2D>();
        newItemRB.simulated=true;

    }
    
}
