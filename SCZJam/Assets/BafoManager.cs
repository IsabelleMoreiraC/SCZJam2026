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
    //Botão cara ou coroa
    public Button botaoCara;
    public Button botaoCoroa;
    public Button botaoRolarMoeda;
    public TextMeshProUGUI textoResultadoMoeda;

    private string escolhaJogador = ""; // Armazena a escolha do jogador
    private bool jaRolou = false; // Variável para controlar se a moeda já foi rolada


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
        
        botaoCara.onClick.AddListener(() => EscolherLado("Cara")); // Adiciona o listener para o botão de cara
        botaoCoroa.onClick.AddListener(() => EscolherLado("Coroa")); // Adiciona o listener para o botão de coroa
        botaoRolarMoeda.onClick.AddListener(RolarMoeda); // Adiciona o listener para o botão de rolar moeda
        botaoRolarMoeda.gameObject.SetActive(false); //esconde até escolher


    }

    void Update()
    {
        
    }

    void EscolherLado(string lado)
    {
        escolhaJogador = lado; // Armazena a escolha do jogador
        botaoCara.gameObject.SetActive(false); // Faz os botoes aparecerem após a escolha
        botaoCoroa.gameObject.SetActive(false);
        botaoCara.interactable = false; // Desabilita os botões após a escolha
        botaoCoroa.interactable = false;
        botaoRolarMoeda.gameObject.SetActive(true); // Agora ele some
    }

    void RolarMoeda()
    {
        if (jaRolou)return; // Evita que o jogador role a moeda mais de uma vez
        string resultado = Random.Range(0,2) == 0 ? "Cara" : "Coroa"; // Gera um resultado aleatório para a moeda
        jaRolou =true;  // Marca que a moeda já foi rolada

        //mostrar resultado
        textoResultadoMoeda.text = $"Você escolheu: {escolhaJogador}\n";
        textoResultadoMoeda.text += $"Resultado: {resultado}\n";

        if (escolhaJogador == resultado)
        {
            textoResultadoMoeda.text += "Você começa!";

        }
        else
        {
            textoResultadoMoeda.text += "O oponente começa!";
        }
        botaoRolarMoeda.gameObject.SetActive(false); // Esconde o botão de rolar moeda após o resultado
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
    if (gameManager.pacoteContainer.childCount > 0)
    {
        Debug.Log("Ainda há cartas na tela! Termine de escolher primeiro.");
        return;
    }
    
    // Se não tem cartas na mão, não deixa jogar
    if (gameManager.maoDoJogador.Count == 0)
    {
        Debug.Log("Você não tem cartas na mão! Compre mais pacotes.");
        botaoJogarBafo.interactable = false;  // Desabilita o botão também
        return;
    }
    
    // Se chegou aqui, pode jogar
    botaoJogarBafo.interactable = true;
    IniciarBafo();
}
public void ReativarBotaoBafo() // Função para reativar o botão de jogar bafo
{
    botaoJogarBafo.interactable = true; // Reativa o botão de jogar bafo, permitindo que o jogador jogue após abrir um pacote
}
 
}