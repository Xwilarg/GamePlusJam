using UnityEngine;

namespace GamesPlusJam.Player
{
    public class LookAt : MonoBehaviour
    {
        /// <summary>
        /// Camera will always look at the target
        /// </summary>
        [SerializeField]
        private Transform _target;

        private Rigidbody _rb;
        private Vector3 _originalLocalPos;
        private float _orDist, _orY;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _originalLocalPos = transform.localPosition;
            _orDist = Vector3.Distance(transform.position, _target.position);
            _orY = _target.position.y;
        }

        private void FixedUpdate()
        {
            // Where camera should be
            var obj = transform.parent.position +
                new Vector3(
                    x: _orDist * Mathf.Sin(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI),
                    y: _orY,
                    z: _orDist * Mathf.Cos(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI)
                );
            Debug.DrawLine(transform.position, obj, Color.blue);
            Debug.DrawLine(transform.parent.position, transform.position, Color.red);
            Debug.DrawLine(transform.parent.position, obj, Color.green);
            var dist = Mathf.Abs((_originalLocalPos - transform.localPosition).magnitude);
            if (dist > 0.3f) // If camera is too far from where it's supposed to be, we move it
            {
                _rb.velocity = (obj - transform.position).normalized;
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }

            // Keep looking at the player
            transform.LookAt(_target);
        }
    }
}
