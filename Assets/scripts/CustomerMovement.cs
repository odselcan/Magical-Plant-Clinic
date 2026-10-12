using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform destination;

    [SerializeField] private Transform returnDestination;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GameObject actionButton;

    private Animator animator;

    private bool hasArrived = false;
    private bool isLeaving = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (actionButton != null)
        {
            actionButton.SetActive(false);

            Debug.Log(
                "Buton başlangıçta kapatıldı: " +
                actionButton.name
            );
        }
    }

    void Update()
    {
        // =========================
        // NPC AYRILIYOR
        // =========================

        if (isLeaving)
        {
            LeaveClinic();
            return;
        }

        // =========================
        // NPC VARIŞ NOKTASINDA
        // =========================

        if (hasArrived)
        {
            if (animator != null)
            {
                animator.SetBool("isWalk", false);
            }

            return;
        }

        if (destination == null)
        {
            return;
        }

        // =========================
        // NPC İLERLİYOR
        // =========================

        if (animator != null)
        {
            animator.SetBool("isWalk", true);
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            destination.position,
            moveSpeed * Time.deltaTime
        );

        // =========================
        // VARIŞ
        // =========================

        if (Vector2.Distance(
                transform.position,
                destination.position) < 0.05f)
        {
            hasArrived = true;

            if (animator != null)
            {
                animator.SetBool("isWalk", false);
            }

            if (actionButton != null)
            {
                actionButton.SetActive(true);
            }

            Debug.Log("Müşteri kliniğe ulaştı.");
        }
    }


    // =========================
    // AYRILMAYI BAŞLAT
    // =========================

    public void StartLeaving()
    {
        Debug.Log("Müşteri klinikten ayrılıyor!");

        hasArrived = false;
        isLeaving = true;

        if (actionButton != null)
        {
            actionButton.SetActive(false);
        }
    }


    // =========================
    // GERİ DÖNÜŞ
    // =========================

    private void LeaveClinic()
    {
        if (returnDestination == null)
        {
            Debug.LogError(
                "CustomerMovement üzerinde Return Destination atanmadı!"
            );

            isLeaving = false;
            return;
        }

        if (animator != null)
        {
            animator.SetBool("isWalk", true);
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            returnDestination.position,
            moveSpeed * Time.deltaTime
        );

        // =========================
        // BAŞLANGIÇ NOKTASINA ULAŞTI
        // =========================

        if (Vector2.Distance(
                transform.position,
                returnDestination.position) < 0.05f)
        {
            if (animator != null)
            {
                animator.SetBool("isWalk", false);
            }

            Debug.Log(
                "Müşteri başlangıç noktasına döndü."
            );

            gameObject.SetActive(false);
        }
    }
}