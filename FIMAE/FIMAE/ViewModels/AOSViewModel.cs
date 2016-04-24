using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS;

namespace FIMAE.ViewModels
{
    public class AOSViewModel : BaseViewModel
    {
        private AgentOrientedSubsystem _aos;

        public AOSViewModel(AgentOrientedSubsystem aos)
        {
            _aos = aos;
        }

        public string FeatureName
        {
            get { return _aos.CurrentFeature.Name; }
        }

        public List<string> FeatureValue
        {
            get { return _aos.CurrentFeature.Values; }
        }
    }
}
