using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Quest : MonoBehaviour
{
    public Quest_Event QEvent;
    void Start()
    {
        
    }

    void Update()
    {

    }
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
        {
            QEvent.end_Quest1 = true;
            Destroy(gameObject);
        }
    }
}
