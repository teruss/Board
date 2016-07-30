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
        Assert.That(KomadaiUtil.Calc(0, 3), Is.EqualTo((KomadaiUtil.Calc(5, 16) + KomadaiUtil.Calc(6, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(1, 3), Is.EqualTo(KomadaiUtil.Calc(9, 16)));
    }

    [Test]
    public void FourPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 4), Is.EqualTo(KomadaiUtil.Calc(5, 16)));
        Assert.That(KomadaiUtil.Calc(2, 4), Is.EqualTo(KomadaiUtil.Calc(9, 16)));
    }

    [Test]
    public void FivePiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 5), Is.EqualTo(KomadaiUtil.Calc(5, 16)));
        Assert.That(KomadaiUtil.Calc(2, 5), Is.EqualTo((KomadaiUtil.Calc(8, 16) + KomadaiUtil.Calc(9, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(4, 5), Is.EqualTo((KomadaiUtil.Calc(10, 16) + KomadaiUtil.Calc(11, 16)) / 2));
    }

    [Test]
    public void SixPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 6), Is.EqualTo((KomadaiUtil.Calc(4, 16) + KomadaiUtil.Calc(5, 16)) / 2));
    }
}
