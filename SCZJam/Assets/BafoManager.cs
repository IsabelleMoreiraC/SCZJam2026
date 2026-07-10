using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class BafoManager : MonoBehaviour
{
    public GameObject faseMenuContainer;
    public GameObject sorteioContainer;
    
    public TextMeshProUGUI textoVsTime;
    public TextMeshProUGUI textoMaoOponente;
    public TextMeshProUGUI textoSuaMao;
    public TextMeshProUGUI textoResultadoMoeda;
    
    public Button botaoJogarBafo;
    public Button botaoCara;
    public Button botaoCoroa;
    public Button botaoRolarMoeda;
    public Button botaoVerAlbum;
    public TextMeshProUGUI moedasText;
        public Button buttonVoltar;

    
    private GameManager gameManager;
    private string[] times = { "Time A", "Time B", "Time C", "Time D" };
    private int faseAtual = 0;
    private string escolhaJogador = "";
    private bool jaRolou = false;
    
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        if (botaoJogarBafo != null)
        {
            botaoJogarBafo.onClick.AddListener(VerificarSePodeJogarBafo);
        }
        
        botaoCara.onClick.AddListener(() => EscolherLado("Cara"));
        botaoCoroa.onClick.AddListener(() => EscolherLado("Coroa"));
        botaoRolarMoeda.onClick.AddListener(RolarMoeda);
        botaoRolarMoeda.gameObject.SetActive(false);
        
        botaoVerAlbum.onClick.AddListener(VerAlbum);
    }
    
    void Update()
    {
    }
    
    public void VerificarSePodeJogarBafo()
    {
        if (gameManager.pacoteContainer.childCount > 0)
        {
            Debug.Log("Ainda há cartas na tela! Termine de escolher primeiro.");
            return;
        }
        
        if (gameManager.maoDoJogador.Count == 0)
        {
            Debug.Log("Você não tem cartas na mão! Compre mais pacotes.");
            botaoJogarBafo.interactable = false;
            return;
        }
        
        botaoJogarBafo.interactable = true;
        IniciarBafo();
    }
    
    public void IniciarBafo()
    {
        faseMenuContainer.SetActive(false);
        sorteioContainer.SetActive(true);
        AtualizarInfoBafo();
    }
    
    void AtualizarInfoBafo()
    {
        textoVsTime.text = "Você VS " + times[faseAtual];
        textoMaoOponente.text = "Mão do Oponente: 5";
        textoSuaMao.text = "Sua Mão: " + gameManager.maoDoJogador.Count;
    }
    
    void EscolherLado(string lado)
    {
        escolhaJogador = lado;
        botaoCara.gameObject.SetActive(false);
        botaoCoroa.gameObject.SetActive(false);
        botaoCara.interactable = false;
        botaoCoroa.interactable = false;
        botaoRolarMoeda.gameObject.SetActive(true);
    }
    
    void RolarMoeda()
    {
        if (jaRolou) return;
        
        string resultado = Random.Range(0, 2) == 0 ? "Cara" : "Coroa";
        jaRolou = true;
        
        textoResultadoMoeda.text = $"Você escolheu: {escolhaJogador}\n";
        textoResultadoMoeda.text += $"Resultado: {resultado}\n";
        
        if (escolhaJogador == resultado)
            textoResultadoMoeda.text += "Você começa!";
        else
            textoResultadoMoeda.text += "O oponente começa!";
        
        botaoRolarMoeda.gameObject.SetActive(false);
    }
    
    public void VerAlbum()
    {
        faseMenuContainer.SetActive(false);
        sorteioContainer.SetActive(false);
        moedasText.gameObject.SetActive(false);  
        SceneManager.LoadScene("AlbumScene", LoadSceneMode.Additive);
        StartCoroutine(PassarGameManagerParaAlbum());
    }

    System.Collections.IEnumerator PassarGameManagerParaAlbum()
    {
        yield return null;
        
        AlbumManager albumManager = FindFirstObjectByType<AlbumManager>();
        GameManager gm = FindFirstObjectByType<GameManager>();
        
        if (albumManager != null && gm != null)
        {
            albumManager.SetGameManager(gm);
        }
    }
    
    public void ResetarFase()
    {
        escolhaJogador = "";
        jaRolou = false;
        botaoCara.gameObject.SetActive(true);
        botaoCoroa.gameObject.SetActive(true);
        botaoCara.interactable = true;
        botaoCoroa.interactable = true;
        botaoRolarMoeda.gameObject.SetActive(false);
        textoResultadoMoeda.text = "";
    }

    public void VoltarDoAlbum()
{
    moedasText.gameObject.SetActive(true);  //  Mostra as moedas de novo
    faseMenuContainer.SetActive(true);      //  Mostra o menu
    SceneManager.UnloadSceneAsync("AlbumScene");  //  Descarrega a AlbumScene
}
}