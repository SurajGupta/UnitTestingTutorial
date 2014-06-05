namespace UnitTestingTutorial
{
    using System;

    public class Robot
    {

        #region Fields (Private)

        private readonly object expirationLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Robot class.
        /// </summary>
        /// <param name="expires">Day/time when robot expires</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when expires is prior to Now</exception>
        public Robot( DateTime expires )
        {
            if ( expires < DateTime.Now )
            {
                throw new ArgumentOutOfRangeException( "expires" , "Robot cannot expire before now." );
            }
            Expires = expires;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the day/time when the robot expires
        /// </summary>
        public DateTime Expires { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines if the robot has expired.
        /// </summary>
        /// <returns>true if the robot has expired, false if not</returns>
        public bool HasExpired()
        {
            lock ( this.expirationLock )
            {
                return Expires <= DateTime.Now;
            }
        }

        /// <summary>
        /// Extends the robot's life.
        /// </summary>
        /// <param name="seconds">The number of seconds by which to extend the robot's life.</param>
        /// <exception cref="ArgumentException">Thrown when seconds &lt; 0</exception>
        public void ExtendLife( int seconds )
        {
            if ( seconds < 0 )
            {
                throw new ArgumentException( "seconds < 0" );
            }
            lock ( this.expirationLock )
            {
                Expires = Expires.AddSeconds( seconds );
            }
        }

        #endregion
        
    }
}
