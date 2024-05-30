using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Borshevik.EnemyControl
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int HitsToDeath = 3;
        public DataSender data;

        private void OnTriggerEnter(Collider other)
        {
            HitsToDeath--;
            if(HitsToDeath < 1)
            {
                Destroy(this.gameObject);
                data.Send("Enemy");
            }
        }
    }
}
