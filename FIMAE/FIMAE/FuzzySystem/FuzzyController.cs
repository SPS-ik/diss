using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using FIMAE.FuzzySystem.Rules;

namespace FIMAE.FuzzySystem
{
    public class FuzzyController
    {
        public List<FuzzyElementBase> Elements = new List<FuzzyElementBase>();
        public List<Link> Links = new List<Link>();

        public FuzzyElementBase AddElement(ElementType eType, string name)
        {
            FuzzyElementBase el = null;
            if (eType == ElementType.Variable)
            {
                el = new FuzzyVariable();
            }
            if (eType == ElementType.Rules)
                el = new RuleBase();

            el.Name = name;
            el.Type = eType;
            Elements.Add(el);

            return el;
        }

        public void DeleteElement(FuzzyElementBase el)
        {
            Elements.Remove(el);
        }

        public Link CreateLink(FuzzyElementBase source, FuzzyElementBase destination)
        {
            Link l = new Link()
            {
                Source = source,
                Destination = destination
            };

            source.AddOutLink(l);
            destination.AddInLink(l);

            Links.Add(l);
            return l;
        }

        public void DeleteLink(Link l)
        {
            l.Destination.RemoveInLink(l);
            l.Source.RemoveOutLink(l);

            Links.Remove(l);
        }
    }
}
