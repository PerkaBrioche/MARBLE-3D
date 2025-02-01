using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void CalculScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }


    public void SlowMotion(float targetTimeScale, float duration)
    {
        StartCoroutine(SlowMotionCoroutine(targetTimeScale, duration));
    }

    public void ResetTimeScale(float duration)
    {
        StartCoroutine(ResetTimeScaleCoroutine(duration));
    }

    private IEnumerator SlowMotionCoroutine(float targetTimeScale, float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, targetTimeScale, t / transitionDuration);
            CalculScale(newScale);
            print(currentScale);
            yield return null;
        }

        CalculScale(targetTimeScale);
    }

    private IEnumerator ResetTimeScaleCoroutine(float transitionDuration)
    {
        float currentScale = Time.timeScale;
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float newScale = Mathf.Lerp(currentScale, 1.0f, t / transitionDuration);
            CalculScale(newScale);
            yield return null;
        }

        CalculScale(1.0f);
    }
}
