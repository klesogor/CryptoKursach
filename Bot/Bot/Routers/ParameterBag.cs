using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Routers
{
    public class ParameterBag
    {
        private readonly Dictionary<string, object> _bag;

        public ParameterBag(Dictionary<string, object> bag)
        {
            _bag = bag;
        }

        public ParameterBag()
        {
            _bag = new Dictionary<string, object>();
        }

        public ParameterBag AddParameter(string param, object val)
        {
            if (_bag.ContainsKey(param)) _bag.Remove(param);
            _bag.Add(param, val);

            return this;
        }

        public object GetItem(string key)
        {
            _bag.TryGetValue(key, out object res);

            return res;
        }

        public string GetObjectAsString(string key)
        {
            return GetItem(key).ToString();
        }
    }
}
