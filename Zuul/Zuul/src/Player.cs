class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    // fields
    private int health;
    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
    }
    
    
    // methods
    public int Damage(int amount)
    {
        health = health - amount;
        return health;
    }

    public int Heal(int amount)
    {
        health = health + amount;
        return health;
    }
    
    public bool IsAlive()
    {
        bool death = false;
        if (health <= 0)
        {
            death = true;
        }
        return death
    }
}