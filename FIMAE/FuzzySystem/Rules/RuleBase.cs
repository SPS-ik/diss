using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FuzzySystem.Rules
{

    public class RuleBase : FuzzyElementBase
    {
        public List<Rule> Rules = new List<Rule>();
        public InferenceMethodDelegate InferenceRules = FuzzyMath.Max;
        public double gamma = 1.0;

        public RuleBase(): base(ElementType.Rules)
        {
        }

        public void AddRule(Rule rule)
        {
            Rules.Add(rule);
        }

        public Rule CreateRule()
        {
            Rule r = new Rule();
            for (int i = 0; i < InLinks.Count; i++)
                r.CreateCondition(InLinks[i].Source as FuzzyVariable, null);
            for (int i = 0; i < OutLinks.Count; i++)
                r.CreateResult(OutLinks[i].Destination as FuzzyVariable, null);
            return r;
        }

        public void RemoveRule(Rule rule)
        {
            Rules.Remove(rule);
        }

        public override void AddInLink(Link l)
        {
            base.AddInLink(l);

            for (int i = 0; i < Rules.Count; i++)
                Rules[i].CreateCondition(l.Source as FuzzyVariable, null);
        }

        public override void AddOutLink(Link l)
        {
            base.AddOutLink(l);

            for (int i = 0; i < Rules.Count; i++)
                Rules[i].CreateResult(l.Destination as FuzzyVariable, null);
        }

        // need refactoring
        public override void RemoveInLink(Link l)
        {
            base.RemoveInLink(l);
            foreach (var rule in Rules)
            {
                rule.Conditions.Remove(rule.Conditions.Find(t => t.Variable.Name == l.Source.Name));
            }
        }

        // need refactoring
        public override void RemoveOutLink(Link l)
        {
            base.RemoveOutLink(l);
            foreach (var rule in Rules)
            {
                rule.Results.Remove(rule.Conditions.Find(t => t.Variable.Name == l.Destination.Name));
            }
        }

        public override void Calculate()
        {
            base.Calculate();

            Hashtable list = new Hashtable();
            Term term;
            Rule r;
            for (int i = 0; i < Rules.Count; i++)
            {
                r = Rules[i];
                double alfaLevelOfCondition = r.AlfaLevelOfCondition() * r.Weight;

                for (int j = 0; j < r.Results.Count; j++)
                {
                    if ((term = r.Results[j].Term) == null) continue;
                    if (!list.ContainsKey(term))
                        list.Add(term, new List<double>());
                    (list[term] as List<double>).Add(alfaLevelOfCondition);
                }
            }
            foreach (Term t in list.Keys)
                t.AlfaLevel = InferenceRules(list[t] as List<double>, gamma);
        }
    }
}
