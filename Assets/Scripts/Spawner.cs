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
    private void Awake()
    {
        pool = new PrefabPool(vegPrefabs,10);
        drag = GetComponent<Drag>();
        Subscribe(drag);
        Spawn();
    }

    private void Spawn()
    {
        StartCoroutine(SpawnTimer());
    }
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.4f);
       // int randInd=UnityEngine.Random.Range(0, vegPrefabs.Length);
        itemToSpawn = pool.Get();
        itemToSpawn.transform.SetParent(transform,false);
        var itemDrag=itemToSpawn.GetComponent<VegetableDrag>();
        itemDrag.Subscribe(drag);
        //var itemDrag =itemToSpawn.GetComponent<VegetableDrag>();
            //Instantiate(vegPrefabs[randInd],transform.position, Quaternion.identity , transform);
        itemWidth= itemToSpawn.GetComponent<RectTransform>().rect.width;
        spawnerRect = gameObject.GetComponent<RectTransform>();
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
