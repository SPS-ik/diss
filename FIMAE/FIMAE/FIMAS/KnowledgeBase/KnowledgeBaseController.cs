using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FuzzySystem;
using FIMAE.FuzzySystem.Rules;
using FIMAE.FIMAS.KnowledgeBase;

namespace FIMAE.FIMAS
{
    public class KnowledgeBaseController
    {
        private FuzzyController FuzzyController = new FuzzyController();
        private RuleBase _fuzzyRuleBase;

        public KnowledgeBaseController()
        {
            _fuzzyRuleBase = new RuleBase();
            FuzzyController.Elements.Add(_fuzzyRuleBase);
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
            FuzzyController.Elements.Add(fuzzyVariable);
        }

        public void AddRule(KnowledgeBaseRule rule)
        {
            Rule fuzzyRule = new Rule();
            foreach(var condition in rule.Conditions)
            {
                FuzzyVariable variable = (FuzzyController.Elements.FirstOrDefault(e => e.Name == condition.Variable.Name) as FuzzyVariable);
                if (variable != null)
                {
                    Term term = variable.Terms.FirstOrDefault(t => t.Name == condition.Term.Name);
                    if (term != null)
                    {
                        fuzzyRule.CreateCondition(variable, term);
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
