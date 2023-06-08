public class Activator : IPlantAttack
{
    public Planted toActivate;

    // Update is called once per frame
    void Update()
    {
        toActivate.planted = planted;
    }
}
