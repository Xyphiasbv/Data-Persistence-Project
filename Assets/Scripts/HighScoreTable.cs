using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public int maxEntryCount;
    public float entryPlacementDifference = 50f;
    public Color firstPlaceColour;
    private List<Transform> highscoreEntryTransformList;
    public int lowestScore;
    public static HighScoreTable Instance;
    private string PlayerName;

    private void Awake() {

        if(Instance != null)
        {
            //Destroy(gameObject);
            return;
        }
        
        Instance = this;
        //DontDestroyOnLoad(gameObject); 
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.LoadName();
            PlayerName = ScoreManager.Instance.PlayerName;
        }

        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);   

        highscoreEntryTransformList = new List<Transform>();

        if(highScore == null){
            highScore = new HighScores();
            highScore.highScoreEntryList = new List<HighScoreEntry>(){
                new HighScoreEntry{ score = 0, name = "MyName"}
            };
        }     


        // Simple sorting algorithm - for each element you cycle thru each element under that one
        // Then you test to see which one has the higher score, and if needed swap them in the list
        for (int i = 0; i< highScore.highScoreEntryList.Count; i++)
        {
            for (int j = i; j < highScore.highScoreEntryList.Count; j++)
            {
                if (highScore.highScoreEntryList[j].score > highScore.highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScore.highScoreEntryList[i];
                    highScore.highScoreEntryList[i] = highScore.highScoreEntryList[j];
                    highScore.highScoreEntryList[j] = tmp;
                }
            }
        }
        // Limits score entries
        if (highScore.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScore.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScore.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        foreach (HighScoreEntry highScoreEntry in highScore.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
        
        
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -entryPlacementDifference * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
            rankString = rank + "TH";
            break;

            case 1:
            if(maxEntryCount == 1){
                rankString = "Best Score: ";
            }
            else{
                rankString = "1ST";    
            }            
            break;

            case 2:
            rankString = "2ND";
            break;

            case 3:
            rankString = "3RD";
            break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entryTransform.Find("NameText").GetComponent<Text>().text = highscoreEntry.name;

// Set background to alternate
        entryTransform.Find("BackGround").gameObject.SetActive(rank % 2 == 1);  

        transformList.Add(entryTransform);
        
    }
    public void AddHighScoreEntry(int score, string name)
    {
    // Create HighScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highScoreTmp = JsonUtility.FromJson<HighScores>(jsonString);

        // Limits score entries
        if (highScoreTmp != null && highScoreTmp.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScoreTmp.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScoreTmp.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        // Load saved HighScores
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);

        // Add new entry to HighscoreTable
        if (highScore == null)
        {
            var highScoreFirstEntry = new List<HighScoreEntry>() { highScoreEntry };
            highScore = new HighScores { highScoreEntryList = highScoreFirstEntry };
        }
        else
        {
            // Add new entry to list
            highScore.highScoreEntryList.Add(highScoreEntry);
        }
        // Save updated list
        string json = JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }
    // Represents a single High Score entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
    
}