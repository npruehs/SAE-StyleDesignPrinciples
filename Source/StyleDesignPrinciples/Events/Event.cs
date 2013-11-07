﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Events
{
    using System;

    /// <summary>
    ///   Event that may contain additional data listeners might be interested in.
    /// </summary>
    public class Event
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Constructs a new event of the specified type.
        /// </summary>
        /// <param name="eventType">
        ///   Type of the new event.
        /// </param>
        public Event(object eventType)
            : this(eventType, null)
        {
        }

        /// <summary>
        ///   Constructs a new event of the specified type and holding the
        ///   passed event data.
        /// </summary>
        /// <param name="eventType">
        ///   Type of the new event.
        /// </param>
        /// <param name="eventData">
        ///   Data any listener might be interested in.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   Specified event type is null.
        /// </exception>
        public Event(object eventType, object eventData)
        {
            if (eventType == null)
            {
                throw new ArgumentNullException("eventType");
            }

            this.EventType = eventType;
            this.EventData = eventData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Data any listener might be interested in.
        /// </summary>
        public object EventData { get; private set; }

        /// <summary>
        ///   Type of this event.
        /// </summary>
        public object EventType { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Returns the type and data of this event, as string.
        /// </summary>
        /// <returns>Type and data of this event, as string.</returns>
        public override string ToString()
        {
            return string.Format("Event: {0} - Event data: {1}", this.EventType, this.EventData);
        }

        #endregion
    }
}