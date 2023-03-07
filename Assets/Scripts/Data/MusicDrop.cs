using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class MusicDrop : DropData
{
    public Music[] Musics;
    public DroppedMusic DroppedMusicPrefab;

    public override GameObject Create(GameObject at)
    {
        var player = FindObjectOfType<Player>();
        var lockedMusic = Musics;

        var music = Musics.Except(player.UnlockedMusics).OrderBy(x => Random.value).FirstOrDefault();
        
        var inst = Instantiate(DroppedMusicPrefab, at.transform.position + at.transform.forward * 3 + Vector3.up,
            Quaternion.identity);

        inst.Music = music;
        return inst.gameObject;
    }
}