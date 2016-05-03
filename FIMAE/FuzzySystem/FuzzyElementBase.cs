using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySystem
{
    public enum ElementType
    {
        Variable = 0,
        Rules = 1
    }

    public class FuzzyElementBase
    {
        public string Name;
        public ElementType Type;

        public FuzzyElementBase()
        {

        }

        public FuzzyElementBase(ElementType type)
        {
            Type = type;
        }

        public List<Link> InLinks = new List<Link>();
        public List<Link> OutLinks = new List<Link>();

        public virtual void Calculate()
        {
            for (int i = 0; i < InLinks.Count; i++)
                InLinks[i].Source.Calculate();
        }

        public virtual void RemoveInLink(Link l)
        {
            InLinks.Remove(l);
        }

        public virtual void RemoveOutLink(Link l)
        {
            OutLinks.Remove(l);
        }

        public virtual void AddInLink(Link l)
        {
            InLinks.Add(l);
        }

        public virtual void AddOutLink(Link l)
        {
            OutLinks.Add(l);
        }
    }

}
