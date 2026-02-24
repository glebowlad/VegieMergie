using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject nextLevelItem;
    private bool isMerging = false;
    private GameObject collidedItem;
    private PrefabPool pool;
    public static event Action<GameObject> Merged;
    private void Awake()
    {
        pool = new PrefabPool(nextLevelItem);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (nextLevelItem == null || isMerging) return;

   
    if (collision.gameObject.CompareTag(gameObject.tag))
    {
        Merge otherMerge = collision.gameObject.GetComponent<Merge>();
        if (otherMerge == null || otherMerge.isMerging) return;

        if (gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
        {
            isMerging = true;
            otherMerge.isMerging = true;
            
            collidedItem = collision.gameObject;
            StartCoroutine(CreateNewItem());
        }
    }
}
    
    private bool InitiateMerge()
    {
        return transform.position.y < collidedItem.transform.position.y;
    }
    private IEnumerator CreateNewItem()
    {
       
        yield return new WaitForSeconds(0.15f);
        //collidedItem.GetComponent<Merge>().isMerging = true;
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
        isMerging=false;

    }
    
}
