using UnityEngine;

public class DelayTimer
{
    private float currentTime = 0f;
    private bool running = false;

    // 딜레이 타이머 실행: 일정 시간 지난 뒤 true 반환 (한 번만 true)
    public bool Run(float setTime)
    {
        if (!running)
        {
            currentTime = 0f;
            running = true;
        }

        currentTime += Time.deltaTime;

        if (currentTime >= setTime)
        {
            running = false;
            return true;
        }

        return false;
    }

    // 강제로 리셋
    public void Reset()
    {
        currentTime = 0f;
        running = false;
    }
}
