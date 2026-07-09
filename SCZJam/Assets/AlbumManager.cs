using UnityEngine;
using UnityEngine.UI;
public class AlbumManager : MonoBehaviour
{
    public GameObject cartaAlbumPrefab; // Prefab da carta que será instanciada
    public Transform contentContainer; // Container onde as cartas serão adicionadas na UI

    private GameManager gameManager; // Referência ao GameManager para acessar as listas de figurinhas
    private Figurinha [] todasAsFigurinhas; // Array para armazenar todas as figurinhas disponíveis

     void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); // Encontra o GameManager na cena
        //Carrega as 52 figurinhas
        todasAsFigurinhas  = Resources.LoadAll<Figurinha>("Figurinhas"); // Carrega todas as figurinhas da pasta "Resources/Figurinhas"

        // Instancia uma carta para cada figurinha e inicializa a UI
        for (int i  = 0; i < todasAsFigurinhas.Length; i++) //Loop para percorrer todas figurinhas
        {
            GameObject novaCarta = Instantiate(cartaAlbumPrefab, contentContainer); // Instancia a carta no container
            Figurinha fig = todasAsFigurinhas[i]; // Obtém a figurinha correspondente 

            //Se tem no albúm vai ficar colorida
            //Se não tem vai ficar acinzentada

            if (gameManager.albumDoJogador.Contains(fig)) //Verifica se a figurinha já está no álbum do jogador
            {
                //Deixa Colorida (normal)
               Image imagemCarta = novaCarta.GetComponentInChildren<Image>(); // Obtém a referência ao componente Image da carta
                    imagemCarta.color = Color.white; // Aplica a cor branca para indicar que a figurinha está no álbum
            }
            else
            {
                //Deixa Acinzentada(grayscale)
                Image imagemCarta = novaCarta.GetComponentInChildren<Image>(); // Obtém a referência ao componente Image da carta
                imagemCarta.color = Color.gray; // Aplica a cor cinza para indicar que a figurinha não está no álbum
            }
            //GetComponent() = pega o componente NO GameObject atual
            //GetComponentInChildren() = pega o componente NO GameObject ou nos filhos dele
            //Usei isso pq a imagem ta dentro do prefab da carta, então é filho do GameObject que instanciei
        }
    }

     void Update()
    {
        
    }
}
