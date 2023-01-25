using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class DoTweenHelperCustom
{

    public static void DoMoveContinuous(this Transform originalTransform, Transform target, float totalTime)
    {
        if (MonoHelper.Instance is null)
        {
            GameObject a = new GameObject("MonoHelper");
            MonoHelper monoHelper = a.AddComponent<MonoHelper>();
            monoHelper.StartCoroutine(MoveCo());
        }
        else
        {
            MonoHelper.Instance.StartCoroutine(MoveCo());
        }
        

        IEnumerator MoveCo()
        {
            float timeElapsed = 0;
            Vector3 original = originalTransform.position;
    
            while (timeElapsed <= totalTime)
            {
                timeElapsed += Time.deltaTime;
                originalTransform.position = Vector3.Lerp(original, target.position, timeElapsed / totalTime);
                yield return null;
            }
        
            originalTransform.position = target.position;
        }
    }
    
}

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
