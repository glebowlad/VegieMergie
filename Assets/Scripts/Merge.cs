using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject nextLevelItem;
    private Canvas canvas;
    private bool isMerging = false;
    private GameObject otherItem;
    

    private void Awake()
    {
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
        Destroy(otherItem.gameObject);
        Destroy(gameObject);
        
        GameObject newItem = Instantiate(nextLevelItem, transform.position, Quaternion.identity, canvas.transform);
        Rigidbody2D newItemRB = newItem.GetComponent<Rigidbody2D>();
        newItemRB.simulated=true;

    }
    
}
