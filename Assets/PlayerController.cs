using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Gerakan
    public float moveSpeed = 5f; // Mungkin perlu disesuaikan lagi nilainya
    public float jumpForce = 7f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveInput;

    // Skor
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreUI();
    }
    
    void Update()
    {
        // Ambil input di Update agar responsif
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        // Simpan input untuk digunakan di FixedUpdate
        moveInput = (transform.right * moveX + transform.forward * moveZ).normalized;

        // --- Loncat (tetap di Update untuk input) ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Lakukan pemindahan posisi di FixedUpdate agar sinkron dengan fisika
    void FixedUpdate()
    {
        // Pindahkan posisi Rigidbody secara langsung
        // Ini tidak menggunakan force, jadi gerakannya instan
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    // Cek Pijakan
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Koleksi Koin
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score++;
            UpdateScoreUI();
        }
    }
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + score;
        }
    }
}