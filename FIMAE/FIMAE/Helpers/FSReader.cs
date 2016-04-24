using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using FIMAE.FuzzySystem;
using FIMAE.FuzzySystem.Rules;

namespace FIMAE.Helpers
{
	public class FSReader
	{
		private FuzzyController fuzzyController;
		private XmlDocument doc;
		private XmlNodeList root;

        private Dictionary<string, FuzzyElementBase> dElements = new Dictionary<string, FuzzyElementBase>();

        public FSReader(FuzzyController fc)
		{
			this.fuzzyController = fc;
		}
		public void Read(string path)
		{
			doc = new XmlDocument();
			doc.Load(path);
			root = doc["FuzzySystem"].ChildNodes;

			for (int i = 0; i < root.Count; i++)
			{
				if (root[i].Name == "Element")
					CreateElement(root[i]);
				if (root[i].Name == "Element" && root[i].Attributes["Type"].Value == "Variable")
					ConfigureVariable(root[i]);						
			}

			for (int i = 0; i < root.Count; i++)
				if (root[i].Name == "Link")
					CreateLink(root[i]);

			for (int i = 0; i < root.Count; i++)
				if (root[i].Name == "Element" && root[i].Attributes["Type"].Value == "RuleBase")
					ConfigureRuleBase(root[i]);		
		}

		private void CreateElement(XmlNode n)
		{
            FuzzyElementBase fe = null;

            if (n.Attributes["Type"].Value == "Variable")
            {
                fe = new FuzzyVariable();
                fe.Type = ElementType.Variable;
            }
            if (n.Attributes["Type"].Value == "RuleBase")
            {
                fe = new RuleBase();
                fe.Type = ElementType.Rules;
            }

            if (fe != null)
            {
                fe.Name = n.Attributes["Name"].Value;
                fuzzyController.Elements.Add(fe);
                dElements.Add(fe.Name, fe);
            }
		}
		private void CreateLink(XmlNode n)
		{
			Link gl = new Link();

            gl.Source = dElements[n.Attributes["Source"].Value];
            gl.Destination = dElements[n.Attributes["Destination"].Value];
			
			gl.Source.OutLinks.Add(gl);
			gl.Destination.InLinks.Add(gl);

            fuzzyController.Links.Add(gl);
		}

		private void ConfigureVariable(XmlNode n)
		{
			XmlNodeList terms = n.ChildNodes;

			var v = dElements[n.Attributes["Name"].Value];

			for (int i = 0; i < terms.Count; i++)
			{
				if (terms[i].Attributes["Type"].Value == "Trapezoid")
					(v as FuzzyVariable).AddTerm(terms[i].Attributes["Name"].Value, TermTypes.Trapezoid, 
											double.Parse(terms[i].Attributes["A"].Value),
											double.Parse(terms[i].Attributes["B"].Value),
											double.Parse(terms[i].Attributes["C"].Value),
											double.Parse(terms[i].Attributes["D"].Value));

				if (terms[i].Attributes["Type"].Value == "Sigmoid")
                    (v as FuzzyVariable).AddTerm(terms[i].Attributes["Name"].Value, TermTypes.Sigmoid, 
											double.Parse(terms[i].Attributes["A"].Value),
											double.Parse(terms[i].Attributes["B"].Value),
											double.Parse(terms[i].Attributes["C"].Value),
											double.Parse(terms[i].Attributes["D"].Value));
			}
		}

		private void ConfigureRuleBase(XmlNode n)
		{
			XmlNodeList rules = n.ChildNodes;
			XmlNodeList conditions, results;
			Rule rule;
			FuzzyVariable v;
			RuleBase rb = dElements[n.Attributes["Name"].Value] as RuleBase;
			string termName;

			rb.gamma = double.Parse(n.ChildNodes[0].Attributes["GammaParameter"].Value);
			if (n.ChildNodes[0].Attributes["Type"].Value == "Gamma")
			{
				rb.InferenceRules = FuzzyMath.Gamma;
			}

			for (int i = 1; i < rules.Count; i++)
			{
				rule = rb.CreateRule();
				rule.Weight = double.Parse(n.ChildNodes[i].Attributes["Weight"].Value);
				
				conditions = n.ChildNodes[i]["Conditions"].ChildNodes;
				results = n.ChildNodes[i]["Results"].ChildNodes;
								
				for (int j = 0; j < conditions.Count; j++)
				{
					v = dElements[conditions[j].Attributes["Var"].Value] as FuzzyVariable;
					//rule.CreateCondition(v, v.terms[conditions[j].Attributes["Val"].Value]);
                    termName = conditions[j].Attributes["Val"].Value;
                    rule.Conditions.Find(c => c.Variable.Name == v.Name).Term = termName == "" ? null : v.Terms.Find(term => term.Name == termName);
				}
				for (int j = 0; j < results.Count; j++)
				{
					v = dElements[results[j].Attributes["Var"].Value] as FuzzyVariable;
					//rule.CreateResult(v, v.terms[results[j].Attributes["Val"].Value]);
                    termName = results[j].Attributes["Val"].Value;
                    rule.Results.Find(c => c.Variable.Name == v.Name).Term = termName == "" ? null : v.Terms.Find(term => term.Name == termName);
				}

				rb.AddRule(rule);
			}
		}

	}
}
