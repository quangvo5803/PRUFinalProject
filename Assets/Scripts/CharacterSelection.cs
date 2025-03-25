using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public static string selectedCharacter;

    // G?n button c?a t?ng nh�n v?t trong Inspector
    public Button character1Button;
    public Button character2Button;

    void Start()
    {
        // G?n s? ki?n khi nh?n n�t
        character1Button.onClick.AddListener(SelectCharacter1);
        character2Button.onClick.AddListener(SelectCharacter2);
    }

    void SelectCharacter1()
    {
        selectedCharacter = "Character1"; // L�u l?a ch?n
        StartGame(); // Chuy?n sang scene ch�i game
    }

    void SelectCharacter2()
    {
        selectedCharacter = "Character2"; // L�u l?a ch?n
        StartGame(); // Chuy?n sang scene ch�i game
    }

    void StartGame()
    {
        int selectLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        SceneManager.LoadScene("Level" + selectLevel); // T�n scene ch�i game c?a b?n
    }
}
