using NUnit.Framework;
using Board;

public class PieceListTest
{
    [Test]
    public void SortedTest()
    {
        var list = new PieceList();
        list.Add(PieceModelUtil.CreatePieceModel(PieceType.Pawn));
        var lance = PieceModelUtil.CreatePieceModel(PieceType.Lance);
        list.Add(lance);

        Assert.That(list[0], Is.EqualTo(lance));
    }
}
