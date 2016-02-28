using NUnit.Framework;
using Board;

public class WorldTest
{
    [Test]
    public void FirstPlayerIsNotSet()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
    }
}
