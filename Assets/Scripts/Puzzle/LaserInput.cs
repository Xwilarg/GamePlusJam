﻿using System;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class LaserInput : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer[] _renderers;

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
            foreach (var renderer in _renderers)
            {
                renderer.enabled = false;
            }
            DrawLaser(_renderer, _laserStart.position, () =>
            {
                return transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2;
            });
        }

        public void DrawLaser(LineRenderer renderer, Vector3 startPos, Func<float> angle)
        {
            renderer.enabled = true;
            renderer.SetPosition(0, startPos);
            Physics.Raycast(new Ray(startPos,
                new Vector3(
                    x: Mathf.Sin(angle()),
                    y: 0f,
                    z: Mathf.Cos(angle())
                )), out RaycastHit hitInfo);
            renderer.SetPosition(1, hitInfo.point);

            if (hitInfo.collider.CompareTag("Mirror"))
            {
                DrawLaser(hitInfo.collider.GetComponent<LineRenderer>(), hitInfo.point, () =>
                {
                    return hitInfo.collider.transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2;
                });
            }
        }
    }
}