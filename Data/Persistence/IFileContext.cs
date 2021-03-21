using System.Collections.Generic;
using LoginExample.Data.Model;

namespace LoginExample.Data.Persistence
{
    public interface IFileContext
    {
        IList<Adult> Adults { get; }
        IList<Family> Families { get; }

        void SaveChanges();
    }
}