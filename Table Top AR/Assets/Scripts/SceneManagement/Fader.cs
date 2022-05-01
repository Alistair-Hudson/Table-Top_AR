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
            var initAlpha = _canvasGroup.alpha;
            AnimationCurve sCurve = AnimationCurve.EaseInOut(0, 0, _fadeTime, 1);
            float time = 0;
            while (time <= _fadeTime)
            {
                var t = sCurve.Evaluate(time);
                _canvasGroup.alpha = Mathf.Lerp(initAlpha, alpha, t);
                yield return null;
                time += Time.deltaTime;
            }
            _canvasGroup.alpha = alpha; 
        }
    }
}
