using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BafoManager : MonoBehaviour
{
    //Referencia à UI
    public GameObject faseMenuContainer; //Os 3 botoes
    public GameObject sorteioContainer; //A tela de sorteio

    //Referencia os textos
    public TextMeshProUGUI textoVsTime; //Texto da fase    
    public TextMeshProUGUI textoMaoOponente; //Texto da fase
    public TextMeshProUGUI textoSuaMao; //Texto da fase

    //referencia o gameManager para pegar as listas
    private GameManager gameManager;

    private string[] times = { "Time A", "Time B", "Time C", "Time D" }; //Array com os nomes dos times
    private int faseAtual = 0; //0 = TimeA, 1 = TimeB, 2 = TimeC, 3 = TimeD 
    //
    public Button botaoJogarBafo;
    void Start()
    {
       gameManager = FindFirstObjectByType<GameManager>(); //Encontra o GameManager na cena
        botaoJogarBafo.onClick.AddListener(VerificarSePodeJogarBafo);  // Usa a variável public, sem declarar de novo 
        //primeiro ve se pode jogar
        Debug.Log("Botão conectado!");// Adiciona o listener para o botão de jogar bafo
        
    }

    void Update()
    {
        
    }

    public void IniciarBafo()
    {
        //Esconder o Menu, começa o sorteio
        faseMenuContainer.SetActive(false);
        sorteioContainer.SetActive(true);
        //atualizar os textos
        AtualizarInfoBafo();

    }
    void AtualizarInfoBafo()
    {
        //Mostra "Você VS TimeA" (ou B, C, D conforme faseAtual)
        textoVsTime.text = "Você VS " + times[faseAtual];
        //Mostrar quantas cartas cada um tem
        textoMaoOponente.text = "Mão do Oponente: 5"; // fixo por enquanto
        textoSuaMao.text = "Sua Mão: " +gameManager.maoDoJogador.Count; // pega a quantidade de cartas na mão do jogador

    }

    public void VerificarSePodeJogarBafo()
    {
        // Se ainda há cartas na tela, não deixa jogar bafo
        if (gameManager.pacoteContainer.childCount > 0 )
        {
            Debug.Log("Ainda há cartas na tela! Não pode jogar bafo.");
            return; // Sai da função sem fazer nada. Por enquanto deixa só o debug
        }
        //se chegou aqui, pode jogar
        IniciarBafo();
    }
}
