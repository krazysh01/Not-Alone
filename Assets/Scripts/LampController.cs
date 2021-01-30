using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
[RequireComponent(typeof(FieldOfView))]
public class LampController : MonoBehaviour
{
    [SerializeField]
    private bool _lit;
    
    private FieldOfView _fieldOfView;

    public bool isLit
    {
        get
        {
            return _lit;
        }
    }
    
    void Start()
    {
        _fieldOfView = this.GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (_fieldOfView.isActive && !_lit)
        {
            _fieldOfView.ToggleFieldOfViewActive();
        } else if (!_fieldOfView.isActive && _lit)
        {
            _fieldOfView.ToggleFieldOfViewActive();
        }
    }

    public void toggleLit()
    {
        _lit = !_lit;
    }
}
