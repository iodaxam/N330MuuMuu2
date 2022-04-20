using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
	private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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

    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
		    other.SendMessage("Kraken");
		    // do animation stuff
			anim.Play("Attack 1");
		}
	}
}