
using System;

public interface ICooldownHandler
{
    bool IsCooldownComplete(TimeSpan currentTime);
    bool IsCooldownActive();

    void StartCooldown(TimeSpan startTime);

    void EndCooldown();
    
}