using UnityEngine;

[CreateAssetMenu(fileName = "FigurinhasAlbum", menuName = "Scriptable Objects/FigurinhasAlbum")]
public class FigurinhasAlbum : ScriptableObject
{
    public Figurinha[] figurinhas = new Figurinha[52];
}