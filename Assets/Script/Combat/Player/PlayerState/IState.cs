public interface IState 
{
    public void EnterState();
    public void ExitState();
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
    public void AnimationTriggerEventBase(PlayerTriggerEventAnim.AnimationTriggerType triggerType);
}
