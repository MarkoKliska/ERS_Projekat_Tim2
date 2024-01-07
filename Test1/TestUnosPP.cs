using Projekat_Tim2.Klase;

namespace TestiranjeUnosa
{
    public class TestUnosPP
    {

        [Test]
        public void UspesanUvozPP()
        {
            //ARRANGE 
            
            var uvozpp1 = new UvozPP();

            string testDirektorijum = TestContext.CurrentContext.TestDirectory;
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);

            string relativnaPutanja = Path.Combine(testDirektorijum, "TestInput", "prog_2020_05_07.xml");

            //ACT
            using (StringReader sr = new StringReader(relativnaPutanja))
            {
                Console.SetIn(sr);
                uvozpp1.UveziXML_PP();
            }
            //ASSERT
            Assert.IsTrue(uvozpp1.DozvolaZaUvoz(), "Ocekujemo dozvoljen ulaz");
        }

        [Test]

        public void NeuspesanuvozPP()
        {
            //ARRANGE

            var uvozpp1 = new UvozPP();

            //ACT
            using (StringReader sr = new StringReader(@"dsss43434"))
            {
                Console.SetIn(sr);
                var ex = Assert.Catch(() => uvozpp1.UveziXML_PP());     //HVATA EXCEPTION

                //ASSERT
                Assert.IsInstanceOf<Exception>(ex);
            }
            
        }
        
    }
}