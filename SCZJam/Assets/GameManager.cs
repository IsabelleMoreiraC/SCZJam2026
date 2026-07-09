using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public int Moedas;
    public Button comprarPacote;
     public TextMeshProUGUI moedasText;   // referência ao texto de moedas
        public GameObject cartaPrefab; // referência ao prefab da carta
        public Transform pacoteContainer; // referência ao container onde as cartas serão instanciadas
    private Figurinha[] todasAsFigurinhas; // array para armazenar todas as figurinhas disponíveis no jogo
    private List<Figurinha> pacoteAberto; // lista para armazenar as figurinhas do pacote que foi aberto
    void Start()
    {
        comprarPacote.onClick.AddListener(ComprarPacote); // Adiciona o listener para o botão de comprar pacote
        //serve pra quando clicar no botão, chamar a função ComprarPacote
        AtualizarMoedas();  // Chama uma vez pra mostrar moedas iniciais
                CarregarFigurinhas();  //  Carrega as figurinhas uma vez
    }
    
    void ComprarPacote()
    {
        if(Moedas >= 5) // Verifica se o jogador tem moedas suficientes para comprar o pacote
        {
            Moedas -= 5; // reduz 5 moedas do total
            AtualizarMoedas(); // Atualiza o texto de moedas
            GerarPacote(); // Gera o pacote
        }
    }
    
    void AtualizarMoedas() // Atualiza o texto de moedas na interface do usuário
    {
        moedasText.text = "Moedas: " + Moedas;  // Atualiza o texto do TextMeshProUGUI com o valor atual de moedas
    }
    void CarregarFigurinhas() { //
        todasAsFigurinhas = Resources.LoadAll<Figurinha>("Figurinhas"); // Carrega todas as figurinhas do tipo 
        // "Figurinha" da pasta "Resources/Figurinhas"
        Debug.Log("Figurinhas carregadas: " + todasAsFigurinhas.Length); // Loga a quantidade de figurinhas carregadas
        // posso tirar isso dps
          }
    void GerarPacote()
    {
        pacoteAberto = new List<Figurinha>(); // Inicializa a lista de figurinhas do pacote
        for (int i = 0; i<5; i++) // Gera 5 figurinhas aleatórias para o pacote
        {
           float chance = Random.Range(0f,100f); // Gera um número aleatório entre 0 e 100 para determinar a raridade da figurinha
           Figurinha sorteada;
           if (chance<30)//30% rara (IDs 41-52)
            {
                int indiceAleatorio = Random.Range(40,52); // Gera um índice aleatório entre 40 e 51 (IDs das figurinhas raras)
                sorteada = todasAsFigurinhas[indiceAleatorio]; // Seleciona a figurinha rara correspondente ao índice aleatório
            }
            else // 70% comum (IDs 1-40)
            {
                int indiceAleatorio = Random.Range(0,40); // Gera um índice aleatório entre 0 e 39 (IDs das figurinhas comuns)
                sorteada = todasAsFigurinhas[indiceAleatorio]; // Seleciona a figurinha comum correspondente ao índice aleatório
            }
            pacoteAberto.Add(sorteada); // Adiciona a figurinha sorteada à lista do pacote
        }
        Debug.Log("Pacote gerado com " + pacoteAberto.Count + " figurinhas."); // Loga a quantidade de figurinhas no pacote gerado
        MostrarPacote(); // Chama a função para mostrar as figurinhas do pacote na tela
    }

    void MostrarPacote()// Função para mostrar as figurinhas do pacote na tela
    {
        //pra limpar as cartas antigas antes de mostrar as novas
        foreach (Transform carta in pacoteContainer)
        {
            Destroy(carta.gameObject); // Destroi cada carta antiga no container
        }
        //Instancia as cartas do pacote aberto na tela
        for (int i = 0; i <pacoteAberto.Count; i++)
        {
            GameObject novaCarta = Instantiate(cartaPrefab, pacoteContainer, false); // Instancia uma nova carta no container
            //vc conecta a figurinha com a carta
        }
    }
}