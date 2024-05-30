using UnityEngine;

public class NPC_Task : MonoBehaviour
{
    public bool EndDialog;
    public GameObject Dialog1;
    public GameObject Dialog2;
    public Quest_Event QE;
    public bool Fin_Dialog;
    private bool onHide;

    private void Update()
    {
        if (QE.end_Quest1 == true)
        {
            Fin_Dialog = true;
        }
        if (EndDialog == true)
        {
            Time.timeScale = 1;
            QE.Quest1 = true;
            Dialog1.SetActive(false);
        }
        if (Fin_Dialog == true)
        {
            Time.timeScale = 1;
            QE.Quest1 = false;
            Dialog1.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && Fin_Dialog == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {

                    if (Fin_Dialog == false)
                    {
                        if (QE.end_Quest1 == false)
                        {
                            Dialog1.SetActive(true);
                        }
                        else
                        {
                            Dialog2.SetActive(true);
                        }
                    }
                    else
                    {
                        if (onHide == false)
                        {
                            Dialog2.SetActive(true);
                            onHide = true;
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && onHide == false)
        {
            Dialog2.SetActive(true);
            onHide = true;
        }
    }
}