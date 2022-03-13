using UnityEngine;

namespace GamesPlusJam.Player
{
    public class Victory : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().Victory();
            }
        }
    }
}
