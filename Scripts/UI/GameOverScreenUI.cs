using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GameOverScreenUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text playerLevelText;
        [SerializeField] private Button restartButton;
        private CanvasGroup _canvasGroup;
        
        public Button.ButtonClickedEvent RestartButtonPressed => restartButton.onClick;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            RestartButtonPressed.AddListener(Hide);
            Hide();
        }


        public void Show(int finalScore, int highScore, int playerLevel)
        {
            _canvasGroup.DOFade(1, 0.5f).SetDelay(1f).OnComplete(() => _canvasGroup.blocksRaycasts = true);
            finalScoreText.text = finalScore.ToString();
            highScoreText.text = highScore.ToString();
            playerLevelText.text = playerLevel.ToString();

            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}