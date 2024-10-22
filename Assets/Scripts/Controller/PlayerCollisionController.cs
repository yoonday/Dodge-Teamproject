using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem 참조
    [SerializeField] private PlayerStatHandler statHandler; // 스탯 참조
    [SerializeField] GameObject player;
    ItemUIController itemUIController;
    

    public void Awake()
    {
        itemUIController = GetComponent<ItemUIController>();
    }

    
    void OnCollisionEnter2D(Collision2D collision) // 총알
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy": // 적, 적 총알에 맞을 경우 체력감소

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
                healthSystem.ChangeHealth(-1);

                if (collision.gameObject.GetComponent<HealthSystem>() == null)
                {
                    collision.gameObject.SetActive(false);
                }


                break;

            case "Item": // 아이템일 경우 효과 적용

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
