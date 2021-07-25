using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    [SerializeField] private Gem[] gems;

    private Portal portal;

    protected override void Awake()
    {
        base.Awake();

        portal = FindObjectOfType<Portal>();
        portal.GetComponent<Portal>().gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckForGems();
    }

    private void CheckForGems()
    {
        for(int i = 0; i < gems.Length; i++)
        {
            if (gems[i])
            {
                return;
            }
            else if(!gems[i])
            {
                if (i == gems.Length-1)
                {
                    portal.GetComponent<Portal>().gameObject.SetActive(true);
                }
                continue;
            }
        }
    }

}
