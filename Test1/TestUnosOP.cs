using Projekat_Tim2.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeUnosa
{
    internal class TestUnosOP
    {
        [Test]
        public void UspesanUvozOP()
        {
            //ARRANGE 

            var uvozop1 = new UvozOP();

            string? testDirektorijum = TestContext.CurrentContext.TestDirectory;
            string? relativnaPutanja;

            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);

            if (testDirektorijum != null)
                relativnaPutanja = Path.Combine(testDirektorijum, "TestInput", "ostv_2020_05_07.xml");
            else
                relativnaPutanja = null;
            //ACT

            if(relativnaPutanja != null)
            {
                using (StringReader sr = new StringReader(relativnaPutanja))
                {
                    Console.SetIn(sr);
                    uvozop1.UveziXML_OP();
                }
            }
            
            //ASSERT
            Assert.IsTrue(uvozop1.DozvolaZaUvoz(), "Ocekujemo dozvoljen ulaz");
        }
    }
}
