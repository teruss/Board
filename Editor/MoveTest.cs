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
        var world = new World();
        var myKing = world.CreatePieceModel(Location.Create(5, 9), PieceType.King, false);
        world.AddPiece(myKing);
        var yourKing = world.CreatePieceModel(Location.Create(5, 1), PieceType.King, true);
        world.AddPiece(yourKing);

        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(5));

        var cell = world.TraversableCells[0];
        world.Select(cell, () => { });

        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));

        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }
}
