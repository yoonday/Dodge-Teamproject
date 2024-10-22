using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem ����
    [SerializeField] private PlayerStatHandler statHandler; // ���� ����
    [SerializeField] GameObject player;
    ItemUIController itemUIController;
    

    public void Awake()
    {
        itemUIController = GetComponent<ItemUIController>();
    }

    
    void OnCollisionEnter2D(Collision2D collision) // �Ѿ�
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy": // ��, �� �Ѿ˿� ���� ��� ü�°���

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
                healthSystem.ChangeHealth(-1);

                if (collision.gameObject.GetComponent<HealthSystem>() == null)
                {
                    collision.gameObject.SetActive(false);
                }


                break;

            case "Item": // �������� ��� ȿ�� ����

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Item);

                ItemStat item = collision.gameObject.GetComponent<ItemStat>();

                if (item != null)
                {

                    if (item.itemType == ItemType.Speed && itemUIController.isHideSkill == false && itemUIController.playerShieldSystem.canActivateShield == false )
                    {


                        itemUIController.playerSkillimage.sprite = collision.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
                        itemUIController.skillTimes = item.ItemDuration;
                        itemUIController.HideSkillSetting();
               
                    }

                    else if (item.itemType == ItemType.Shield && itemUIController.isHideSkill == false && itemUIController.playerShieldSystem.canActivateShield == false)
                    {
                        itemUIController.playerSkillimage.sprite = collision.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
                        itemUIController.skillTimes = item.ItemDuration;

                    }


                    item.ApplyItemEffect(healthSystem, statHandler, player);
                    Destroy(collision.gameObject);
                }
                break;

        }
    }
}
