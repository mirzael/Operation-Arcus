public abstract class SecondaryForm : Form {
	protected float timeActive = 4;
	protected float timeActiveOrig = 4;
	protected bool isActive = false;
	
	public abstract void Activate();
	
	public bool isDeactivated() {
		return !isActive;
	}
}
