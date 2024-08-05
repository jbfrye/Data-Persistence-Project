using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {
  public Brick BrickPrefab;
  public int LineCount = 6;
  public Rigidbody Ball;

  public Text ScoreText;
  public Text HighScoreText;

  private bool m_Started = false;
  private int m_Points;
  private bool m_GameOver = false;
  private GameManager _gameManager;


  void Start() {
    _gameManager = GameManager.Instance;
    const float step = 0.6f;
    int perLine = Mathf.FloorToInt(4.0f / step);

    int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
    for (int i = 0; i < LineCount; ++i) {
      for (int x = 0; x < perLine; ++x) {
        Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
        var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
        brick.PointValue = pointCountArray[i];
        brick.onDestroyed.AddListener(AddPoint);
      }
    }

    var highScore = GameManager.LoadHighScore();
    if (highScore != null) {
      HighScoreText.text = $"High Scores: {highScore.Name} - {highScore.Score}";
    }
  }

  private void Update() {
    if (!m_Started) {
      if (Input.GetKeyDown(KeyCode.Space)) {
        m_Started = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
      }
    }
  }

  void AddPoint(int point) {
    m_Points += point;
    ScoreText.text = $"Score : {m_Points}";
  }

  public void GameOver() {
    m_GameOver = true;
    _gameManager.CurrentScore = m_Points;
    SceneManager.LoadScene(1);
  }
}
