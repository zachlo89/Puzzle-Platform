using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LiftPanel : MonoBehaviour
{
    [SerializeField] GameObject _callButton;
    // [SerializeField] MeshRenderer _callButton; // shortcut direct access to meshrenderer.

    // ** lift and coins
    [SerializeField] private int _liftCoinGoal = 8;
    private Lift _lift;
    private bool _liftCalled = false;


    void Start()
    {
        _lift = GameObject.Find("Elevator").GetComponent<Lift>();

        if (_lift == null)
        {
            Debug.LogError("The Lift is NULL");
        }
    }

    void OnTriggerStay(Collider other) // will be called as long as we are standing w/ in trigger zone
    {
        if (other.tag == "Player")
        {
            int playerCoinCount = other.GetComponent<Player>().CoinCount();

            if (Input.GetKeyDown(KeyCode.E) && playerCoinCount >= _liftCoinGoal)
            {
                if (_liftCalled == true)
                {
                    _callButton.GetComponent<MeshRenderer>().material.color = Color.red;
                    // _callButton.material.color = Color.green;
                }
                else
                {
                    _callButton.GetComponent<MeshRenderer>().material.color = Color.green;
                    _liftCalled = true;
                }

                _lift.CallLift(); // call lift
            }
        }
    }
}
