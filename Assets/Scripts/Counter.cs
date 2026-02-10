using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    public void CountObjects()
    {
        // Находим все объекты с тегом "Item0"
        GameObject[] objects0 = GameObject.FindGameObjectsWithTag("Item0");
        GameObject[] objects1 = GameObject.FindGameObjectsWithTag("Item1");
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("Item2");
        GameObject[] objects3 = GameObject.FindGameObjectsWithTag("Item3");
        GameObject[] objects4 = GameObject.FindGameObjectsWithTag("Item4");
        GameObject[] objects5 = GameObject.FindGameObjectsWithTag("Item5");
        GameObject[] objects6 = GameObject.FindGameObjectsWithTag("Item6");
        GameObject[] objects7 = GameObject.FindGameObjectsWithTag("Item7");
        GameObject[] objects8 = GameObject.FindGameObjectsWithTag("Item8");
        GameObject[] objects9 = GameObject.FindGameObjectsWithTag("Item9");

        // Получаем их количество
        int count0 = objects0.Length;
        int count1 = objects1.Length;
        int count2 = objects2.Length;
        int count3 = objects3.Length;
        int count4 = objects4.Length;
        int count5 = objects5.Length;
        int count6 = objects6.Length;
        int count7 = objects7.Length;
        int count8 = objects8.Length;
        int count9 = objects9.Length;

        // Выводим результат в консоль
        Debug.Log($"Горох: {count0} " +
          $"Чеснок: {count1} " +
          $"Брокколи: {count2} " +
          $"Лук: {count3} " +
          $"Картошка: {count4} " +
          $"Помидор: {count5} " +
          $"Перец: {count6} " +
          $"Свекла: {count7} " +
          $"Капуста: {count8} " +
          $"Тыква: {count9}");

        int total_count = count0 + count1*2 + count2*4 + count3*8 +
            count4*16 + count5*32 + count6*64 + count7*128 + 
            count8*256 + count9*512;

        Debug.Log(total_count);
        scoreText.text = $"{total_count}";

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CountObjects(); // Вызов метода
        }
    }
}