using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.Extensions
{
    public static partial class UnityExtensions
    {
        public static void AddListerer(this EventTrigger eventTrigger, EventTriggerType eventType,UnityAction<BaseEventData> action)
        {
            if (action == null)
                return;
            EventTrigger.Entry entry=null;
            foreach(var item in eventTrigger.triggers)
            {
                if(item.eventID== eventType)
                {
                    entry = item;
                    break;
                }
            }
            if (entry == null)
            {
                entry = new EventTrigger.Entry() { eventID = eventType };
                eventTrigger.triggers.Add(entry);
            }
            entry.callback.AddListener(action);
        }

        public static void RemoveListerer(this EventTrigger eventTrigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
        {
            if (action == null)
                return;
            EventTrigger.Entry entry = null;
            foreach (var item in eventTrigger.triggers)
            {
                if (item.eventID == eventType)
                {
                    entry = item;
                    break;
                }
            }
            if (entry == null)
            {
                return;
            }
            entry.callback.RemoveListener(action);
        }
    }
}