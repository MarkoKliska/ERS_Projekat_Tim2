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

            string? testDirektorijum = TestContext.CurrentContext.TestDirectory;
            string? relativnaPutanja;

            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);

            if (testDirektorijum != null)
                relativnaPutanja = Path.Combine(testDirektorijum, "TestInput", "prog_2020_05_07.xml");
            else
                relativnaPutanja = null;
            //ACT
            if(relativnaPutanja != null)
            {
                using (StringReader sr = new StringReader(relativnaPutanja))
                {
                    Console.SetIn(sr);
                    uvozpp1.UveziXML_PP();
                }
            }
            
            //ASSERT
            Assert.IsTrue(uvozpp1.DozvolaZaUvoz(), "Ocekujemo dozvoljen ulaz");
        }        
    }
}