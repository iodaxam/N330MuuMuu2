using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGGINGSCRIPT : MonoBehaviour
{
    private Ray ray;
    public LayerMask terrainLayer;

    private Vector3 HitPosition;

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(gameObject.transform.position, Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit info, 400, terrainLayer.value))
        {
            Vector3 CurrentPosition = gameObject.transform.position;
            //gameObject.transform.position = new Vector3(CurrentPosition.x, info.point.y, CurrentPosition.z);
            //Debug.Log(info.point);
            HitPosition = info.point;

            //info.collider.gameObject.tag = "Respawn";
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(HitPosition, 2f);
    }

}
