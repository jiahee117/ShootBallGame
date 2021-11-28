using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Slider slider;
    private Rigidbody rb;
    public float strength;
    
    private bool isMaxStrength = false;
    private GameManager gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        
        slider = GameObject.Find("Canvas").GetComponentInChildren<Slider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        
        
    }

    private void OnMouseDrag()
    {
        if (!gameManager.gameOver)
        {
            slider.value = strength / 10000;
            BuildStrength();
            checkStrength();
        }
    }

    void BuildStrength()
    {
        if (!isMaxStrength)
        {
            strength += 1500 * Time.deltaTime;
        }
        else strength -= 1500 * Time.deltaTime;
    }

    void checkStrength()
    {
        if (strength >= 10000)
        {
            isMaxStrength = true;
        }
        else if (strength <= 8000)
        {
            isMaxStrength = false;
        }
    }


    private void OnMouseUp()
    {       
        if (!gameManager.gameOver)
        {
            
            ShootTheBall();
            
            StartCoroutine("WaitForSpawnBall");
        }
        strength = 1600;
        slider.value = strength / 10000;
    }

    private void OnCollisionEnter(Collision collision)//destroy game object if touched the ground
    {        
        StartCoroutine("WaitBeforeDissapear");       
    }

    IEnumerator WaitBeforeDissapear()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        
    }

   IEnumerator WaitForSpawnBall()
    {
        yield return new WaitForSeconds(1);
        gameManager.SpawnBall();
    }

    void ShootTheBall()
    {
        rb.AddForce(Vector3.up * strength, ForceMode.Force);
        rb.AddForce(Vector3.forward * strength, ForceMode.Force);
        rb.useGravity = true;
    }
}
