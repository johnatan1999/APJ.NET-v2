using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models.User
{
    public class Historique : BaseModel
    {
        #region properties
        private string _idUser;

		public string IdUser
		{
			get { return _idUser; }
			set { _idUser = value; }
		}

		private string _objectId;

		public string RefObject
		{
			get { return _objectId; }
			set { _objectId = value; }
		}


		private string _action;

		public string Action
		{
			get { return _action; }
			set { _action = value; }
		}

		private string _object;

		public string Object
		{
			get { return _object; }
			set { _object = value; }
		}

		private DateTime _date;

		public DateTime DateAction
		{
			get { return _date; }
			set { _date = value; }
		}

		private string _time;

		public string TimeAction
		{
			get { return _time; }
			set { _time = value; }
		}
        #endregion

        public Historique()
		{
			SetTableName("Historique");
			SetSequenceName("historique_seq");
			SetPkLength(10);
			SetIndicePk("HIST");
		}

	}
}
