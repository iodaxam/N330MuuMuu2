using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
	private Animator anim;

	private float krakenAnimationTime;

    // Start is called before the first frame update

    void Start()
    {
		int n = Random.Range(1, 3);
		var idle = "Idle " + n.ToString();
        anim = GetComponentInChildren<Animator>();
		anim.Play(idle, 0, Random.value);
    }

	// private void LateUpdate(){
	// 	if (!attack){
	// 		anim.Play("Idle 1");
	// 	}
	// 	if (attack){
	// 		anim.Play("Attack 1");
	// 		// Debug.Log(anim.GetCurrentAnimatorClipInfo());
	// 		attack = false;
	// 	}
	// }

	public float getLength()
    {
		krakenAnimationTime = anim.GetCurrentAnimatorStateInfo(0).length;
		return krakenAnimationTime;
    }
	

    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
		    other.SendMessage("Kraken");
			krakenAnimationTime = anim.GetCurrentAnimatorStateInfo(0).length;
		    // do animation stuff
			anim.Play("Attack 1");
		}
	}
}