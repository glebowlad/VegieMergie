using System;
using System.Collections;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject nextLevelItem;
    private bool isMerging = false;
    private GameObject collidedItem;
    private PrefabPool pool;
    
    // Событие теперь передает уровень (int)
    public static event Action<int> Merged;

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

    private IEnumerator CreateNewItem()
    {
        yield return new WaitForSeconds(0.15f);
        GameObject newItem = pool.Get();

        int level = (int)char.GetNumericValue(newItem.name[0]);
        Merged?.Invoke(level);

        newItem.transform.SetParent(transform.parent, false);
        newItem.transform.position = (transform.position + collidedItem.transform.position) / 2f;

        pool.Release(collidedItem);
        pool.Release(gameObject);
        
        newItem.GetComponentInChildren<ParticleSystem>().Play();
        newItem.GetComponent<Rigidbody2D>().simulated = true;
        isMerging = false;
    }
}
