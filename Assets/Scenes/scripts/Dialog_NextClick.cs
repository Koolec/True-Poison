using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_NextClick : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    private bool isText1 = true;
    public NPC_Task npc_taskScript;
    public bool Fin_Dialog;
    public GameObject ObjectQuest;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isText1 == true)
            {
                isText1 = false;
            }
            else
            {
                if (Fin_Dialog == false)
                {
                    isText1 = true;
                    if (ObjectQuest != null)
                        ObjectQuest.SetActive(true);
                    if (npc_taskScript != null)
                        npc_taskScript.EndDialog = true;
                }
                else
                {
                    isText1 = true;
                    if (npc_taskScript != null)
                        npc_taskScript.Fin_Dialog = true;
                }
            }

        }
        if (isText1 == true)
        {
            if (Text1 != null)
                Text1.SetActive(true);
            if (Text2 != null)
                Text2.SetActive(false);
        }
        else
        {
            if (Text1 != null)
                Text1.SetActive(false);
            if (Text2 != null)
                Text2.SetActive(true);
        }
    }
}