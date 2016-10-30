using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS.FuzzySystem;
using FuzzySystem;
using FuzzySystem.Rules;
using FIMAE.FIMAS.KnowledgeBase;

namespace FIMAE.FIMAS
{
    public class KnowledgeBaseAgent
    {
        private FuzzyAgent _fuzzyAgent;
        private RuleBase _fuzzyRuleBase;

        public KnowledgeBaseAgent(FuzzyAgent fuzzyAgent)
        {
            _fuzzyAgent = fuzzyAgent;

            _fuzzyRuleBase = new RuleBase();
            _fuzzyAgent.FuzzyController.Elements.Add(_fuzzyRuleBase);
        }

        public List<KnowledgeBaseVariable> GetValiables()
        {
            var result = new List<KnowledgeBaseVariable>();

            var fuzzyVars = _fuzzyAgent.FuzzyController.Elements.Where(e => e.Type == ElementType.Variable).OfType<FuzzyVariable>().ToList();
            foreach(var fuzzyVar in fuzzyVars)
            {
                var knowledgeBaseVar = new KnowledgeBaseVariable()
                {
                    Name = fuzzyVar.Name
                };
                foreach(var term in fuzzyVar.Terms)
                {
                    knowledgeBaseVar.Terms.Add(new KnowledgeBaseTerm(term.Name, term.A, term.B, term.C, term.D));
                }

                result.Add(knowledgeBaseVar);
            }

            return result;
        }

        public void AddVariable(KnowledgeBaseVariable variable)
        {
            FuzzyVariable fuzzyVariable = new FuzzyVariable();
            fuzzyVariable.Name = variable.Name;
            foreach (var term in variable.Terms)
            {
                //ToDo: Add supporting of sigmoid term
                fuzzyVariable.Terms.Add(new TrapezoidTerm(term.Name, term.A, term.B, term.C, term.D));
            }
            _fuzzyAgent.FuzzyController.Elements.Add(fuzzyVariable);
        }

        public List<KnowledgeBaseRule> GetRules()
        {
            var result = new List<KnowledgeBaseRule>();

            var fuzzyRuleBase = (_fuzzyAgent.FuzzyController.Elements.FirstOrDefault(e => e.Type == ElementType.Rules) as RuleBase);
            foreach (var fuzzyRule in fuzzyRuleBase.Rules)
            {
                var rule = new KnowledgeBaseRule();
                //conditions
                foreach (var condition in fuzzyRule.Conditions)
                {
                    KnowledgeBaseVariable variable = GetValiables().FirstOrDefault(e => e.Name == condition.Variable.Name);
                    if (variable != null)
                    {
                        KnowledgeBaseTerm term = variable.Terms.FirstOrDefault(t => t.Name == condition.Term.Name);
                        if (term != null)
                        {
                            rule.Conditions.Add(new KnowledgeBaseExpression(variable, term));
                        }
                    }
                }
                //result
                if (fuzzyRule.Results.Count > 0)
                {
                    KnowledgeBaseVariable resultVar = GetValiables().FirstOrDefault(e => e.Name == fuzzyRule.Results[0].Variable.Name);
                    if (resultVar != null)
                    {
                        KnowledgeBaseTerm term = resultVar.Terms.FirstOrDefault(t => t.Name == fuzzyRule.Results[0].Term.Name);
                        if (term != null)
                        {
                            rule.Result = new KnowledgeBaseExpression(resultVar, term);
                        }
                    }
                }
                result.Add(rule);
            }

            return result;
        }
        
        public void AddRule(KnowledgeBaseRule rule)
        {
            Rule fuzzyRule = new Rule();
            foreach(var condition in rule.Conditions)
            {
                FuzzyVariable variable = (_fuzzyAgent.FuzzyController.Elements.FirstOrDefault(e => e.Name == condition.Variable.Name) as FuzzyVariable);
                if (variable != null)
                {
                    Term term = variable.Terms.FirstOrDefault(t => t.Name == condition.Term.Name);
                    if (term != null)
                    {
                        fuzzyRule.CreateCondition(variable, term);
                    }
                }
            }

            if (rule.Result != null)
            {
                FuzzyVariable resultVar = (_fuzzyAgent.FuzzyController.Elements.FirstOrDefault(e => e.Name == rule.Result.Variable.Name) as FuzzyVariable);
                if (resultVar != null)
                {
                    Term term = resultVar.Terms.FirstOrDefault(t => t.Name == rule.Result.Term.Name);
                    if (term != null)
                    {
                        fuzzyRule.CreateResult(resultVar, term);
                    }
                }
            }

            _fuzzyRuleBase.AddRule(fuzzyRule);
        }

        public DefiningFeatureValue Calculate(DefiningFeature inputFeature, DefiningFeature outputFeature)
        {
            return null;
        }

    }
}
