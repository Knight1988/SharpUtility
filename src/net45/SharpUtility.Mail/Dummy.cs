using System;

namespace SharpUtility.Mail
{
    internal class Dummy
    {
        // DO NOT DELETE THIS CODE UNLESS WE NO LONGER REQUIRE ASSEMBLY <Telnet>!!!
        internal void DummyFunctionToMakeSureReferencesGetCopiedProperly_DO_NOT_DELETE_THIS_CODE()
        {
            // Assembly <Telnet> is used by this file, and that assembly depends on assembly <LiteGuard>,
            // but this project does not have any code that explicitly references assembly <LiteGuard>. Therefore, when another project references
            // this project, this project's assembly and the assembly <Telnet> get copied to the project's bin directory, but not
            // assembly <LiteGuard>. So in order to get the required assembly <LiteGuard> copied over, we add some dummy code here (that never
            // gets called) that references assembly <LiteGuard>; this will flag VS/MSBuild to copy the required assembly <LiteGuard> over as well.
            var dummyType = typeof(LiteGuard.Guard);
            Console.WriteLine(dummyType.FullName);
        }
    }
}
