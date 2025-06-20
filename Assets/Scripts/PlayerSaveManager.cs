using UnityEngine;

public class PlayerSaveManager : SaveableObject
{
    public int points = 0;

    void Update()
    { // add points for testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            points += 10;
            Debug.Log($"Points added! Total: {points}");
        }
    }

    public override void Save()
    {
        // saves players position and points to playerprefs
        Vector3 pos = transform.position;
        PlayerData data = new PlayerData
        {
            x = pos.x,
            y = pos.y,
            z = pos.z,
            points = this.points
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
        Debug.Log($"Saved at {pos} with {points} points.");
    }

    public override void Load()
    {
        // load players position and points from playerprefs
        if (!PlayerPrefs.HasKey("PlayerData")) return;

        string json = PlayerPrefs.GetString("PlayerData");
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        transform.position = new Vector3(data.x, data.y, data.z);
        points = data.points;

        Debug.Log($"Loaded at ({data.x}, {data.y}, {data.z}) with {points} points.");
    }
}
