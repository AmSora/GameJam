using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private MusicTrack musicTrack;

    private Dictionary<string, int> towerCount = new Dictionary<string, int>();

    private void Awake()
    {
        instance = this;
    }

    public void RegisterTower(string trackID)
    {
        if (musicTrack == null)
        {
            Debug.LogWarning("MusicTrack not assigned to MusicManager");
            return;
        }

        if (!towerCount.ContainsKey(trackID))
            towerCount[trackID] = 0;

        towerCount[trackID]++;

        if (towerCount[trackID] == 1)
        {
            musicTrack.SetTrackState(trackID, true);
        }
    }

    public void UnregisterTower(string trackID)
    {
        if (musicTrack == null)
        {
            Debug.LogWarning("MusicTrack not assigned to MusicManager");
            return;
        }

        if (string.IsNullOrEmpty(trackID))
        {
            Debug.LogWarning("Empty musicID passed to UnregisterTower");
            return;
        }

        if (!towerCount.ContainsKey(trackID))
        {
            Debug.LogWarning("Track ID not found in towerCount: " + trackID);
            return;
        }

        towerCount[trackID]--;
        Debug.Log("Unregister: " + trackID + " | Remaining: " + towerCount[trackID]);

        if (towerCount[trackID] <= 0)
        {
            towerCount[trackID] = 0;
            musicTrack.SetTrackState(trackID, false);
            Debug.Log("Music muted for track: " + trackID);
        }
    }
    [ContextMenu("Debug Tower Counts")]
    public void PrintCounts()
    {
        foreach (var entry in towerCount)
        {
            Debug.Log("Track: " + entry.Key + " | Count: " + entry.Value);
        }
    }

}
