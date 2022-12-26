using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Assets.Scripts.UI
{
    public class AnimationWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Sequence _sequence;
        public void SetupScoreTextAnimation(float damage)
        {
            //if (_sequence == null)
            //    return;

            _sequence = DOTween.Sequence();

            _text.gameObject.SetActive(true);
            _text.gameObject.transform.position = Vector3.zero;
            _text.text = "+" + damage.ToString();
            _sequence.Append(_text.transform.DOLocalMoveY(50, 0.5f));
            //_sequence.Append(_text.DOFade(0, 2f)).OnComplete(() => ResetScoreText());
            _sequence.AppendCallback(() => { ResetScoreText(); });
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
        public void ResetScoreText()
        {
            if (_sequence == null)
                return;

            _text.DOFade(1, 0f);
            _sequence.Append(_text.transform.DOLocalMoveY(0, 0f));
            _text.gameObject.SetActive(false);
            //_sequence = null;
            //_text.rectTransform.position = Vector3.zero;
        }
    }
}
