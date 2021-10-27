using System.Collections.Generic;
using AssaultSrtProvider.Interfaces;
using AssaultSrtProvider.Representation;

namespace AssaultSrtProvider.Providers
{
    public class AssaultProvider : IDataProvider
    {
        public IEnumerable<Snapshot> Snapshots()
        {
            throw new System.NotImplementedException();
        }
    }
}