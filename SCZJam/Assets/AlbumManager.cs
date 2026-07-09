using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlbumManager : MonoBehaviour
{
    public GameObject cartaAlbumPrefab;
    public Transform contentContainer;

    private GameManager gameManager;
    private Figurinha[] todasAsFigurinhas;

    void Start()
    {
    }

    public void SetGameManager(GameManager gm)
    {
        gameManager = gm;
        CarregarAlbum();
    }

    void CarregarAlbum()
    {
        StartCoroutine(CarregarAlbumCoroutine());
    }

    System.Collections.IEnumerator CarregarAlbumCoroutine()
    {
        yield return null;

        if (gameManager == null)
        {
            Debug.Log("GameManager não encontrado!");
            yield break;
        }

        todasAsFigurinhas = Resources.LoadAll<Figurinha>("Figurinhas");
        Debug.Log("Figurinhas carregadas: " + todasAsFigurinhas.Length);

        for (int i = 0; i < todasAsFigurinhas.Length; i++)
        {
            GameObject novaCarta = Instantiate(cartaAlbumPrefab, contentContainer);
            Debug.Log("Carta " + i + " instanciada");
            
            Figurinha fig = todasAsFigurinhas[i];
            
            Image imagemCarta = novaCarta.GetComponentInChildren<Image>();
            
            if (imagemCarta == null)
            {
                Debug.Log("Erro: Image não encontrada na carta " + i);
                continue;
            }
            
            if (gameManager.albumDoJogador.Contains(fig))
            {
                imagemCarta.color = Color.white;
            }
            else
            {
                imagemCarta.color = Color.gray;
            }
        }
        
        LayoutRebuilder.MarkLayoutForRebuild(contentContainer as RectTransform);
        yield return null;
yield return null;  // Espera 2 frames

int filhos = contentContainer.childCount;
Debug.Log("Cartas no container: " + filhos);
    }
}