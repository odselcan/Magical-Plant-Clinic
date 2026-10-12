using UnityEngine;

[CreateAssetMenu(fileName = "YeniMusteri", menuName = "Oyun Verileri/Musteri Karti")]
public class CustomerData : ScriptableObject
{
    [Header("Müşteri Bilgisi")]
    public string customerName;       // Örn: Cadı Lili, Gezgin Elf
    public Sprite portrait;           // Müşterinin yüz portresi

    [Header("Diyaloglar ({0} yazan yere otomatik bitki adı gelir)")]
    [TextArea(2, 4)]
    public string[] entryLines = new string[]
    {
        "{0}'ım hastalandı.",
        "Onu iyileştirmeme yardım eder misin?"
    };

    [TextArea(2, 4)]
    public string[] curedLines = new string[]
    {
        "İnanılmaz! {0}'ım yeniden canlandı.",
        "Çok teşekkür ederim!"
    };
}