
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    #region Member Variables

    private readonly List<ICooldownHandler> _cooldownObjects = new List<ICooldownHandler>();

    #endregion



    #region Register

    public void RegisterCooldown(ICooldownHandler cooldownObject)
    {
        if (!_cooldownObjects.Contains(cooldownObject))
        {
            _cooldownObjects.Add(cooldownObject);
        }
    }

    #endregion



    #region Behavior

    private void Awake()
    {
        // 서비스 로케이터에 등록
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        CoroutineManagement.Instance.StartManagedCoroutine(CooldownRoutine());
    }

    #endregion



    #region Cooldown Coroutine

    private IEnumerator CooldownRoutine()
    {
        while (true)
        {
            foreach (var cooldownObject in _cooldownObjects.Where(cooldownObject => 
                         cooldownObject.IsCooldownActive()).
                         Where(cooldownObject => 
                             cooldownObject.IsCooldownComplete(Managers.Game.RealTime)))
            {
                cooldownObject.EndCooldown();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
