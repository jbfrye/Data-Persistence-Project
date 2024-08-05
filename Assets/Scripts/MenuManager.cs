using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
  [SerializeField] private TMP_InputField _nameField;
  [SerializeField] private TMP_Text _scoreText;
  private GameManager _gameManager;

  void Start() {
    _gameManager = GameManager.Instance;
    _scoreText.text = $"Score: {_gameManager.CurrentScore}";
  }

  public void Restart() {
    SaveScore();
    SceneManager.LoadScene(0);
  }

  public void Quit() {
    SaveScore();
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }

  private void SaveScore() {
    if (_nameField.text.Length > 0) {
      GameManager.GameScore gameScore = new GameManager.GameScore(_nameField.text, _gameManager.CurrentScore);
      GameManager.SaveScore(gameScore);
    }
  }
}
