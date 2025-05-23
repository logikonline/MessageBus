using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DSoft.MessageBus
{
	/// <summary>
	/// Collection of messagebuseventhandlers
	/// </summary>
	internal class MessageBusEventHandlerCollection : Collection<MessageBusEventHandler>
	{
		#region Methods

		/// <summary>
		/// Handlers for event.
		/// </summary>
		/// <param name="EventId">The event identifier.</param>
		/// <returns></returns>
		internal MessageBusEventHandler[] HandlersForEvent(string EventId)
		{
			var results = from item in this.Items
						  where item != null
						  where !string.IsNullOrWhiteSpace(item.EventId)
						  where string.Equals(item.EventId, EventId, StringComparison.OrdinalIgnoreCase)
						  where item.EventAction != null
						  select item;

			return results.ToArray();
		}

		/// <summary>
		/// Handlerses for event type
		/// </summary>
		/// <returns>The for event.</returns>
		/// <param name="EventType">Event type.</param>
		internal MessageBusEventHandler[] HandlersForEvent(Type EventType)
		{
			var list = new List<MessageBusEventHandler>();

			foreach (var item in this.Items)
			{
				if (item is TypedMessageBusEventHandler typedItem &&
					typedItem.EventAction != null &&
					typedItem.EventType != null &&
					typedItem.EventType.Equals(EventType))
				{
					list.Add(typedItem);
				}
			}

			return list.ToArray();
		}

		/// <summary>
		/// Returns the event handlers for the specified Generic MessageBusEvent Type 
		/// </summary>
		/// <returns>The for event.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		internal MessageBusEventHandler[] HandlersForEvent<T> () where T : MessageBusEvent
		{
			return HandlersForEvent (typeof(T));
		}

		#endregion
	}
}

