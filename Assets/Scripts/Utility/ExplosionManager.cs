using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExplosionModes
{
    Haunting,
    Timing
}
public class ExplosionManager : MonoBehaviour
{
    /// <summary>
    /// Enum to handle different explosion modes
    /// </summary>
    public enum ExplosionModes { Haunting, Timing }
    [Tooltip("The modes used for explosion:" +
        "In Hauting mode explosion will pursue player" +
        "In timing mode the explosion will happen afetr stoppint of the timer")]
    public ExplosionModes explosionMode = ExplosionModes.Haunting;

    [Header("Component")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("Hauting Mode Component Settings")]
    [SerializeField] private Vector3 instantPosition;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private float waitTime = 0f;

    [Header("Timer Mode Component Settings")]
    [SerializeField] private Timer timer;
    private bool wasExploded = false;


    private void Start()
    {
        if (explosionMode == ExplosionModes.Haunting)
        {
            explosionPrefab = Instantiate(explosionPrefab, instantPosition,Quaternion.identity);
            explosionPrefab.GetComponent<AudioSource>().mute = true;
        }
    }
    void Update()
    {
        if (explosionMode == ExplosionModes.Haunting)
        {
            Invoke(nameof(BoomMove), waitTime);
            Invoke(nameof(MusicOn), waitTime);
        }
        else if (explosionMode == ExplosionModes.Timing && timer.currentTime <= timer.timerLimit && wasExploded == false)
        {   
            explosionPrefab = Instantiate(explosionPrefab,new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,
                Camera.main.transform.position.z - Camera.main.transform.position.z) ,Quaternion.identity);
            explosionPrefab.GetComponent<AudioSource>().loop = false;
            wasExploded = true;
        }
    }

    public void BoomMove()
    {
        
        explosionPrefab.transform.position += new Vector3(-1,0,0) * Time.deltaTime * movementSpeed;
    }

    public void MusicOn()
    {
        
        explosionPrefab.GetComponent<AudioSource>().mute = false;
    }
}
