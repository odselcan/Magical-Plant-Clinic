using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }

    // =========================
    // SAĞLIKLI MOONFLOWER
    // =========================

    [SerializeField] private Sprite healthyMoonflower;


    // =========================
    // DİYALOG
    // =========================

    private UIDocument dialogueDocument;
    private VisualElement m_NPCDialogue;
    private VisualElement m_Flower;
    private Label m_DialogueText;

    private Button m_CloseButton;
    private Button m_FinishButton;


    // =========================
    // TEDAVİ
    // =========================

    private UIDocument treatmentDocument;
    private VisualElement m_TreatmentPanel;

    private Button m_TreatButton;
    private Button m_CollectMaterialsButton;

    private Label m_MaterialMoonleaf;
    private Label m_MaterialWater;


    // =========================
    // AWAKE
    // =========================

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        dialogueDocument = GetComponent<UIDocument>();
    }


    // =========================
    // START
    // =========================

    private void Start()
    {
        // =========================
        // DİYALOG UI
        // =========================

        if (dialogueDocument == null)
        {
            Debug.LogError(
                "UIHandler üzerinde UIDocument yok!"
            );

            return;
        }

        var dialogueRoot =
            dialogueDocument.rootVisualElement;


        // DİYALOG PANELİ

        m_NPCDialogue =
            dialogueRoot.Q<VisualElement>("background");

        if (m_NPCDialogue == null)
        {
            Debug.LogError(
                "background bulunamadı!"
            );
        }


        // ÇİÇEK

        m_Flower =
            dialogueRoot.Q<VisualElement>("flower");

        if (m_Flower == null)
        {
            Debug.LogError(
                "flower bulunamadı!"
            );
        }
        m_DialogueText =
    dialogueRoot.Q<Label>("label");


        // İLK DİYALOG BUTONU

        m_CloseButton =
            dialogueRoot.Q<Button>("CloseButton");

        if (m_CloseButton == null)
        {
            Debug.LogError(
                "CloseButton bulunamadı!"
            );
        }
        else
        {
            m_CloseButton.clicked += OpenTreatment;
        }


        // BİTİR BUTONU

        m_FinishButton =
            dialogueRoot.Q<Button>("FinishButton");

        if (m_FinishButton == null)
        {
            Debug.LogError(
                "FinishButton bulunamadı!"
            );
        }
        else
        {
            m_FinishButton.clicked += FinishDialogue;

            m_FinishButton.style.display =
                DisplayStyle.None;
        }


        // =========================
        // TEDAVİ UI
        // =========================

        GameObject treatmentObject =
            GameObject.Find("TreatmentUI");

        if (treatmentObject == null)
        {
            Debug.LogError(
                "TreatmentUI bulunamadı!"
            );

            return;
        }

        treatmentDocument =
            treatmentObject.GetComponent<UIDocument>();

        if (treatmentDocument == null)
        {
            Debug.LogError(
                "TreatmentUI üzerinde UIDocument yok!"
            );

            return;
        }

        var treatmentRoot =
            treatmentDocument.rootVisualElement;


        // TEDAVİ PANELİ

        m_TreatmentPanel =
            treatmentRoot.Q<VisualElement>(
                "TreatmentPanel"
            );


        // TEDAVİ BUTONU

        m_TreatButton =
            treatmentRoot.Q<Button>(
                "TreatButton"
            );


        // MALZEMELERİ TOPLA

        m_CollectMaterialsButton =
            treatmentRoot.Q<Button>(
                "CollectMaterialsButton"
            );


        // MALZEME YAZILARI

        m_MaterialMoonleaf =
            treatmentRoot.Q<Label>(
                "MaterialMoonleaf"
            );

        m_MaterialWater =
            treatmentRoot.Q<Label>(
                "MaterialWater"
            );


        // =========================
        // BUTON KONTROLLERİ
        // =========================

        if (m_TreatButton == null)
        {
            Debug.LogError(
                "TreatButton bulunamadı!"
            );
        }
        else
        {
            m_TreatButton.clicked -= TreatPlant;
            m_TreatButton.clicked += TreatPlant;
        }


        if (m_CollectMaterialsButton == null)
        {
            Debug.LogError(
                "CollectMaterialsButton bulunamadı!"
            );
        }
        else
        {
            m_CollectMaterialsButton.clicked -=
                CollectMaterials;

            m_CollectMaterialsButton.clicked +=
                CollectMaterials;
        }


        // =========================
        // BAŞLANGIÇ
        // =========================

        HideDialogue();

        HideTreatment();

        UpdateMaterialDisplay();
    }


    // =========================
    // DİYALOGU GÖSTER
    // =========================

    public void DisplayDialogue()
    {
        Debug.Log(
            "Diyalog açılıyor..."
        );


        // Hasta çiçeği geri getir

        if (m_Flower != null &&
            healthyMoonflower != null)
        {
            // Diyalog yeniden başladığında
            // sağlıklı görsel yerine hasta görseli
            // UXML'deki orijinal haline dönsün.

            m_Flower.style.backgroundImage =
                new StyleBackground(
                    Resources.Load<Texture2D>(
                        "Moonflower"
                    )
                );
        }


        if (m_NPCDialogue != null)
        {
            m_NPCDialogue.style.display =
                DisplayStyle.Flex;
        }


        if (m_FinishButton != null)
        {
            m_FinishButton.style.display =
                DisplayStyle.None;
        }


        if (m_CloseButton != null)
        {
            m_CloseButton.style.display =
                DisplayStyle.Flex;
        }
    }


    // =========================
    // DİYALOGU KAPAT
    // =========================

    private void HideDialogue()
    {
        if (m_NPCDialogue != null)
        {
            m_NPCDialogue.style.display =
                DisplayStyle.None;
        }
    }


    // =========================
    // TEDAVİ EKRANINI AÇ
    // =========================

    private void OpenTreatment()
    {
        Debug.Log(
            "TEDAVİ EKRANI AÇILIYOR!"
        );


        HideDialogue();

        UpdateMaterialDisplay();


        if (m_TreatmentPanel != null)
        {
            m_TreatmentPanel.style.display =
                DisplayStyle.Flex;
        }
    }


    // =========================
    // TEDAVİYE DEVAM ET
    // =========================

    public void OpenTreatmentAgain()
    {
        Debug.Log(
            "TEDAVİYE DEVAM EDİLİYOR!"
        );


        HideDialogue();

        UpdateMaterialDisplay();


        if (m_TreatmentPanel != null)
        {
            m_TreatmentPanel.style.display =
                DisplayStyle.Flex;
        }
    }


    // =========================
    // TEDAVİ PANELİNİ KAPAT
    // =========================

    private void HideTreatment()
    {
        if (m_TreatmentPanel != null)
        {
            m_TreatmentPanel.style.display =
                DisplayStyle.None;
        }
    }


    // =========================
    // MALZEMELERİ GÖSTER
    // =========================

    private void UpdateMaterialDisplay()
    {
        if (GameManager.Instance == null)
        {
            return;
        }


        if (m_MaterialMoonleaf != null)
        {
            m_MaterialMoonleaf.text =
                GameManager.Instance.HasMoonleaf
                ? "🌿 Moonleaf: 1 / 1   ✓"
                : "🌿 Moonleaf: 0 / 1   ↬   Büyülü Sera";
        }


        if (m_MaterialWater != null)
        {
            m_MaterialWater.text =
                GameManager.Instance.HasMagicalWater
                ? "💧 Magical Water: 1 / 1   ✓"
                : "💧 Magical Water: 0 / 1   ↬   İksir Dükkanı";
        }


        UpdateTreatButton();
    }


    // =========================
    // TEDAVİ BUTONU
    // =========================

    private void UpdateTreatButton()
    {
        if (m_TreatButton == null)
        {
            return;
        }


        if (GameManager.Instance == null)
        {
            m_TreatButton.SetEnabled(false);
            return;
        }


        bool materialsReady =
            GameManager.Instance.HasMoonleaf &&
            GameManager.Instance.HasMagicalWater;


        m_TreatButton.SetEnabled(
            materialsReady
        );
    }


    // =========================
    // MALZEMELERİ TOPLA
    // =========================

    private void CollectMaterials()
    {
        Debug.Log(
            "MALZEMELERİ TOPLA TIKLANDI!"
        );


        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "GameManager.Instance NULL!"
            );

            return;
        }


        GameManager.Instance.UnlockShops();

        HideTreatment();

        Debug.Log(
            "Tedavi ekranı kapatıldı!"
        );
    }


    // =========================
    // BİTKİYİ TEDAVİ ET
    // =========================

    private void TreatPlant()
    {
        Debug.Log(
            "TEDAVİ ET TIKLANDI!"
        );


        if (GameManager.Instance == null)
        {
            Debug.LogError(
                "GameManager.Instance NULL!"
            );

            return;
        }


        if (!GameManager.Instance.HasMoonleaf ||
            !GameManager.Instance.HasMagicalWater)
        {
            Debug.Log(
                "Gerekli malzemeler eksik!"
            );

            return;
        }


        Debug.Log(
            "Moonflower başarıyla tedavi edildi!"
        );


        // MALZEMELERİ TÜKET

        GameManager.Instance.ConsumeMaterials();


        // 40 GOLD ÖDÜL

        GameManager.Instance.AddGold(40);


        // SAĞLIKLI ÇİÇEĞİ GÖSTER

        if (m_Flower != null &&
            healthyMoonflower != null)
        {
            m_Flower.style.backgroundImage =
                Background.FromSprite(
                    healthyMoonflower
                );
        }


        // TEDAVİ PANELİNİ KAPAT

        HideTreatment();


        // TEŞEKKÜR DİYALOĞUNU AÇ

        ShowFinishDialogue();
    }


    // =========================
    // TEŞEKKÜR DİYALOĞU
    // =========================

    private void ShowFinishDialogue()
    {
        Debug.Log(
        "TEŞEKKÜR DİYALOĞU AÇILIYOR!"
    );


    // TEŞEKKÜR YAZISI

    if (m_DialogueText != null)
    {
        m_DialogueText.text =
            "Çok teşekkür ederim! Moonflower’ım yeniden ışıldıyor. Tedavi ücretiniz: 40 Gold.";
    }


    if (m_NPCDialogue != null)
    {
        m_NPCDialogue.style.display =
            DisplayStyle.Flex;
    }


    if (m_CloseButton != null)
    {
        m_CloseButton.style.display =
            DisplayStyle.None;
    }


    if (m_FinishButton != null)
    {
        m_FinishButton.style.display =
            DisplayStyle.Flex;
    }
    }


    // =========================
    // BİTİR
    // =========================

    private void FinishDialogue()
    {
        Debug.Log(
            "BİTİR TIKLANDI!"
        );


        HideDialogue();


        if (m_FinishButton != null)
        {
            m_FinishButton.style.display =
                DisplayStyle.None;
        }


        if (m_CloseButton != null)
        {
            m_CloseButton.style.display =
                DisplayStyle.Flex;
        }


        // NPC'Yİ GERİ GÖNDER

        CustomerMovement customer =
            FindAnyObjectByType<CustomerMovement>();


        if (customer != null)
        {
            customer.StartLeaving();
        }


        Debug.Log(
            "Müşteri klinikten ayrılıyor."
        );
    }


    // =========================
    // TEMİZLEME
    // =========================

    private void OnDisable()
    {
        if (m_CloseButton != null)
        {
            m_CloseButton.clicked -=
                OpenTreatment;
        }


        if (m_CollectMaterialsButton != null)
        {
            m_CollectMaterialsButton.clicked -=
                CollectMaterials;
        }


        if (m_TreatButton != null)
        {
            m_TreatButton.clicked -=
                TreatPlant;
        }


        if (m_FinishButton != null)
        {
            m_FinishButton.clicked -=
                FinishDialogue;
        }
    }
}