using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    int Fruits = 0;

    [SerializeField] private Text fruitText;

    [SerializeField] private AudioSource collectionSoundEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            collectionSoundEffect.Play();
            Fruits++;
            fruitText.text = "Fruits: " + Fruits;
            ScoreManager.instance.AddPoint();
        }
    }
}
