using GamesPlusJam.SO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamesPlusJam
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private List<AudioClip> _footstepsWalk, _footstepsRun;

        private AudioSource _audioSource;
        private CharacterController _controller;
        private bool _isSprinting;
        private float _verticalSpeed;
        private float _footstepDelay;

        private Vector2 _mov;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;

            _footstepsWalk = _info.FootstepsWalk.ToList();
            _footstepsRun = _info.FootstepsRun.ToList();
        }

        private void FixedUpdate()
        {
            var pos = _mov;
            Vector3 desiredMove = transform.forward * pos.y + transform.right * pos.x;

            // Get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _controller.radius, Vector3.down, out RaycastHit hitInfo,
                               _controller.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            Vector3 moveDir = Vector3.zero;
            moveDir.x = desiredMove.x * _info.ForceMultiplier * (_isSprinting ? _info.SpeedRunningMultiplicator : 1f);
            moveDir.z = desiredMove.z * _info.ForceMultiplier * (_isSprinting ? _info.SpeedRunningMultiplicator : 1f);

            if (_controller.isGrounded && _verticalSpeed < 0f) // We are on the ground and not jumping
            {
                moveDir.y = -.1f; // Stick to the ground
                _verticalSpeed = -_info.GravityMultiplicator;
            }
            else
            {
                // We are currently jumping, reduce our jump velocity by gravity and apply it
                _verticalSpeed += Physics.gravity.y * _info.GravityMultiplicator;
                moveDir.y += _verticalSpeed;
            }

            var p = transform.position;
            _controller.Move(moveDir);

            // Footsteps
            _footstepDelay -= Vector3.SqrMagnitude(p - transform.position);
            if (_footstepDelay < 0f)
            {
                // TODO: Need audio
                /*
                var target = _isSprinting ? _footstepsRun : _footstepsWalk;
                var clipIndex = Random.Range(1, target.Count);
                var clip = target[clipIndex];
                target.RemoveAt(clipIndex);
                target.Insert(0, clip);

                _audioSource.PlayOneShot(clip);
                _footstepDelay += _info.FootstepDelay * (_isSprinting ? _info.FootstepDelayRunMultiplier : 1f);
                */
            }
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>().normalized;
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            var rot = value.ReadValue<Vector2>();

            transform.rotation *= Quaternion.AngleAxis(rot.x * _info.HorizontalLookMultiplier, Vector3.up);

            // TODO: Vertical rotation
            /*
            _headRotation -= mousePos.y * _info.VerticalLookMultiplier; // Vertical look is inverted by default, hence the -=

            _headRotation = Mathf.Clamp(_headRotation, -89, 89);
            _head.transform.localRotation = Quaternion.AngleAxis(_headRotation, Vector3.right);
            */
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            if (_controller.isGrounded)
            {
                _verticalSpeed = _info.JumpForce;
            }
        }

        public void OnSprint(InputAction.CallbackContext value)
        {
            _isSprinting = value.ReadValueAsButton();
        }
    }
}
