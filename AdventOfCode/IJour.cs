using System.IO;

namespace AdventOfCode
{ 
    public interface IJour
    {
        void Init(string inputfile);

        void ResolveFirstPart();

        void ResolveSecondPart();
       
    }
}
