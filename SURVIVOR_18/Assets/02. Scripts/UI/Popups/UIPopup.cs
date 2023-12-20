
public class UIPopup : UIBase
{
    public override void Initialize()
    {
        Managers.UI.SetCanvas(gameObject);
    }
    
    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
