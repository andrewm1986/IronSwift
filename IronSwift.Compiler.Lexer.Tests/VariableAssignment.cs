using System.IO;
using System.Linq;
using System.Text;
using IronSwift.Compiler.Lexer.Tokens;
using NUnit.Framework;

namespace IronSwift.Compiler.Lexer.Tests
{
    public class VariableAssignment
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void VariableDoubleWithValue()
        {
            // Arrange
            const string testString = "var mutableDouble:Double = 1.0";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(9, tokens.Count);

            Assert.IsInstanceOf<VarKeywordToken>(tokens[0]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<ColonKeywordToken>(tokens[3]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[4]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[5]);
            Assert.IsInstanceOf<SingleEqualsToken>(tokens[6]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[7]);
            Assert.IsInstanceOf<DecimalLiteralToken>(tokens[8]);

            Assert.AreEqual("var", tokens[0].LiteralText);
            Assert.AreEqual(" ", tokens[1].LiteralText);
            Assert.AreEqual("mutableDouble", tokens[2].LiteralText);
            Assert.AreEqual(":", tokens[3].LiteralText);
            Assert.AreEqual("Double", tokens[4].LiteralText);
            Assert.AreEqual(" ", tokens[5].LiteralText);
            Assert.AreEqual("=", tokens[6].LiteralText);
            Assert.AreEqual(" ", tokens[7].LiteralText);
            Assert.AreEqual("1.0", tokens[8].LiteralText);
        }

        [Test]
        public void MutableInferredDoubleWithValue()
        {
            // Arrange
            const string testString = "var mutableInferredDouble = 1.0";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(7, tokens.Count);

            Assert.IsInstanceOf<VarKeywordToken>(tokens[0]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[3]);
            Assert.IsInstanceOf<SingleEqualsToken>(tokens[4]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[5]);
            Assert.IsInstanceOf<DecimalLiteralToken>(tokens[6]);

            Assert.AreEqual("var", tokens[0].LiteralText);
            Assert.AreEqual(" ", tokens[1].LiteralText);
            Assert.AreEqual("mutableInferredDouble", tokens[2].LiteralText);
            Assert.AreEqual(" ", tokens[3].LiteralText);
            Assert.AreEqual("=", tokens[4].LiteralText);
            Assert.AreEqual(" ", tokens[5].LiteralText);
            Assert.AreEqual("1.0", tokens[6].LiteralText);
        }

        [Test]
        public void ConstantDoubleWithValue()
        {
            // Arrange
            const string testString = "let constantDouble:Double = 1.0";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(9, tokens.Count);

            Assert.IsInstanceOf<LetKeywordToken>(tokens[0]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<ColonKeywordToken>(tokens[3]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[4]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[5]);
            Assert.IsInstanceOf<SingleEqualsToken>(tokens[6]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[7]);
            Assert.IsInstanceOf<DecimalLiteralToken>(tokens[8]);

            Assert.AreEqual("let", tokens[0].LiteralText);
            Assert.AreEqual(" ", tokens[1].LiteralText);
            Assert.AreEqual("constantDouble", tokens[2].LiteralText);
            Assert.AreEqual(":", tokens[3].LiteralText);
            Assert.AreEqual("Double", tokens[4].LiteralText);
            Assert.AreEqual(" ", tokens[5].LiteralText);
            Assert.AreEqual("=", tokens[6].LiteralText);
            Assert.AreEqual(" ", tokens[7].LiteralText);
            Assert.AreEqual("1.0", tokens[8].LiteralText);
        }

        [Test]
        public void ReadVariableFromFunctionCall()
        {
            // Arrange
            const string testString = "let inputMessage = readln()";

            // Act
            var lexer = Lexer.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(testString)));
            var tokens = lexer.GetTokens().ToList();

            // Assert
            Assert.AreEqual(9, tokens.Count);

            Assert.IsInstanceOf<LetKeywordToken>(tokens[0]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[1]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[2]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[3]);
            Assert.IsInstanceOf<SingleEqualsToken>(tokens[4]);
            Assert.IsInstanceOf<WhitespaceToken>(tokens[5]);
            Assert.IsInstanceOf<IdentifierToken>(tokens[6]);
            Assert.IsInstanceOf<OpenParenthesisToken>(tokens[7]);
            Assert.IsInstanceOf<CloseParenthesisToken>(tokens[8]);

            Assert.AreEqual("let", tokens[0].LiteralText);
            Assert.AreEqual(" ", tokens[1].LiteralText);
            Assert.AreEqual("inputMessage", tokens[2].LiteralText);
            Assert.AreEqual(" ", tokens[3].LiteralText);
            Assert.AreEqual("=", tokens[4].LiteralText);
            Assert.AreEqual(" ", tokens[5].LiteralText);
            Assert.AreEqual("readln", tokens[6].LiteralText);
            Assert.AreEqual("(", tokens[7].LiteralText);
            Assert.AreEqual(")", tokens[8].LiteralText);
        }
    }
}
