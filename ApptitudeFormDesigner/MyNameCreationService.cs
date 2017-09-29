using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;

namespace ApptitudeFormDesigner
{
    class MyNameCreationService : INameCreationService
    {
        private Dictionary<string, int> typeTracking = new Dictionary<string, int>();
        #region INameCreationService Members

        public string CreateName(System.ComponentModel.IContainer container, Type dataType)
        {
            string type = dataType.Name;

            if (typeTracking.ContainsKey(type))
            {
                typeTracking[type]++;
            }
            else
            {
                typeTracking.Add(type, 1);
            }

            return type + typeTracking[type];

        }

        
        //TODO: Really should implement this but for this prototype maybe not
        public bool IsValidName(string name)
        {
            return true; // throw new NotImplementedException();
        }

        public void ValidateName(string name)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
