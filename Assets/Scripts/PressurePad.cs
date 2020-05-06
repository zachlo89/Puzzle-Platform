using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    // private Rigidbody _rb;
     void OnTriggerStay(Collider other)
    {
        // detect moving box
        if (other.tag == "MovingBox")
        {
            // when close to center; vec3.dist returns dist between two vectors
            // dist between A; myself tform.pos.other & B moving box
            float distance = Vector3.Distance(transform.position, other.transform.position);
            // get val of distance, so can see where box would sit
            Debug.Log("Distance: " + distance);
            // disable box at this dist
            if (distance < 0.05f)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if (box != null)
                {
                    // disable the box's rb or set it to kinematic
                    // get rb; iskinematic to tru; forces box to stop
                    box.isKinematic = true;
                }
                // cuz mesh renderer is on display child of pressure pad
                MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    // chng color of pad to green
                    renderer.material.color = Color.green;
                }

                Destroy(this);// destroy this component; so doesn't continuously call
            }
        }
    }
}
