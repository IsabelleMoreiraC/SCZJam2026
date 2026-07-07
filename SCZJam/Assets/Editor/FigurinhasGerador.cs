using UnityEngine;
using UnityEditor;
using System.IO;

public class FigurinhasGerador
{
    [MenuItem("Assets/Gerar Figurinhas")] // Adiciona um item de menu no Unity Editor
    public static void GerarFigurinhas()  //  Método que envolve tudo
    {
        string caminhoPasta = "Assets/Figurinhas"; // Caminho onde os assets serão salvos
        
        if (!Directory.Exists(caminhoPasta)) // Verifica se a pasta já existe, caso não exista, cria a pasta
        {
            Directory.CreateDirectory(caminhoPasta); // Cria a pasta "Figurinhas" dentro de "Assets" se não funcionar tá com nome 
            //errado
        }

        for (int i = 1; i <= 52; i++) // Loop para criar as 52 figurinhas
        {
            Figurinha novaFigurinha = ScriptableObject.CreateInstance<Figurinha>(); // Cria uma nova instância do ScriptableObject
            //  "Figurinha"
            novaFigurinha.id = i; // Atribui o ID da figurinha (de 1 a 52)
            novaFigurinha.nomeFigurinha = $"Figurinha {i:D2}"; // Atribui o nome da figurinha (ex: "Figurinha 01", "Figurinha 02", ...)
            novaFigurinha.time = (Figurinha.Time)(i % 4);   // Atribui o time da figurinha
            novaFigurinha.raridade = i <= 40 ? Figurinha.Raridade.Comum : Figurinha.Raridade.Rara; // Atribui a raridade da figurinha
            novaFigurinha.numeroNoAlbum = i; // Atribui o número da figurinha no álbum
            string caminhoAsset = $"{caminhoPasta}/Figurinha_{i:D2}.asset"; // Define o caminho completo do asset a ser criado 
            // (ex: "Assets/Figurinhas/Figurinha_01.asset")
            AssetDatabase.CreateAsset(novaFigurinha, caminhoAsset); // Cria o asset no caminho especificado
        }

        AssetDatabase.SaveAssets(); // Salva todas as alterações feitas nos assets
        AssetDatabase.Refresh(); // Atualiza o AssetDatabase para refletir as mudanças no Unity Editor
        
        Debug.Log("Figurinhas geradas com sucesso!");   
    }
}