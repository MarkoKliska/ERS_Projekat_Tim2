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

            string testDirektorijum = TestContext.CurrentContext.TestDirectory;
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            testDirektorijum = Path.GetDirectoryName(testDirektorijum);
            string relativnaPutanja = Path.Combine(testDirektorijum, "TestInput", "ostv_2020_05_07.xml");
            //ACT
            using (StringReader sr = new StringReader(relativnaPutanja))
            {
                Console.SetIn(sr);
                uvozop1.UveziOP();
            }
            //ASSERT
            Assert.IsTrue(uvozop1.DozvolaZaUvoz(), "Ocekujemo dozvoljen ulaz");
        }

        [Test]

        public void NeuspesanuvozOP()
        {
            //ARRANGE

            var uvozop1 = new UvozOP();

            //ACT
            using (StringReader sr = new StringReader(@"dsss43434"))
            {
                Console.SetIn(sr);
                var ex = Assert.Catch(() => uvozop1.UveziOP());     //HVATA EXCEPTION

                //ASSERT
                Assert.IsInstanceOf<Exception>(ex);
            }

        }

    }
}
