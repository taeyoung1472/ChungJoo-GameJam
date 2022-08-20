using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class EffectManager : MonoSingleTon<EffectManager>
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Image colorPanelImage;

    float shakeTimer = 0;
    float shakeForce;

    Sequence zoomSeq;
    Sequence panelSeq;

    public void Update()
    {
        if(shakeTimer > 0)
        {
            shakeForce = Mathf.Lerp(shakeForce, 5, Time.deltaTime * 5);
        }
        else
        {
            shakeForce = Mathf.Lerp(shakeForce, 0, Time.deltaTime * 5);
        }

        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeForce;

        shakeTimer -= Time.deltaTime;
    }

    public void Shake(float time)
    {
        if(time > shakeTimer)
        {
            shakeTimer = time;
        }
    }
    public void ZoomOut(float value)
    {
        zoomSeq?.Kill();
        zoomSeq = DOTween.Sequence();
        zoomSeq.Append(DOTween.To(() => vcam.m_Lens.OrthographicSize, x => vcam.m_Lens.OrthographicSize = x, value, 0.2f).SetUpdate(true)).SetUpdate(true);
        zoomSeq.Append(DOTween.To(() => vcam.m_Lens.OrthographicSize, x => vcam.m_Lens.OrthographicSize = x, 5, 1f).SetUpdate(true)).SetUpdate(true);
    }
    public void ActiveColorPanel(Color color)
    {
        colorPanelImage.color = color;
        panelSeq?.Kill();
        panelSeq = DOTween.Sequence();
        panelSeq.Append(colorPanelImage.DOFade(0.5f, 0).SetUpdate(true)).SetUpdate(true);
        panelSeq.Append(colorPanelImage.DOFade(0f, 0.5f).SetUpdate(true)).SetUpdate(true);
    }
}
