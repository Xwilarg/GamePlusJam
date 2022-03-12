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

        /// <summary>
        /// Camera will attempt to stay at the same distance of the target
        /// </summary>
        [SerializeField]
        private Transform _follow;

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

        private bool _headToTarget = true;

        private void FixedUpdate()
        {
            var obj = transform.parent.position +
                new Vector3(
                    x: _orDist * Mathf.Sin(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI),
                    y: _orY,
                    z: _orDist * Mathf.Cos(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI)
                );
            Debug.DrawLine(transform.position, obj, Color.blue);
            Debug.DrawLine(transform.parent.position, transform.position, Color.red);
            Debug.DrawLine(transform.parent.position, obj, Color.green);
            if (_headToTarget)
            {
                var dist = Mathf.Abs((_originalLocalPos - transform.localPosition).magnitude);
                if (dist > 0.3f)
                {
                    _rb.velocity = (obj - transform.position).normalized;
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }

            // Keep looking at the player
            transform.LookAt(_target);
        }
    }
}
