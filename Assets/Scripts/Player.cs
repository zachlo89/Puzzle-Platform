using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField]  private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private Vector3 _direction, _velocity;
    private Vector3 _wallSurfaceNormal;
    private bool _canDoubleJump = false;
    private bool _canWallJump = false;
    private UIManager _uiManager;
    [SerializeField] private int _lives = 3;

    // ** lift and coins
    [SerializeField] private int _coins;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }
        _uiManager.UpdateLivesDisplay(_lives);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _canWallJump = true;
            // dir and velocity only calc if grounded is true
            // commit to jump unless grounded 
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else // not grounded and able to wall jump
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false) 
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = _jumpHeight; // jump boost
                // velocity nn = surface normal of wall; bounce off at speed of player
                _velocity = _wallSurfaceNormal * _speed;
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }


// hit the wall and not grounded then able to wall jump
// detect platform 3 and 4 tag as wall
// tk curr velocity and reassign to surface normal


    /// <summary>
    /// OnControllerColliderHit is called when the char controller hits a
    /// collider/anything while performing a Move.
    /// use surface normals
    /// </summary>
    /// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        // condition when to wall jump
        // if not grounded and touching a wall
        // means we are in mid air; and if we are hitting a wall
        if (_controller.isGrounded == false && hit.transform.tag == "Wall") 
        // if this is true then we want to draw surface normal that we nn to reflect off of
        {
            // hit is impact point
            // hit.normal is the normal dir; perpindicular to surface
            Debug.DrawRay(hit.point, hit.normal, Color.blue);

            _wallSurfaceNormal = hit.normal;

            // if this works we can wall jump
            _canWallJump = true;
        }
    }


    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }


    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    // how many coins hv
    public int CoinCount()
    {
        return _coins;
    }
}
