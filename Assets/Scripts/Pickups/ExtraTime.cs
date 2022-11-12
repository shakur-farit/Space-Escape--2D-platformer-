using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTime : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [Tooltip("how much extra time will give (in seconds)")]
    [SerializeField] private float extraTime;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("im here");
            timer.currentTime += extraTime;
            Destroy(this.gameObject);
        }
    }
}
