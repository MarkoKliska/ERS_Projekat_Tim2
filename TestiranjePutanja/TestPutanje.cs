using Projekat_Tim2.Klase;

namespace TestiranjePutanja
{
    public class TestsSkladista
    {
        [Test]
        public void TestSkladistePP()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetSkladistePP();

            bool pass = false;

            if(putanja != null)
                pass = true;

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestSkladisteOP()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetSkladisteOP();

            bool pass = false;

            if (putanja != null)
                pass = true;
            
            Assert.IsTrue(pass);
        }

        [Test]
        public void TestSkladisteRO()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetTabelaRO();

            bool pass = false;

            if(putanja != null)
                pass = true;

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestSkladisteEV()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetSkladisteEv();

            bool pass = false;

            if (putanja != null)
                pass = true;

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestSkladisteF()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetSkladisteFajlova();

            bool pass = false;

            if (putanja != null)
                pass = true;

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestSkladisteAudit()
        {
            var sk = new PutanjeDoSkladista();
            string? putanja = sk.GetAuditTabela();

            bool pass = false;

            if (putanja != null)
                pass = true;

            Assert.IsTrue(pass);
        }
    }
}