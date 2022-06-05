using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject treasureOpen;
    public GameObject treasureClose;
    public ParticleSystem firework1;
    public ParticleSystem firework2;

    #region Singleton
    public static TreasureManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    void Start()
    {
        treasureOpen.SetActive(false);
        treasureClose.SetActive(true);
    }

    public void WaitToOpenTreasure(float time)
    {
        Invoke(nameof(OpenTreasure), time);
    }

    private void OpenTreasure()
    {
        treasureOpen.SetActive(true);
        treasureClose.SetActive(false);
    }

    public void StartFireworks()
    {
        firework1.Play();
        firework2.Play();
    }

}
