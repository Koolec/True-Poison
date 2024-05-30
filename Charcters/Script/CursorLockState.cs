using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
[SerializeField] private List<KeyCode> keys;
[SerializeField] private bool currentState;

private void Switch(bool state) {
currentState = state;
Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
Cursor.visible = state;
}

private void Update() {
foreach (var key in keys)
if(Input.GetKeyDown(key))
Switch(!currentState);
}
}