using UnityEngine;
using UnityEngine.Events;

public class CauldronTable : MonoBehaviour, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private GameObject _craftPanel;

    private void Start()
    { 
        _craftPanel = FindObjectOfType<CraftPanel>(true).gameObject;
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        if (!_craftPanel)
        {
            interactSuccessful = false;
        }
        else
        {
            _craftPanel.SetActive(true);
            interactSuccessful = true;
        }
    }

    public void EndInteraction()
    {
    }
}
