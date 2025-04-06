using System.Collections.Generic;
using UnityEngine;

public class MusicTrack : MonoBehaviour
{
    [SerializeField] private List<MusicLayer> layers = new List<MusicLayer>();
    private Dictionary<string, AudioSource> trackSources = new Dictionary<string, AudioSource>();

    void Start()
    {
        foreach (MusicLayer layer in layers)
        {
            if (layer.clip == null || string.IsNullOrEmpty(layer.id)) continue;

            GameObject audioObj = new GameObject("Track_" + layer.id);
            audioObj.transform.SetParent(this.transform);

            AudioSource source = audioObj.AddComponent<AudioSource>();
            source.clip = layer.clip;
            source.loop = true;
            source.playOnAwake = false;
            source.mute = true;
            source.spatialBlend = 0;
            source.Play();

            trackSources[layer.id] = source;
        }
    }

    public void SetTrackState(string id, bool active)
    {
        if (trackSources.ContainsKey(id))
        {
            trackSources[id].mute = !active;
        }
        else
        {
            Debug.LogWarning("Track ID not found: " + id);
        }
    }
}
