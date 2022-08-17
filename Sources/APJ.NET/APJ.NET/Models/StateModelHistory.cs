using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models
{
    public class StateModelHistory : BaseModelHistory
    {
		private int _state;

		public int State
		{
			get { return _state; }
			set { _state = value; }
		}
	}
}
