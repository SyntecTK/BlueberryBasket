using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.GameOver += StopBGM;
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= StopBGM;
    }
    private void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
    }
}
