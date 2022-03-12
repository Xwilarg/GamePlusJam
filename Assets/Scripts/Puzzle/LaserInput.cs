using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class LaserInput : MonoBehaviour
    {
        private LineRenderer _renderer;

        [SerializeField]
        private Transform _laserStart;

        private void Start()
        {
            _renderer = GetComponent<LineRenderer>();

            _renderer.SetPosition(0, _laserStart.position);
        }

        private void Update()
        {
            var dist = Vector3.Distance(_laserStart.position, transform.position);
            Physics.Raycast(new Ray(_laserStart.position,
                new Vector3(
                    x: dist * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2),
                    y: 0f,
                    z: dist * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2)
                )), out RaycastHit hitInfo);
            _renderer.SetPosition(1, hitInfo.point);
        }
    }
}
