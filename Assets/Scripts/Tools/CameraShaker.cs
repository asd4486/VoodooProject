using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShaker : MonoBehaviour
{
    Camera camera;

    private float environmentSpeedBeforeFreeze;

    void Awake()
    {
        camera = GetComponent<Camera>();
        environmentSpeedBeforeFreeze = GameManager.Instance.environmentSpeed;
    }

    public void CameraLittleDownShake()
    {
        camera.transform.DOShakePosition(0.1f, 0.11f);
    }

    public void CameraHardDownShake()
    {
        camera.transform.DOShakePosition(0.1f, 0.1f);
    }

    public void CameraOrthoSize()
    {
        camera.DOOrthoSize(2f, 0.15f).OnComplete(() => camera.DOOrthoSize(3f, 0.15f));
    }

    public void FreezeMovement()
    {
        GameManager.Instance.environmentSpeed = 0f;
    }

    public void UnfreezeMovement()
    {
        GameManager.Instance.environmentSpeed = environmentSpeedBeforeFreeze;
    }
}
