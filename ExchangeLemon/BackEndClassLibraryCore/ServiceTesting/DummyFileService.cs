using System;
using System.Threading.Tasks;

namespace BackEndClassLibrary
{
    class DummyFileService : IFileService
    {
        public Task<string> Load(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task Save(string content, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}