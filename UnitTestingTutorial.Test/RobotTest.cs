namespace UnitTestingTutorial.Test
{

    using System;
    using System.Threading;
    using Xunit;

    public class SqlRequiredClass
    {

        /// <summary>
        /// 
        /// </summary>
        public SqlRequiredClass()
        {
            // clone production DB

            // any setup on that DB like connections strings
        }


    }


    public class RobotTest : SqlRequiredClass
    {

        [Fact]
        public static void ThingBeingTested_ConditionOrStateOfTheWorld_OutcomeExpected()
        {

            // Arrange


            // Act


            // Assert

        }


        [Fact]
        public static void Constructor_ParameterExpiresIsPriorToNow_ThrowsArgumentOutOfRangeException()
        {

            // Arrange, Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>( () => new Robot( DateTime.Now.AddSeconds( -5 ) ) );
            Assert.Throws<ArgumentOutOfRangeException>( () => new Robot( DateTime.Today ) );
            Assert.Throws<ArgumentOutOfRangeException>( () => new Robot( DateTime.MinValue ) );

        }

        [Fact]
        public static void Constructor_ParameterExpiresIsInTheFutureOfNow_DoesNotThrowAnyException()
        {

            // Arrange, Act, Assert
            Assert.DoesNotThrow( () => new Robot( DateTime.Now.AddSeconds( 10 ) ) );
            Assert.DoesNotThrow( () => new Robot( DateTime.Today.AddDays( 1 ) ) );
            Assert.DoesNotThrow( () => new Robot( DateTime.MaxValue ) );

        }

        // copy/paste ctor example here
        [Fact]
        public static void HasExpired_RobotHasExpired_ReturnsTrue()
        {

            // Arrange
            var robot1 = new Robot( DateTime.Now.AddSeconds( 5 ) );
            var robot2 = new Robot( DateTime.Now.AddSeconds( 10 ) );
            Thread.Sleep( 12 * 1000 );

            // Act
            bool robot1Expired = robot1.HasExpired();
            bool robot2Expired = robot2.HasExpired();

            // Assert
            Assert.True( robot1Expired );
            Assert.True( robot2Expired );

        }

        [Fact]
        public static void HasExpired_RobotHasNotExpired_ReturnsFalse()
        {

            // Arrange
            var robot1 = new Robot( DateTime.Now.AddSeconds( 30 ) );
            var robot2 = new Robot( DateTime.Today.AddDays( 1 ) );
            var robot3 = new Robot( DateTime.MaxValue );
            Thread.Sleep( 3 * 1000 );

            // Act
            bool robot1Expired = robot1.HasExpired();
            bool robot2Expired = robot2.HasExpired();
            bool robot3Expired = robot3.HasExpired();

            // Assert
            Assert.False( robot1Expired );
            Assert.False( robot2Expired );
            Assert.False( robot3Expired );

        }

        // copy/paste false example

        [Fact]
        public static void ExtendLife_AddsSpecifiedSecondsToExpiresProperty()
        {

            // Arrange
            DateTime expiration = DateTime.Now.AddDays( 1 ); // notice I didn't hardcode a date, I used DateTime.Now which is dynamic.  gives us more test coverage (CI is running all the time)
            var random = new Random();

            var robot1 = new Robot( expiration );
            const int extension1 = 0;

            var robot2 = new Robot( expiration );
            int extension2 = random.Next( 0 , 10000000 ); // notice I didn't hardcode a value here.  Used random numbers

            var robot3 = new Robot( expiration );
            const int extension3 = int.MaxValue; // try the boundaries!!  doesn't prove correctness but you'll fish out 90% of the issues.

            // Act
            robot1.ExtendLife( extension1 );
            robot2.ExtendLife( extension2 );
            robot3.ExtendLife( extension3 );

            // Assert
            Assert.Equal( expiration , robot1.Expires ); // always test what you expect against what actually occured, not vice-versa (as convention)
            Assert.Equal( expiration.AddSeconds( extension2 ) , robot2.Expires );
            Assert.Equal( expiration.AddSeconds( extension3 ) , robot3.Expires ); // show what happens when expectation is not met

        }


    }
}
