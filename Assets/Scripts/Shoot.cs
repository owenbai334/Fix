using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float currentTime = 0.1f;
    private float invokeTime;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        invokeTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            invokeTime += Time.deltaTime;
            if (invokeTime - currentTime > 0)
            {
                Instantiate(Bullet, this.transform.position, Quaternion.identity);
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            invokeTime = currentTime;
        }

    }
}
