using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight
{
    #region Member Variables

    // Sun Light Transform
    private Transform _dailyRotationRoot;
    private Transform _sunLightTransform;
    
    // Sun
    private Light _sunLight;
    private float _intensity;
    private float _sunBaseIntensity = 1f;
    private float _sunVariation = 1.5f;
    private Gradient _sunColor;

    #endregion



    #region Initialize
    
    public void Initialize(Transform dayCycleSystem)
    {
        if (dayCycleSystem == null) return;

        CreateDailyRotationRoot(dayCycleSystem);
        CreateSunColor();
    }
    
    private void CreateDailyRotationRoot(Transform dayCycleSystem)
    {
        var go = new GameObject { name = "DailyRotationRoot" };
        
        go.transform.SetParent(dayCycleSystem);
        _dailyRotationRoot = go.transform;
        
        CreateSunLight(go.transform);
    }

    private void CreateSunLight(Transform root)
    {
        var go = Managers.Resource.Instantiate(Literals.SUN_LIGHT, Literals.PATH_DAYCYCLE, root);

        _sunLightTransform = go.transform;
        _sunLightTransform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
        _sunLight = _sunLightTransform.GetComponent<Light>();
    }

    private void CreateSunColor()
    {
        _sunColor ??= new Gradient();

        var colorKeys = new GradientColorKey[3];
        colorKeys[0].color = Color.black;  // 0% 위치의 색상 (검정)
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = new Color32(135, 91, 54, 255); // 10% 위치의 색상 (#875B36)
        colorKeys[1].time = 0.1f;
        colorKeys[2].color = new Color32(253, 225, 143, 255); // 100% 위치의 색상 (#FDE18F)
        colorKeys[2].time = 1.0f;
        
        var alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.1f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;
        
        _sunColor.SetKeys(colorKeys, alphaKey);
    }

    #endregion



    #region Public Methods

    public void StartSunRotate()
    {
        CoroutineManagement.Instance.StartManagedCoroutine(SunRotateRoutine());
    }

    #endregion



    #region Coroutines

    private IEnumerator SunRotateRoutine()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            AdjustSunRotation();
            AdjustSunColor();
            SunIntensity();
        }
    }

    
    
    private void AdjustSunRotation()
    {
        float sunAngle = Managers.Game.TimeOfDay * 360f;
        
        _dailyRotationRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }

    private void AdjustSunColor()
    {
        _sunLight.color = _sunColor.Evaluate(_intensity);
    }

    private void SunIntensity()
    {
        _intensity = Vector3.Dot(_sunLight.transform.forward, Vector3.down);
        _intensity = Mathf.Clamp01(_intensity);

        _sunLight.intensity = _intensity * _sunVariation + _sunBaseIntensity;
    }
    
    
    
    #endregion
    
    
    
}
