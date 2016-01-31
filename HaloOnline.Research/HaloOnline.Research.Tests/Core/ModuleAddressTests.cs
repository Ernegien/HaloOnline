using HaloOnline.Research.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaloOnline.Research.Tests.Core
{
    [TestClass] 
    public class ModuleAddressTests
    {
        readonly ModuleAddressContext _context = new ModuleAddressContext(0x400000, 0x700000, 0x200000);

        [TestMethod]
        public void CreationTest()
        {
            ModuleAddress addr = new ModuleAddress(_context, _context.BaseAddress);
            Assert.AreEqual(addr.Context, _context);
            Assert.AreEqual(addr.Value, _context.BaseAddress);

            new ModuleAddress(_context, _context.BaseAddress + _context.Size);

            try
            {
                new ModuleAddress(_context, _context.BaseAddress - 1);
                Assert.Fail();
            }
            catch { /* before valid range */ }

            try
            {
                new ModuleAddress(_context, _context.BaseAddress + _context.Size + 1);
                Assert.Fail();
            }
            catch { /* past valid range */}

            try
            {
                new ModuleAddressContext(0x400000, 0x700000, 0);
                Assert.Fail();
            }
            catch { /* zero size */ }

            try
            {
                new ModuleAddressContext(uint.MaxValue - 1, 0, 10);
                Assert.Fail();
            }
            catch { /* image address overflow */ }

            try
            {
                new ModuleAddressContext(0, uint.MaxValue - 1, 10);
                Assert.Fail();
            }
            catch { /* module address overflow */ }
        }

        [TestMethod]
        public void ConversionTest()
        {
            Assert.AreEqual(ModuleAddress.FromImageAddress(_context, _context.ImageBaseAddress), _context.BaseAddress);
            Assert.AreEqual(ModuleAddress.ToImageAddress(_context, _context.BaseAddress), _context.ImageBaseAddress);
            ModuleAddress addr = new ModuleAddress(_context, _context.BaseAddress);
            Assert.AreEqual(addr.ToImageAddress(), _context.ImageBaseAddress);
        }

        [TestMethod]
        public void CastTest()
        {
            ModuleAddress addr = new ModuleAddress(_context, _context.BaseAddress);
            Assert.AreEqual(addr.Value, addr);
            Assert.AreEqual(addr.Value, _context.BaseAddress);
        }
    }
}
