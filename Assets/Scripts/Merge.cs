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
    private void Awake()
    {
        pool= new PrefabPool(nextLevelItem);
        canvas = GetComponentInParent<Canvas>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (nextLevelItem == null || isMerging) return;

    // Проверяем тег
    if (collision.gameObject.CompareTag(gameObject.tag))
    {
        Merge otherMerge = collision.gameObject.GetComponent<Merge>();

        // Если сосед уже в процессе слияния — игнорируем
        if (otherMerge == null || otherMerge.isMerging) return;

        // ПРАВИЛО ПРИОРИТЕТА:
        // 1. Сливаемся, только если мой ID меньше, чем у соседа (выбираем одного "лидера")
        // 2. Это исключит ситуацию, когда один объект пытается слиться сразу с двумя
        if (gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
        {
            isMerging = true;
            otherMerge.isMerging = true; // Сразу блокируем соседа
            
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
        isMerging=false;

    }
    
}
