using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day Length in Minutes")]
    [SerializeField]
    private float _targetDayLength = 0.25f; //length of day in minutes
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    [SerializeField]
    private int _dayNumber = 0; //tracks the days passed
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }
    [SerializeField]
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }
    private float _timeScale = 100f;
    [SerializeField]
    private int _yearLength = 100;
    public float yearLength
    {
        get
        {
            return _yearLength;
        }
    }

    public bool pause = false;

    [Header("Sun Light")]

    [SerializeField]
    private Transform dailyRotation;

    [SerializeField]
    private Light sun;
    private float intensity;

    [SerializeField]
    private float sunBaseIntensity = 1f;

    [SerializeField]
    private float sunVariation = 1.5f;

    [SerializeField]
    private Gradient sunColor;

    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            //UpdateClock();
        }

        AdjustSunRotation();
        SunIntensity();
        //AdjustSunColor();
    }

    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
        //_timeScale *= timeCurve.Evaluate(elapsedTime / (targetDayLength * 60)); //changes timescale based on time curve
        //_timeScale /= timeCurveNormalization; //keeps day length at target value
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400; // seconds in a day
        //elapsedTime += Time.deltaTime;
        if (_timeOfDay > 1) //new day!!
        {
            //elapsedTime = 0;
            _dayNumber++;
            _timeOfDay -= 1;

            if (_dayNumber > _yearLength) //new year!
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    //rotates the sun daily (and seasonally soon too);
    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f; //converts 0-1 value to degrees, so time of day multiply by 360 degrees
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));

        //float seasonalAngle = -maxSeasonalTilt * Mathf.Cos(dayNumber / yearLength * 2f * Mathf.PI);
        //sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(seasonalAngle, 0f, 0f));
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
    }

}