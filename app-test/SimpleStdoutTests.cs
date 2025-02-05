using Lms.Views;

namespace SimpleStdoutTests
{
    public class SimpleStdoutTests
    {
        public struct Foo()
        {
            public required string A {get; init;}
            public required string B {get; init;}
            public required string C {get; init;}
        }

        public struct FooWithLongPropertyNames()
        {
            public required string Abcdefg {get; init; }
            public required string B {get; init; }
        }

        [Fact]
        public void BasicOneItemStringifyTest()
        {
            // Arrange
            var simpleStdout = new SimpleStdout();
            var expected = 
$@"#### Foo ####
A: b
B: c
C: d
#############";
            var obj = new Foo {
                A = "b",
                B = "c",
                C = "d"
            };


            // Act
            var actual = simpleStdout.Stringify(obj);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LongOneItemStringifyTest()
        {
            // Arrange
            var simpleStdout = new SimpleStdout();
            var expected = 
$@"#### FooWithLongPropertyNames ####
Abcdefg: hello
      B: haha
##################################";
            var obj = new FooWithLongPropertyNames {
                Abcdefg = "hello",
                B = "haha"
            };


            // Act
            var actual = simpleStdout.Stringify(obj);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MultipleItemStringifyTest()
        {
            // Arrange
            var simpleStdout = new SimpleStdout();
            var expected = 
$@"| A | B | C |
| - | - | - |
| a | b | c |
| d | e | f |
| h | i | j |";
            var foos = new List<Foo> {
                new Foo {A = "a", B = "b", C = "c"},
                new Foo {A = "d", B = "e", C = "f"},
                new Foo {A = "h", B = "i", C = "j"}
            };

            // Act
            var actual = simpleStdout.Stringify(foos);

            // Assert
            Assert.Equal(expected,actual);

        }

        [Fact]
        public void MultipleLongItemsTest()
        {
            // Arrange
            var simpleStdout = new SimpleStdout();
            var expected = 
$@"| Abcdefg | B |
| ------- | - |
|       a | b |
|       d | e |
|       h | i |";
            var foos = new List<FooWithLongPropertyNames> {
                new FooWithLongPropertyNames {Abcdefg = "a", B = "b"},
                new FooWithLongPropertyNames {Abcdefg = "d", B = "e"},
                new FooWithLongPropertyNames {Abcdefg = "h", B = "i"}
            };

            // Act
            var actual = simpleStdout.Stringify(foos);

            // Assert
            Assert.Equal(expected,actual);

        }


        [Fact]
        public void MultipleLongItemsTestTwo()
        {
            // Arrange
            var simpleStdout = new SimpleStdout();
            var expected = 
$@"| Abcdefg | B               |
| ------- | --------------- |
|       a | badhuwahduwhadu |
|       d |               e |
|       h |               i |";
            var foos = new List<FooWithLongPropertyNames> {
                new FooWithLongPropertyNames {Abcdefg = "a", B = "badhuwahduwhadu"},
                new FooWithLongPropertyNames {Abcdefg = "d", B = "e"},
                new FooWithLongPropertyNames {Abcdefg = "h", B = "i"}
            };

            // Act
            var actual = simpleStdout.Stringify(foos);

            // Assert
            Assert.Equal(expected,actual);

        }
    }
}