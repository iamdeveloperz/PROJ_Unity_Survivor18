
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class OriginResource : MonoBehaviour, IHitable, ICooldownHandler
{
    #region Member Variables

    private OriginResourceConfig _config;

    private TimeSpan _respawnCooldown;
    private TimeSpan _cooldownStartTime;

    private int _initAmountValue;
    private Transform _initTransform;

    private int _minGetItem;
    private int _maxGetItem;

    private bool _isCooldownActive;

    #endregion
    
    
    
    #region Properties

    private int Amount { get; set; }

    #endregion



    #region Behavior

    private void Start()
    {
        ServiceLocator.GetService<CooldownSystem>().RegisterCooldown(this);
    }

    #endregion



    #region Initalize

    public void Initialize(int amount, int minValue, int maxValue, TimeSpan respawnCooldown, OriginResourceConfig config)
    {
        InitAmount(amount);
        InitMinMaxValue(minValue, maxValue);
        InitCooldown(respawnCooldown);
        InitConfig(config);

        _initTransform = transform;
    }

    private void InitCooldown(TimeSpan cooldown)
    {
        if (cooldown.TotalSeconds is < 1 or > Literals.DAY_PER_SECONDS)
        {
            Debug.LogWarning($"[쿨타임] 초 구성은 1 ~ 86400이어야만 합니다. : {cooldown.TotalSeconds}");
            return;
        }

        _respawnCooldown = cooldown;
    }

    private void InitAmount(int amount)
    {
        
        // 추 후 변경 가능성이 있어 보임 [By. 희성]
        if (amount is < 1 or > 10)
        {
            Debug.LogWarning($"체력 구성은 1 ~ 10이어야만 합니다 : {amount}");
            return;
        }
        
        Amount = amount;
        _initAmountValue = Amount;
    }

    private void InitMinMaxValue(int minValue, int maxValue)
    {
        if((minValue is < 1 or > 20) && (maxValue is < 2 or > 99))
        {
            Debug.LogWarning($"Min Max 구성은 1 ~ 99이어야만 합니다 : {minValue}, {maxValue}");
            return;
        }

        _minGetItem = minValue;
        _maxGetItem = maxValue;
    }

    private void InitConfig(OriginResourceConfig config)
    {
        if (config == null) return;

        _config = config;
    }

    #endregion



    #region Detroy

    private void DestroyAction()
    {
        CoroutineManagement.Instance.StartManagedCoroutine(DestroyGetItemActionCoroutine());
        
        // 쿨다운 초기 지점 셋업
        StartCooldown(Managers.Game.RealTime);
        
        transform.gameObject.SetActive(false);
        _isCooldownActive = true;
    }

    private void GetItem()
    {
        var randomGetNumber = Random.Range(_minGetItem, _maxGetItem + 1);

        for (var i = 0; i < randomGetNumber; ++i)
        {
            var randomOffset = Random.insideUnitSphere * 0.5f;
            randomOffset.y = Mathf.Max(randomOffset.y, 0.5f); // Y값이 최소 0.5 이상이 되도록 설정

            var spawnPosition = transform.position + randomOffset;
            var spawnRotation = Random.rotation;
            
            var dropItem = Instantiate(_config.GetResourceItem.dropPrefab, spawnPosition, spawnRotation);
            CoroutineManagement.Instance.StartManagedCoroutine(DropItemStopMovementSeconds(dropItem, 5f));
        }
    }

    private IEnumerator DropItemStopMovementSeconds(GameObject item, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (item == null) yield break;

        var rb = item.GetComponent<Rigidbody>();
        
        if (rb == null) yield break;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private IEnumerator DestroyGetItemActionCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        
        GetItem();
    }
    
    #endregion



    #region Hitable

    public void Hit(float amount)
    {
        Amount -= 1;
        Managers.Sound.PlayEffectSound(transform, this.name);
        Debug.LogWarning(Amount);
        if (Amount == 0)
        {
            DestroyAction();
        }
    }

    #endregion


    
    #region Cooldown Handler

    public bool IsCooldownComplete(TimeSpan currentTime)
    {
        return currentTime - _cooldownStartTime > _respawnCooldown;
    }

    public bool IsCooldownActive()
    {
        return _isCooldownActive;
    }

    public void StartCooldown(TimeSpan startTime)
    {
        _cooldownStartTime = startTime;
    }

    // 자원 재생성을 해야함
    public void EndCooldown()
    {
        Amount = _initAmountValue;
        transform.SetLocalPositionAndRotation(_initTransform.position, _initTransform.rotation);
        
        // 비활성화 되어 있던 객체를 다시 활성화 처리
        transform.gameObject.SetActive(true);
        _isCooldownActive = false;
    }

    #endregion
    
}
