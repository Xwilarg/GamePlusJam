using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class RotatePlanets : MonoBehaviour
    {
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var c = transform.GetChild(i);
                c.transform.rotation = Quaternion.Euler(
                    c.transform.rotation.x,
                    Random.Range(0, 360),
                    c.transform.rotation.z
                    );
            }
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(
                x: transform.rotation.eulerAngles.x,
                y: transform.rotation.eulerAngles.y + Time.deltaTime * -50f,
                z: transform.rotation.eulerAngles.z
                );
        }
    }
}
