
using UnityEngine;

public class OriginResource : MonoBehaviour, IHitable
{
    #region Member Variables

    private OriginResourceConfig _config; 

    #endregion
    
    
    
    #region Properties

    public int Amount { get; set; }

    #endregion



    #region Initalize
    

    public void InitAmount(int amount)
    {
        // 추 후 변경 가능성이 있어 보임 [By. 희성]
        if (Amount is < 1 or > 10)
        {
            Debug.LogWarning("체력 구성은 1 ~ 10이어야만 합니다.");
            return;
        }
        
        Amount = amount;
    }

    public void InitConfig(OriginResourceConfig config)
    {
        if (config == null) return;

        _config = config;
        
        // 테스트
        Debug.Log(_config.ResourceType.ToString() +
                  _config.ModelPrefab.name +
                  _config.GetResourceItem.name);
    }

    #endregion
    
    public void Hit(float amount)
    {
        
    }
}
