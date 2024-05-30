using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Wood : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        _animator.SetBool("MouseOver", true);
    }
    private void  OnMouseEnd()
    {
        _animator.SetBool("MouseOver", false);
    }
    private void OnMouseDown()
    {
        
    }
}
