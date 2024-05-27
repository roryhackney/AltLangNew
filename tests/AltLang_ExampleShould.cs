// using System;
// using Xunit;
// using Moq;

// // Imports Example File from TestProject
// using Program;

// namespace AltLang.UnitTests.Example
// {
//     public class Example_NewClassShould
//     {
//         private readonly NewClass _newclass;
        
//         public Example_NewClassShould()
//         {
//             _newclass = new NewClass();
//         }
        
//         // First Test for NewMethod
//         [Fact]
//         public void NewMethod_ShouldReturnTrue()
//         {
//             bool result = _newclass.NewMethod(true);

//             Assert.True(result, "Given true should return true.");
//         }
        
//         // Second Test for NewMethod
//         [Fact]
//         public void NewMethod_ShouldReturnFalse()
//         {
//             bool result = _newclass.NewMethod(false);

//             Assert.False(result, "Given false should return false.");
//         }
//     }
// }