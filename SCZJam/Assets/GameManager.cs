using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int Moedas;
    public Button comprarPacote;
     public TextMeshProUGUI moedasText;   // referência ao texto de moedas
    
    void Start()
    {
        comprarPacote.onClick.AddListener(ComprarPacote);
        AtualizarMoedas();  // Chama uma vez pra mostrar moedas iniciais
    }
    
    void ComprarPacote()
    {
        if(Moedas >= 5)
        {
            Moedas -= 5;
            AtualizarMoedas();
        }
    }
    
    void AtualizarMoedas()
    {
        moedasText.text = "Moedas: " + Moedas;  // Atualiza o texto de moedas
    }
}