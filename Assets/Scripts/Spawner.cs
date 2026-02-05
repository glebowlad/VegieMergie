using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] vegPrefabs;
    private GameObject itemToSpawn;
    private float itemWidth;
    private RectTransform spawnerRect;
    private Drag drag;
    private PrefabPool pool;
    [SerializeField]
    private GameObject gameOverLine;

    private void Awake()
    {
        spawnerRect = gameObject.GetComponent<RectTransform>();
        pool = new PrefabPool(vegPrefabs,10);
        drag = GetComponent<Drag>();
        Subscribe(drag);
        Spawn();
    }

    private void Spawn()
    {
        if (itemToSpawn != null )
        {
            StartCoroutine(CheckGameOver());
            //return;
        }
            StartCoroutine(SpawnTimer());

        
    }

    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(0.4f);
        if(itemToSpawn.transform.position.y > gameOverLine.transform.position.y)
        {
            Debug.Log("GAME OVER");
        }
    }
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.4f);
        itemToSpawn = pool.Get();
        itemToSpawn.transform.SetParent(transform,false);
        var itemDrag=itemToSpawn.GetComponent<VegetableDrag>();
        itemDrag.Subscribe(drag);
        itemWidth= itemToSpawn.GetComponent<RectTransform>().rect.width;
        spawnerRect.sizeDelta= new Vector2(itemWidth,spawnerRect.sizeDelta.y);
    }
    private void Subscribe(Drag _drag)
    {
        drag.OnDragFinished += Spawn;
    }
    private void OnDestroy()
    {
        drag.OnDragFinished -= Spawn;
    }
}
