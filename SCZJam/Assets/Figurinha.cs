using UnityEngine;
[CreateAssetMenu(fileName = "Figurinha", menuName = "Scriptable Objects/Figurinha")] // Adiciona a opção de criar um asset do tipo
//  "Figurinha" no menu de criação de assets do Unity Editor
public class Figurinha : ScriptableObject // Define a classe "Figurinha" que herda de ScriptableObject, 
// permitindo criar assets do tipo "Figurinha" no Unity Editor
{
    public int id; // Atributo para armazenar o ID da figurinha
    public string nomeFigurinha; // Atributo para armazenar o nome da figurinha
    public enum Time {TimeA, TimeB, TimeC, TimeD}; // Enumeração para os times
    public enum Raridade {Comum, Rara}; // Enumeração para as raridades

    public Time time;              //  campo do tipo Time
    public Raridade raridade;      //  campo do tipo Raridade
    
    public Sprite sprite; 
    public int numeroNoAlbum;
}