using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.SceneManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [SerializeField]
        private float _fadeTime = 0.3f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();    
        }

        //Look at how we did fade in out
        public IEnumerator FadeInOut(float alpha)
        {
            AnimationCurve sCurve = AnimationCurve.EaseInOut(0, _canvasGroup.alpha, _fadeTime, alpha);
            float time = 0;
            while (time <= _fadeTime)
            {
                time += Time.deltaTime;
                var delta = sCurve.Evaluate(time);
                _canvasGroup.alpha = delta;
            }
            _canvasGroup.alpha = alpha; 

            yield return null;
        }
    }
}
