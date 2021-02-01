using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewRenderer : MonoBehaviour
{
    private Camera _camera;
    private RenderTexture _shadowMask;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        _shadowMask = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.R8);
        Shader.SetGlobalTexture("_FieldOfViewMask", _shadowMask);

        GameObject shadowCamObject = new GameObject("ShadowCamera");
        shadowCamObject.transform.parent = transform;
        shadowCamObject.transform.localPosition = Vector3.zero;
        shadowCamObject.transform.localRotation = Quaternion.identity;

        Camera shadowCam = shadowCamObject.AddComponent<Camera>();
        shadowCam.CopyFrom(_camera);
        shadowCam.backgroundColor = Color.white;
        shadowCam.cullingMask = LayerMask.GetMask(new[] {"FieldOfView"});
        shadowCam.targetTexture = _shadowMask;
    }
}