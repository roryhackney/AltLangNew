using System;
using Xunit;
using Moq;

// Imports Example File from TestProject
using Program;

namespace AltLang.UnitTests.P
{
    public class ProgramTests
    {
        
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

        [Fact]
        public void getFirstLaunchYear_shouldWork() {
            Dictionary<int, Cell> dict = new Dictionary<int, Cell>();
            dict.Add(1, new Cell(null, null, 2018, null, null, null, null, null, null, null, null, null));
            Assert.Equal(2018, Program.Program.getFirstLaunchYear(dict));
            dict.Add(2, new Cell(null, null, 2011, null, null, null, null, null, null, null, null, null));
            dict.Add(3, new Cell(null, null, 2019, null, null, null, null, null, null, null, null, null));
            dict.Add(4, new Cell(null, null, 2033, null, null, null, null, null, null, null, null, null));
            dict.Add(5, new Cell(null, null, 2000, null, null, null, null, null, null, null, null, null));
            Assert.Equal(2000, Program.Program.getFirstLaunchYear(dict));
        }
        
        [Fact]
        public void fileRead_shouldNotBeEmpty() {
            string path = "C:\\Users\\aphac\\OneDrive\\Documents\\Rory\\ProgrammingLangs_spr24\\altLangWorkingFolder\\AltLangNew\\src\\Cell.cs";
            Assert.True(File.Exists(path), "File should exist at path");
            try {
                string[] content = File.ReadAllLines(path);
                Assert.True(true, "File was read successfully");
                Assert.NotEmpty(content);
            } catch (Exception e) {
                Assert.Fail("File could not be read");
            }
        }

        [Fact]
        public void missingData_shouldBecomeNull() {
            string[] origs = new string[12] {" ", " - ", "\n ", "-", "    ", "  -", " ", " ", " ", "-", " ", "- "};
            string? oem = Program.Program.cleanMissingValue(origs[0]);
            Assert.Null(oem);
            string? model = Program.Program.cleanMissingValue(origs[1]);
            Assert.Null(model);
            int? announceYear = Program.Program.cleanYear(Program.Program.cleanMissingValue(origs[2]));
            Assert.Null(announceYear);
            string? launchStatus = Program.Program.cleanStatus(Program.Program.cleanMissingValue(origs[3]));
            Assert.Null(launchStatus);
            string? bodyDim = Program.Program.cleanMissingValue(origs[4]);
            Assert.Null(bodyDim);
            float? weight = Program.Program.cleanWeight(Program.Program.cleanMissingValue(origs[5]));
            Assert.Null(weight);
            string? sim = Program.Program.cleanYesNo(Program.Program.cleanMissingValue(origs[6]));
            Assert.Null(sim);
            string? displayType = Program.Program.cleanMissingValue(origs[7]);
            Assert.Null(displayType);
            float? inches = Program.Program.cleanSize(Program.Program.cleanMissingValue(origs[8]));
            Assert.Null(inches);
            string? res = Program.Program.cleanMissingValue(origs[9]);
            Assert.Null(res);
            string? features = Program.Program.cleanNums(Program.Program.cleanMissingValue(origs[10]));
            Assert.Null(features);
            string? os = Program.Program.cleanOS(Program.Program.cleanMissingValue(origs[11]));
            Assert.Null(os);
        }
    }
}