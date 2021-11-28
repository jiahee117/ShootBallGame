using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    private bool isMaxRange;
    public float speed = 20f;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isMaxRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameOver)
        {
            CheckRange();
            if (isMaxRange)
            {
                transform.position += Vector3.back * Time.deltaTime * speed;
            }
            else transform.position += Vector3.forward * Time.deltaTime * speed;
        }
    }

    void CheckRange()
    {
        if(transform.position.z >= 30)
        {
            isMaxRange = true;
            
        }else if(transform.position.z <= 0)
        {
            isMaxRange = false;           
        }
    }

}
