using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Ai.EQS
{
    public class EQS_ContextFilter : IEQS_ContextFilter
    {
        private List<FilterWrapper> elements = new List<FilterWrapper>();

        private IEQS_Context context;

        public IEQS_ContextElement GetContextElement()
        {
            if (elements.Count > 0)
                return elements[0].element;
            return null;
        }

        public List<IEQS_ContextElement> GetContextElements()
        {
            Profiler.BeginSample("GetContextElements");
            List<IEQS_ContextElement> res = new List<IEQS_ContextElement>();
            foreach(FilterWrapper wrapper in elements)
            {
                res.Add(wrapper.element);
            }

            Profiler.EndSample();
            return res;
        }

        public void SetContext(IEQS_Context context)
        {
            elements.Clear();

            this.context = context;
        }

        public IEQS_ContextFilter AddElementsOfPath(string path)
        {
            Profiler.BeginSample("AddElementsOfPath");
            foreach (IEQS_ContextElement element in context.GetContextElements())
            {
                if (element.Path.Equals(path))
                    elements.Add(new FilterWrapper(element));
            }

            Profiler.EndSample();
            return this;
        }

        public IEQS_ContextFilter AddElementsByChildrenPath(string childPath, string path = "")
        {
            Profiler.BeginSample("AddElementsByChildrenPath");
            foreach (IEQS_ContextElement element in context.GetContextElements())
            {
                if ((path.Equals("") || element.Path.Equals(path)) && element.GetChildrenByPath(childPath).Count > 0)
                    elements.Add(new FilterWrapper(element));
            }

            Profiler.EndSample();
            return this;
        }

        public IEQS_ContextFilter SortByDistance(Vector3 targetPosition, int multiply = 1)
        {
            Profiler.BeginSample("SortByDistance");
            foreach (FilterWrapper wrapper in elements)
            {
                wrapper.weight += 1 * multiply / Vector3.Distance(targetPosition, wrapper.element.Position);
            }
            SortElements();

            Profiler.EndSample();
            return this;
        }


        private void SortElements()
        {
            Profiler.BeginSample("SortElements");
            elements.Sort((first, second) => { return first.weight.CompareTo(second.weight); });
            Profiler.EndSample();
        }

        private class FilterWrapper
        {
            public IEQS_ContextElement element;
            public float weight = 0;

            public FilterWrapper() { }

            public FilterWrapper(IEQS_ContextElement element)
            {
                this.element = element;
            }
        }
    }
}