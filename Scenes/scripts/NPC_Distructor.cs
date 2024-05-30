using UnityEngine;

public class NPC_Distructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rio"))
        {
            other.gameObject.SetActive(false);
        }
    }
}