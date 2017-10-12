using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;

    public AudioClip sound;
    public GameObject oneOffAudioPrefab;

    public virtual void Use()
    {
        Debug.Log("Using " + name);

        if (oneOffAudioPrefab != null)
        {
            var source = Instantiate(oneOffAudioPrefab).GetComponent<AudioSource>();
            source.PlayOneShot(sound);
            Destroy(source.gameObject, 10);
        }


    }
}
