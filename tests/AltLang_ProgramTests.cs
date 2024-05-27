using System;
using Xunit;
using Moq;

// Imports Example File from TestProject
using Program;

namespace AltLang.UnitTests.P
{
    public class ProgramTests
    {
        private readonly Program.Program program;
        
        public ProgramTests()
        {
            program = new Program.Program();
        }
        
        // First Test for NewMethod
        [Fact]
        public void cleanMissingValue_shouldWork()
        {
            string s = "  -";
            string? result = Program.Program.cleanMissingValue(s);
            Assert.True(result == null, "Given dash should return null.");

            s = "\t    \n  ";
            result = Program.Program.cleanMissingValue(s);
            Assert.True(result == null, "Given whitespace should return null");

            s = "  stuff   ";
            result = Program.Program.cleanMissingValue(s);
            Assert.True(result == "stuff", "Given text should return trimmed");

        }
        
        // Second Test for NewMethod
    //     [Fact]
    //     public void NewMethod_ShouldReturnFalse()
    //     {
    //         bool result = _newclass.NewMethod(false);

    //         Assert.False(result, "Given false should return false.");
    //     }
    }
}