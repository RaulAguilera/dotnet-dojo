using NetArchTest.Rules;

namespace Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string ApiNamespace = "API";

        [Fact]
        public void Domain_Should_Not_Depend_On_Other_Projects()
        {
            //arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApplicationNamespace, InfrastructureNamespace, ApiNamespace
            };

            //act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();
            //assert
            Assert.True(testResult.IsSuccessful);

        }


        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructre_Or_API()
        {
            //arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamespace, ApiNamespace
            };

            //act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();
            //assert
            Assert.True(testResult.IsSuccessful);

        }
    }
}