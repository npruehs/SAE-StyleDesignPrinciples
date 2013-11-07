// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventManager.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace StyleDesignPrinciples.Events
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   Allows listeners to register for events and notifies them
    ///   whenever one of these events is fired.
    /// </summary>
    public class EventManager
    {
        #region Fields

        /// <summary>
        ///   Queue of events that are currently being processed.
        /// </summary>
        private readonly List<Event> currentEvents;

        /// <summary>
        ///   Listeners that are interested in events of specific types.
        /// </summary>
        private readonly Dictionary<object, EventDelegate> listeners;

        /// <summary>
        ///   Queue of events to be processed.
        /// </summary>
        private readonly List<Event> newEvents;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructs a new event manager with an empty event queue and
        ///   without any listeners.
        /// </summary>
        public EventManager()
        {
            this.newEvents = new List<Event>();
            this.currentEvents = new List<Event>();
            this.listeners = new Dictionary<object, EventDelegate>();
        }

        #endregion

        #region Delegates

        /// <summary>
        ///   Signature of the event callbacks.
        /// </summary>
        /// <param name="e">Event which occurred.</param>
        public delegate void EventDelegate(Event e);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Passes all queued events on to interested listeners and clears the
        ///   event queue.
        /// </summary>
        public void ProcessEvents()
        {
            // Process queues events.
            while (this.newEvents.Count > 0)
            {
                this.currentEvents.AddRange(this.newEvents);
                this.newEvents.Clear();

                foreach (var e in this.currentEvents)
                {
                    this.ProcessEvent(e);
                }

                this.currentEvents.Clear();
            }
        }

        /// <summary>
        ///   Queues a new event of the specified type along with the passed
        ///   event data.
        /// </summary>
        /// <param name="eventType">Type of the event to queue.</param>
        /// <param name="eventData">Data any listeners might be interested in.</param>
        public void QueueEvent(object eventType, object eventData)
        {
            var newEvent = new Event(eventType, eventData);
            this.newEvents.Add(newEvent);
        }

        /// <summary>
        ///   Registers the specified delegate for events of the specified type.
        /// </summary>
        /// <param name="eventType">Type of the event the caller is interested in.</param>
        /// <param name="callback">Delegate to invoke when specified type occurred.</param>
        /// <exception cref="ArgumentNullException">Specified delegate is null.</exception>
        /// <exception cref="ArgumentNullException">Specified event type is null.</exception>
        public void RegisterListener(object eventType, EventDelegate callback)
        {
            if (eventType == null)
            {
                throw new ArgumentNullException("eventType");
            }

            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            if (this.listeners.ContainsKey(eventType))
            {
                this.listeners[eventType] += callback;
            }
            else
            {
                this.listeners[eventType] = callback;
            }
        }

        /// <summary>
        ///   Unregisters the specified delegate for events of the specified type.
        /// </summary>
        /// <param name="eventType">Type of the event the caller is no longer interested in.</param>
        /// <param name="callback">Delegate to remove.</param>
        /// <exception cref="ArgumentNullException">Specified delegate is null.</exception>
        /// <exception cref="ArgumentNullException">Specified event type is null.</exception>
        public void RemoveListener(object eventType, EventDelegate callback)
        {
            if (eventType == null)
            {
                throw new ArgumentNullException("eventType");
            }

            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            if (this.listeners.ContainsKey(eventType))
            {
                this.listeners[eventType] -= callback;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Notifies all interested listeners of the specified event.
        /// </summary>
        /// <param name="e">Event to pass to listeners.</param>
        private void ProcessEvent(Event e)
        {
            EventDelegate eventListeners;

            if (!this.listeners.TryGetValue(e.EventType, out eventListeners))
            {
                return;
            }

            if (eventListeners != null)
            {
                eventListeners(e);
            }
        }

        #endregion
    }
}