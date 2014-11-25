using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Metrics
{
    public class EventTimeline
    {
        private readonly List<TimelineEvent> _events;
        private float _current;
        private int _currentEvent;

        public EventTimeline()
        {
            _events = new List<TimelineEvent>();
            _current = 0f;
        }

        public EventTimeline Add(TimelineEvent ev)
        {
            _events.Add(ev);
            Sort();
            return this;
        }

        private void Sort()
        {
            _events.Sort((x, y) => x.Time.CompareTo(y.Time));
        }

        public void Update(float dt)
        {
            if (_currentEvent > _events.Count - 1)
                return;

            _current += dt;

            var ev = _events[_currentEvent];
            if (ev.Time <= _current)
            {
                ev.Invoke();
                _currentEvent++;
            }
        }
    }

    public class TimelineEvent
    {
        private readonly EventTrigger _callback;
        private readonly float _time;

        public delegate void EventTrigger(float time);

        public TimelineEvent(EventTrigger callback, float time)
        {
            if (callback == null) throw new ArgumentNullException("callback");
            _callback = callback;
            _time = time;
        }

        public float Time 
        {
            get { return _time; }
        }

        public void Invoke()
        {
            _callback(_time);
        }
    }
}
