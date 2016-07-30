using NUnit.Framework;
using Board;

public class KomadaiUtilTest
{
    [Test]
    public void FirstPieceTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 1), Is.EqualTo(KomadaiUtil.Calc(4, 9)));
    }

    [Test]
    public void TwoPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 2), Is.EqualTo((KomadaiUtil.Calc(3, 9) + KomadaiUtil.Calc(4, 9)) / 2));
    }

    [Test]
    public void ThreePiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 3), Is.EqualTo(KomadaiUtil.Calc(3, 9)));
    }
}
