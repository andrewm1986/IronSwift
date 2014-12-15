using System.IO;
using System.Linq;
using System.Text;
using IronSwift.Compiler.Lexer.Tokens;
using NUnit.Framework;

namespace IronSwift.Compiler.Lexer.Tests
{
    public class FunctionCall
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CallWithOneIdentifierArgument()
        {
            // Arrange
            const string testString = "println(x)";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(4, tokens.Count);

            Assert.IsInstanceOf<IdentifierToken>(tokens[0]);
            Assert.IsInstanceOf<OpenParenthesisToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<CloseParenthesisToken>(tokens[3]);

            Assert.AreEqual("println", tokens[0].LiteralText);
            Assert.AreEqual("(", tokens[1].LiteralText);
            Assert.AreEqual("x", tokens[2].LiteralText);
            Assert.AreEqual(")", tokens[3].LiteralText);
        }

        [Test]
        public void CallWithTwoIdentifierArguments()
        {
            // Arrange
            const string testString = "println(x,y)";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(6, tokens.Count);

            Assert.IsInstanceOf<IdentifierToken>(tokens[0]);
            Assert.IsInstanceOf<OpenParenthesisToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<CommaToken>(tokens[3]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[4]);
            Assert.IsInstanceOf<CloseParenthesisToken>(tokens[5]);

            Assert.AreEqual("println", tokens[0].LiteralText);
            Assert.AreEqual("(", tokens[1].LiteralText);
            Assert.AreEqual("x", tokens[2].LiteralText);
            Assert.AreEqual(",", tokens[3].LiteralText);
            Assert.AreEqual("y", tokens[4].LiteralText);
            Assert.AreEqual(")", tokens[5].LiteralText);
        }

        [Test]
        public void CallWithThreeIdentifierArguments()
        {
            // Arrange
            const string testString = "println(x,y,z)";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(8, tokens.Count);

            Assert.IsInstanceOf<IdentifierToken>(tokens[0]);
            Assert.IsInstanceOf<OpenParenthesisToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<CommaToken>(tokens[3]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[4]);
            Assert.IsInstanceOf<CommaToken>(tokens[5]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[6]);
            Assert.IsInstanceOf<CloseParenthesisToken>(tokens[7]);

            Assert.AreEqual("println", tokens[0].LiteralText);
            Assert.AreEqual("(", tokens[1].LiteralText);
            Assert.AreEqual("x", tokens[2].LiteralText);
            Assert.AreEqual(",", tokens[3].LiteralText);
            Assert.AreEqual("y", tokens[4].LiteralText);
            Assert.AreEqual(",", tokens[5].LiteralText);
            Assert.AreEqual("z", tokens[6].LiteralText);
            Assert.AreEqual(")", tokens[7].LiteralText);
        }
    }
}
