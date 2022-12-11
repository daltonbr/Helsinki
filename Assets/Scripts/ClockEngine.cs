using System;
using System.Collections;
using PlasticPipe;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public class ClockEngine : MonoBehaviour
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform secondHand;

    private readonly WaitForSeconds oneSecondWait = new(1f);
    private DateTime timeNow;

    private Coroutine _clockCoroutine;
    
    private void OnEnable()
    {
        if (_clockCoroutine != null)
        {
            StopCoroutine(_clockCoroutine);        
        }
        
        _clockCoroutine = StartCoroutine(UpdateClock());
    }

    private void OnDisable()
    {
        if (_clockCoroutine != null)
        {
            StopCoroutine(_clockCoroutine);        
        } 
    }

    private IEnumerator UpdateClock()
    {
        while (true)
        {
            timeNow = DateTime.Now;

            var hour = timeNow.Hour;
            var minute = timeNow.Minute;
            var second = timeNow.Second;
        
            //Debug.Log($"{hour}:{minute}:{second} | {30f * (float)((hour % 12) + minute / 60f)}");
            
            hourHand.localRotation = Quaternion.Euler(30f * (hour % 12 + minute / 60f), 0f, 0f);
            minuteHand.localRotation = Quaternion.Euler(6f * minute, 0f, 0f);
            secondHand.localRotation = Quaternion.Euler(6f * second, 0f, 0f);

            yield return oneSecondWait;
        }
    }
}
