using UnityEngine;

[CreateAssetMenu(fileName = "YeniBitki", menuName = "Oyun Verileri/Bitki Karti")]
public class PlantData : ScriptableObject
{
    [Header("Bitki Bilgisi")]
    public string plantName;          // Örn: Moonflower, Lavanta

    [Header("Görseller")]
    public Sprite sickPortrait;       // Solmuş / hasta saksı görseli
    public Sprite healthyPortrait;    // İyileşmiş / canlı saksı görseli
}