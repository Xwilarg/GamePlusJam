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
        private Vector3 _originalPos;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _originalPos = transform.localPosition;
        }

        private bool _headToTarget = true;

        private void FixedUpdate()
        {
            var obj = transform.parent.position +
                new Vector3(
                    x: 1f * Mathf.Sin(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI),
                    y: 1f,
                    z: 1f * Mathf.Cos(transform.parent.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI)
                );
            Debug.DrawLine(transform.position, obj, Color.blue);
            Debug.DrawLine(transform.parent.position, transform.position, Color.red);
            Debug.DrawLine(transform.parent.position, obj, Color.green);
            if (_headToTarget)
            {
                var dist = Mathf.Abs((_originalPos - transform.localPosition).magnitude);
                if (dist > 0.1f)
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
