using UnityEngine;
using UnityEngine.UI;
public class CartaUI : MonoBehaviour
{
    private Figurinha figurinha; // Referência à figurinha associada a esta carta
    private Button botaoColar; // Referência ao botão de colar a figurinha
    private Button botaoGuardar; // Referência ao botão de guardar a figurinha
    private GameManager gameManager; // Referência ao GameManager para acessar as listas de figurinhas
   public void Inicializar(Figurinha fig)
    {
        figurinha = fig; // Inicializa a figurinha associada a esta carta
        gameManager = FindFirstObjectByType<GameManager>(); // Encontra o GameManager na cena
        // Aqui dá pra atualizar a UI da carta com as informações da figurinha, como imagem, nome, etc.
    Button[] botoes = GetComponentsInChildren<Button>(); // Obtém todos os botões filhos da carta
        botaoColar = botoes[0]; // Assume que o primeiro botão é o de colar
        botaoGuardar = botoes[1]; // Assume que o segundo botão é o de guardar
        botaoColar.onClick.AddListener(ColarNoAlbum); // Adiciona o listener para o botão de colar
        botaoGuardar.onClick.AddListener(GuardarNaMao); // Adiciona o listener para o botão de guardar
    }
    void ColarNoAlbum()
    {
        gameManager.AdicionarNoAlbum(figurinha); // Chama o método do GameManager para adicionar a figurinha ao álbum
        Destroy(gameObject); // Destroi a carta da UI após colar no álbum
        Debug.Log("Colando "+ figurinha.nomeFigurinha);
        gameManager.VerificarSeTodasAsCartasForamDestruidas(); // Verifica se todas as cartas foram destruídas para reativar o botão de comprar pacote
    }
    void GuardarNaMao()
    {
        gameManager.AdicionarAoMao(figurinha); // Chama o método do GameManager para adicionar a figurinha à mão
        Destroy(gameObject); // Destroi a carta da UI após guardar na mão
        Debug.Log("Guardando: " + figurinha.nomeFigurinha);
        gameManager.VerificarSeTodasAsCartasForamDestruidas(); // Verifica se todas as cartas foram destruídas para reativar o botão de comprar pacote

    }
}
