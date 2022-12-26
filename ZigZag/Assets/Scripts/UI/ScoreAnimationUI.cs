using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Assets.Scripts.UI
{
    public class ScoreAnimationUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Sequence _sequence;
        public void SetupScoreTextAnimation(float damage)
        {
            _text.text = "-" + damage.ToString();

            _sequence = DOTween.Sequence();
            _sequence.Append(gameObject.transform.DOLocalMoveY(50, 4f));
            _sequence.Append(_text.DOFade(0, 4f));

            _sequence.AppendCallback(() => { ResetScoreText(); });
        }

        public void ResetScoreText()
        {
            _sequence?.Kill();
            _text.DOFade(1, 0f);
            gameObject.SetActive(false);
        }
    }
}