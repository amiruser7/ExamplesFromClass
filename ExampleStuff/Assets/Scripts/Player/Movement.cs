using UnityEngine;
using UnityEngine.UIElements;//Allows us to connect

namespace Player
{
    [RequireComponent(typeof(CharacterController))] 
    public class Movement : MonoBehaviour
    {
        //octothorpe = # = Hash
        #region Variables
        [SerializeField]
        [Header("This is a title")]
        [Space(64)]
        [Tooltip("Hover Description")]
        //move direction that the player will be heading
        Vector3 _moveDirection = Vector3.zero;
        //the moevement speeds such as walk run jump
        [SerializeField] float _moveSpeed, _walk = 5, _sprint = 10, _crouch = 2.5f, _jump = 8;
        //how the player is going to not float away
        [SerializeField] float _gravity = 20;
        //a way to calculate physics
        [SerializeField] CharacterController _characterController;
        #endregion
        #region Function
        private void Awake()
        {
            //Assign a value to our reference by getting
            //This object that the script is attached to
            //Get a component on this object called Character Controller
            _characterController = this.GetComponent<CharacterController>();
            _moveSpeed = _walk;
            this.enabled = true;
        }
        private void Update()
        {
            if( _characterController != null )
            {
                if(_characterController.isGrounded)
                {
                    //Runs the Behaviour Container that allows us to change speed based on user input
                    SpeedControl();
                    _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    _moveDirection *= _moveSpeed;

                    if(Input.GetButton("Jump"))
                    {
                        _moveDirection.y = _jump;
                    }
                }
                _moveDirection.y -= _gravity * Time.deltaTime;
                _characterController.Move(_moveDirection * Time.deltaTime);
            }    
        }
        #endregion
        void SpeedControl()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _moveSpeed = _sprint;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                _moveSpeed = _crouch;
            }
            else
            {
                _moveSpeed = _walk;
            }
        }
    }
}