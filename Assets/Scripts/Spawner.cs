using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] vegPrefabs;
    [SerializeField]
    private GameObject gameOverLine;
    private GameObject itemToSpawn;
    private RectTransform spawnerRect;
    private PrefabPool pool;
    private float itemWidth;
    private Drag drag;
    public static bool IsSpawned { get; private set; }

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
        IsSpawned = false;
        StopAllCoroutines();
        StartCoroutine(SpawnTimer());

    }
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.4f);
        itemToSpawn = pool.Get();
        IsSpawned = true;
        itemToSpawn.transform.SetParent(transform,false);
        var itemDrag=itemToSpawn.GetComponent<Vegetable>();
        itemDrag.Initialize(drag, gameOverLine);
        itemWidth= itemToSpawn.GetComponent<RectTransform>().rect.width;
        spawnerRect.sizeDelta= new Vector2(itemWidth,spawnerRect.sizeDelta.y);
    }
    private void Subscribe(Drag _drag)
    {
        _drag = drag;
        drag.OnDragFinished += Spawn;
    }
    private void OnDestroy()
    {
        drag.OnDragFinished -= Spawn;
    }
}
