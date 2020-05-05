using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lift : MonoBehaviour
{
    [SerializeField] private Transform _origin, _target;
    [SerializeField] private float _speed = 3.0f;
    private bool _goingDown = false;


    public void CallLift() // respond for whether go up or down.
    {
        // _goingDown = true;
        // what happens if this is called again
        _goingDown =! _goingDown;
    }

    void FixedUpdate()
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _origin.position, _speed * Time.deltaTime);
        }
    }

    // ** jitter fix
    // detect player entering lift collider
    // if other is = to player
    // we parent player to lift
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform; // which is this lift obj
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
