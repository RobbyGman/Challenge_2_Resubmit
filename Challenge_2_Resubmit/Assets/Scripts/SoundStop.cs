using UnityEngine;

public class SoundStop : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            Debug.Log(name + "Stopped Playing");
            Destroy(gameObject);
        }
    }
}
