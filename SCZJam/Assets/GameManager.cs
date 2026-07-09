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

    // A lista pra guardar as figurinhas agora

    private List<Figurinha> albumDoJogador = new List<Figurinha>(); // Lista para armazenar as figurinhas coladas no álbum do jogador
    public List<Figurinha> maoDoJogador = new List<Figurinha>(); // Lista para armazenar as figurinhas guardadas na mão do jogador
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

    void MostrarPacote() // Função para mostrar as figurinhas do pacote na tela
{
    comprarPacote.interactable = false; // Desativa o botão de comprar pacote enquanto o pacote está sendo mostrado
    foreach (Transform carta in pacoteContainer)  // Itera sobre todos os filhos do container de pacotes
    {
        Destroy(carta.gameObject); // Destroi cada carta filha do container para limpar a tela antes de mostrar o novo pacote
    }
    
    for (int i = 0; i < pacoteAberto.Count; i++) // Itera sobre cada figurinha no pacote aberto
    {
        GameObject novaCarta = Instantiate(cartaPrefab, pacoteContainer, false); // Instancia uma nova carta a partir 
        // do prefab e a coloca como filha do container de pacotes
        CartaUI cartaUI = novaCarta.GetComponent<CartaUI>(); // Obtém o componente CartaUI da nova carta instanciada
        cartaUI.Inicializar(pacoteAberto[i]); // Inicializa a carta com a figurinha correspondente do pacote aberto
    }
}
public void VerificarSeTodasAsCartasForamDestruidas()
{
    StartCoroutine(VerificarProximoFrame());
}

System.Collections.IEnumerator VerificarProximoFrame()
{
    yield return null;
    if (pacoteContainer.childCount == 0)
    {
        comprarPacote.interactable = true;
    }
}

    public bool JaTemNoAlbum(Figurinha fig) // Função para verificar se o jogador já tem a figurinha no álbum
    {
        return albumDoJogador.Contains(fig); // Retorna true se a figurinha estiver na lista do álbum, false caso contrário
    }
    public void AdicionarNoAlbum(Figurinha fig) // Função pra adicionar a figurinha no album do jogador
    {
        if (!JaTemNoAlbum(fig)) // Verifica se o jogador ainda não tem a figurinha no álbum, o ! quer dizer que é falso o parametro
        // Se o jogador não tiver a figurinha no álbum, adiciona ela
        {
            albumDoJogador.Add(fig); //Adiciona a figurinha na lista do álbum do jogador
            Debug.Log("Adicionado ao album: " + fig.nomeFigurinha); // Loga a ação de adicionar a figurinha ao álbum
        }
        else
        {
            Debug.Log("Figurinha repetida!"); // Loga que a figurinha já está no álbum
        }
    }
    public void AdicionarAoMao(Figurinha fig)
    {
        maoDoJogador.Add(fig); // Adiciona a figurinha na lista da mão do jogador
        Debug.Log("Adicionado à mão: " + fig.nomeFigurinha); // Loga a ação de adicionar a figurinha à mão
    }
    
}