using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBombScript : MonoBehaviour
{
    public GameObject BombObject;
    public GameObject Target;

    public GameObject SplashPE;

    public float speed;

    private bool Moving;
    private bool DoOnce = true;

    void Start()
    {
        BombObject.transform.Find("SM_Env_Rock_01").transform.rotation = Quaternion.Euler(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
    }

    void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player")
        {
            Moving = true;
        }
    }

    void Update()
    {
        if(Moving && (BombObject.transform.position.y > Target.transform.position.y))
        {
            BombObject.transform.position += new Vector3(0, (-speed * Time.deltaTime), 0);
        }
        if((BombObject.transform.position.y <= (Target.transform.position.y + 50)) && DoOnce)
        {
            DoOnce = false;

            GameObject PE = Instantiate(SplashPE, Target.transform.position, Quaternion.identity);

            PE.transform.localScale = new Vector3(2,2,2);
        }
    }
}
