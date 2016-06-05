using NUnit.Framework;
using Board;

public class MoveTest
{
    [Test]
    public void FirstMoveTest()
    {
        MoveDictionary dic = new MoveDictionary();
        var move = dic.Get(PieceType.King);
        Assert.IsInstanceOf<King>(move);
    }

    [Test]
    public void PawnTurnsToGold()
    {
        MoveDictionary dic = new MoveDictionary();
        var move = dic.GetPromoted(PieceType.Pawn);
        Assert.IsInstanceOf<GoldGeneral>(move);
    }

    [Test]
    public void AtFirstPrevPieceIsNull()
    {
        var c = new MoveController();
        Assert.That(c.PrevPiece, Is.Null);
    }

    [Test]
    public void SecondTouchIsIgnored()
    {
        var world = new World(true);
        var myKing = world.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourKing = world.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.Black);
        world.AddPiece(yourKing);

        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(5));

        var cell = world.TraversableCells[0];
        world.Select(cell, () => { });

        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Black));

        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }
}
