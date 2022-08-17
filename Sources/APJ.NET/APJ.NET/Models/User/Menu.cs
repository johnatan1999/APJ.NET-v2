using Apj.Net.Dao.Annotations;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace APJ.NET.Models.User
{
    public class Menu : BaseModel
    {

        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _label;

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        private string _idParent;

        [ForeignKey("Menu", tableName = "Menu")]
        public string IdParent
        {
            get { return _idParent; }
            set { _idParent = value; }
        }

        private int _rank;

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public virtual IList<Menu> Submenus { get; set; }

        public Menu()
        {
            SetTableName("Menu");
            SetPkLength(10);
            SetIndicePk("MENU");
            Submenus = new List<Menu>();
        }

    }
}
