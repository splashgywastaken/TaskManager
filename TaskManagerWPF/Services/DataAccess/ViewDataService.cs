using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Services.DataAccess
{
    class ViewDataService
    {
        private readonly Dictionary<string, object> _viewDictionary;

        public ViewDataService()
        {
            _viewDictionary = new Dictionary<string, object>();
        }

        public void AddView(object obj, string viewName)
        {
            _viewDictionary.Remove(viewName);
            _viewDictionary.Add(viewName, obj);
        }

        public object? GetView(string viewName)
        {
            return !_viewDictionary.ContainsKey(viewName) ? null : _viewDictionary[viewName];
        }
    }
}
