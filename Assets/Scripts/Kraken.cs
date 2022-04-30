 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class Kraken : MonoBehaviour
{

	public GameObject[] tentacles;
	List<Animator> animList = new List<Animator>();

	private float krakenAnimationTime;

    // Start is called before the first frame update

    void Start()
    {
		if (tentacles.Length >= 1)
         {
             for (int i = 0; i < tentacles.Length; i++) 
             {
				int idleNum = Random.Range(1, 3);
                 animList.Add(tentacles[i].GetComponent<Animator>()); 
                //  animList[i].enabled = false;
				switch (i) {
					case 0:
						animList[i].Play("Idle " + idleNum, 0, Random.value);
						break;
					case 1:
						animList[i].Play("Idle " + idleNum, 0, Random.value);
						break;
					case 2:
						animList[i].Play("Idle " + idleNum, 0, Random.value);
						break;
					case 3:
						animList[i].Play("Idle " + idleNum, 0, Random.value);
						break;
				}
             }
         }
         else
         {
             return; 
         }

    }
     public void FindTentacle(string tentacleName, string clipName)
     {
         if (tentacles.Length >= 1)
         {
			 Debug.Log("W");
             for (int i = 0; i < tentacles.Length; i++)
             {
				 Debug.Log(tentacles[i].name + " " + tentacleName);
                 if(tentacles[i].name == tentacleName)
                 {
					 Debug.Log("W");
                     animList[i].enabled = true;
                     animList[i].Play(clipName, 0, Random.Range(0.0f, 0.25f));
					 Debug.Log(clipName);
                 } else {
					  Debug.Log("L");
				 }
             }
         }
         else
         {
			 Debug.Log("L");
             return;
         }
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
			for (int i = 0; i < tentacles.Length; i++) {
		    	string tentacleGameobjectName = this.gameObject.transform.GetChild(i).name;
				FindTentacle(tentacleGameobjectName, "Attack 1");
			}
			other.SendMessage("Kraken");
		}
	}
}