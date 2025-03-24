    using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public static string selectedCharacter;

    // G?n button c?a t?ng nhân v?t trong Inspector
    public Button character1Button;
    public Button character2Button;

    void Start()
    {
        // G?n s? ki?n khi nh?n nút
        character1Button.onClick.AddListener(SelectCharacter1);
        character2Button.onClick.AddListener(SelectCharacter2);
    }

    void SelectCharacter1()
    {
        selectedCharacter = "Character1"; // Lýu l?a ch?n
        StartGame(); // Chuy?n sang scene chõi game
    }

    void SelectCharacter2()
    {
        selectedCharacter = "Character2"; // Lýu l?a ch?n
        StartGame(); // Chuy?n sang scene chõi game
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level1"); // Tên scene chõi game c?a b?n
    }
}
