using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject nextLevelItem;
    private Canvas canvas;
    private bool isMerging = false;
    private GameObject otherItem;
    private PrefabPool pool;
    public event Action Merged;
    private void Awake()
    {
        pool= new PrefabPool(nextLevelItem);
        canvas = GetComponentInParent<Canvas>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( isMerging)
            return;
        otherItem = collision.gameObject;
        if (nextLevelItem != null)
        {
        if (gameObject.CompareTag(otherItem.tag))
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
        return transform.position.y < otherItem.transform.position.y;
    }
    private IEnumerator CreateNewItem()
    {
        isMerging= true;
        yield return new WaitForSeconds(0.05f);
        otherItem.GetComponent<Merge>().isMerging = true;
        GameObject newItem = pool.Get();
        Merged?.Invoke();
        newItem.transform.SetParent(transform.parent, false);
        newItem.transform.position = transform.position;

        pool.Release(otherItem);
        pool.Release(gameObject);
        
            //Instantiate(nextLevelItem, transform.position, Quaternion.identity, canvas.transform);
        ParticleSystem newItemEffect=newItem.GetComponentInChildren<ParticleSystem>();
        newItemEffect.Play();
        Rigidbody2D newItemRB = newItem.GetComponent<Rigidbody2D>();
        newItemRB.simulated=true;

    }
    
}
