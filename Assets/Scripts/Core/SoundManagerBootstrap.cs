using UnityEngine;

public class SoundManagerBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject soundManagerPrefab;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            Instantiate(soundManagerPrefab);
        }
    }
}