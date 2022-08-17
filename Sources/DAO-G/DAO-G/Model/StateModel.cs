using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Model
{
    public abstract class StateModel : BaseModel
    {

		private int _state;

		public int State
		{
			get { return _state; }
			set { _state = value; }
		}
	}
}
