public class Assassin : Character {

	// Use this for initialization
	override protected void Awake() {
        base.Awake();
    }

    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        SpriteDirection = Directions.Left;
        if ((Anim.GetNextAnimatorStateInfo(0).IsName("Fly") || Anim.GetCurrentAnimatorStateInfo(0).IsName("Fly")) && !Attacking)
        {
            SpriteDirection = Directions.Right;
        }
    }
}
