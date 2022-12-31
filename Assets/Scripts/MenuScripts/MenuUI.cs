using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public string playerName, bestPlayer;

    public int score, bestScore;

    public TMP_InputField playerNameInput;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI statsInGame;

    private void Start()
    {
        LoadScore();
        LoadName();

        if (bestScore > 0)
        {
            bestScoreText.text = ("Best Score: " + bestScore + " by " + bestPlayer);
        }
        else
        {
            bestScoreText.text = ("No one played yet");
        }

        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void OnPlayerNameInput()
    {
        playerName = playerNameInput.text;
    }



    [System.Serializable]

    // Variables
    class SaveData
    {
        public int bestScore;
        public string bestPlayer;
    }

    // Saving best players name to .json.

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.bestPlayer = bestPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/save_player_name.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/save_player_name.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.bestPlayer;
        }
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/save_score_file.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/save_score_file.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
        }
    }

    // Option that is executed when butto nis clicked. Checks if the name is entered and if it is loads the game
    public void LoadSceneOnStart()
    {
        if (playerNameInput.text == "")
        {
            bestScoreText.text = "Please enter your name!";
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            LoadScore();
            LoadName();

            statsInGame = GameObject.Find("Best Player Stats").GetComponent<TextMeshProUGUI>();
            statsInGame.text = ("Best Score: " + bestScore + " by " + bestPlayer);
        }
    }
}